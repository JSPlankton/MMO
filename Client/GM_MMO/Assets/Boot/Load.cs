using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using YooAsset;
using static UnityEngine.Rendering.ReloadAttribute;

/**
 * Title:
 * Description:
 */


public class Load : MonoBehaviour {

    [SerializeField, Header("����ģʽ")] private EPlayMode _playMode = EPlayMode.EditorSimulateMode;
    [SerializeField, Header("��Դϵͳ��ַ")] private string defaultHostServer;
    [SerializeField, Header("���õ�ַ")] private string fallbackHostServer;

    [SerializeField, Header("�ȸ���View")] private HotUpdateView _hotUpdateView;

    private ResourcePackage _package;


    //��ȡ��Դ������
    private static Dictionary<string, byte[]> s_assetDatas = new Dictionary<string, byte[]>();


    //����Ԫ����dll���б�Yooasset�в���Ҫ����׺
    public static List<string> AOTMetaAssemblyNames { get; } = new List<string>()
    {
        "mscorlib.dll",
        "System.dll",
        "System.Core.dll",
        //"Assembly-CSharp.dll"
    };

    private void Awake() {

        //��ʼ��YooAsset
        InitYooAsset();
    }

    /// <summary>
    /// ��ʼ��YooAsset
    /// </summary>
    private void InitYooAsset() {
        // ��ʼ����Դϵͳ
        YooAssets.Initialize();

        // ����Ĭ�ϵ���Դ��
        _package = YooAssets.CreatePackage("DefaultPackage");

        // ���ø���Դ��ΪĬ�ϵ���Դ��������ʹ��YooAssets��ؼ��ؽӿڼ��ظ���Դ�����ݡ�
        YooAssets.SetDefaultPackage(_package);

        StartCoroutine(InitPackage());
    }


    private IEnumerator InitPackage() {


        InitializationOperation operation = null;

        switch (_playMode) {
            case EPlayMode.EditorSimulateMode:
                //�༭��ģʽ
                EditorSimulateModeParameters editorParameters = new EditorSimulateModeParameters();
                editorParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild("DefaultPackage");
                operation = _package.InitializeAsync(editorParameters);
                break;

            case EPlayMode.HostPlayMode:
                //��������ģʽ
                HostPlayModeParameters hostParameters = new HostPlayModeParameters();
                hostParameters.BuildinQueryServices = new GameQueryServices(); //̫��ս��DEMO�Ľű��࣬��ϸ��StreamingAssetsHelper
                hostParameters.DeliveryQueryServices = new GameDeliveryQueryServices();
                hostParameters.DecryptionServices = new GameDecryptionServices();
                hostParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                operation = _package.InitializeAsync(hostParameters);

                break;
        }

        //�ȴ���ʼ�����..
        yield return operation;

        Debug.Log("operation::" + operation.Status);
        if (operation.Status != EOperationStatus.Succeed) {
            Debug.Log($"{operation.Error}");
            yield break;
        }


        //��ʼ����ɺ󣬻�ȡ���汾��Ϣ
        var versionOperation = _package.UpdatePackageVersionAsync();
        yield return versionOperation;
        if (operation.Status != EOperationStatus.Succeed) {
            Debug.LogError("RequestVersion Error::" + operation.Error);
            yield break;
        }

        //������Դ�嵥
        var manifestOperation = _package.UpdatePackageManifestAsync(versionOperation.PackageVersion);
        yield return manifestOperation;



        //������ؽ��
        if (manifestOperation.Status != EOperationStatus.Succeed) {
            Debug.LogError("UpdateManifest Error::" + manifestOperation.Error);
            yield break;
        }

        //��ʼ����
        yield return Download();
    }

    private IEnumerator Download() {
        int downloadingMaxNum = 10;
        int failedTryAgain = 3;
        var package = YooAssets.GetPackage("DefaultPackage");
        var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);


        //û����Ҫ���ص���Դ
        if (downloader.TotalDownloadCount == 0) {

            _hotUpdateView.RefreshUI(1, "û����Դ����..");
            yield return InitCode();
            yield break;
        }


        //��Ҫ���ص��ļ��������ܴ�С 
        int totalDownloadCount = downloader.TotalDownloadCount;
        long totalDownloadBytes = downloader.TotalDownloadBytes;

        //ע��ص�����
        downloader.OnDownloadOverCallback = OnDownloadOverCallback; //�����������������۳ɹ���ʧ�ܣ�
        downloader.OnDownloadErrorCallback = OnDownloadErrorCallback; //����������������
        downloader.OnDownloadProgressCallback = OnDownloadProgressCallback; //�����ؽ��ȷ����仯
        downloader.OnStartDownloadFileCallback = OnStartDownloadFileCallback; //����ʼ����ĳ���ļ�

        //��������
        downloader.BeginDownload();
        yield return downloader;

        //������ؽ��
        if (downloader.Status == EOperationStatus.Succeed) {
            //���سɹ�
            yield return InitCode();
        }
        else {
            //����ʧ��
            Debug.Log("����ʧ��...");
            yield break;
        }
    }

    /// <summary>
    /// ��ʼ������Ԫ����dll
    /// </summary>
    /// <returns></returns>
    private IEnumerator InitCode() {

        var assets = new List<string> {
               "GM_Game.dll"
        }.Concat(AOTMetaAssemblyNames);

        foreach (var asset in assets) {
            var dllHandle = _package.LoadAssetAsync<TextAsset>("Assets/GM_Game/Dlls/" + asset);
            yield return dllHandle;
            TextAsset textAsset = dllHandle.AssetObject as TextAsset;
            s_assetDatas[asset] = textAsset.bytes;
            Debug.Log($"dll:{asset} size:{textAsset.bytes.Length}");
        }

        LoadMetadataForAOTAssemblies();
#if !UNITY_EDITOR
        // Editor�����£�GM_Game.dll.bytes�Ѿ����Զ����أ�����Ҫ���أ��ظ����ط���������⡣
        Assembly.Load(s_assetDatas["GM_Game.dll"] );
#endif

        yield return EnterGame();
    }

    private IEnumerator EnterGame() {

        SceneOperationHandle handle = _package.LoadSceneAsync("Assets/GM_Game/Scenes/Scene_Login");
        yield return handle;
        Debug.Log("���س������..");

    }

    private static void LoadMetadataForAOTAssemblies() {

        /// ע�⣬����Ԫ�����Ǹ�AOT dll����Ԫ���ݣ������Ǹ��ȸ���dll����Ԫ���ݡ�
        /// �ȸ���dll��ȱԪ���ݣ�����Ҫ���䣬�������LoadMetadataForAOTAssembly�᷵�ش���
        HomologousImageMode mode = HomologousImageMode.SuperSet;
        foreach (var aotDllName in AOTMetaAssemblyNames) {
            byte[] dllBytes = s_assetDatas[aotDllName];
            // ����assembly��Ӧ��dll�����Զ�Ϊ��hook��һ��aot���ͺ�����native���������ڣ��ý������汾����
            LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
            Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
        }
    }

    //����ʼ����ĳ���ļ�
    private void OnStartDownloadFileCallback(string fileName, long sizeBytes) {
        Debug.Log($"��ʼ���أ�{fileName}  ��С��{sizeBytes / 1024f}KB");
    }

    //�����ؽ��ȷ����仯
    private void OnDownloadProgressCallback(int totalDownloadCount, int currentDownloadCount,
        long totalDownloadBytes, long currentDownloadBytes) {

        float prgs = currentDownloadBytes * 1.0f / totalDownloadBytes;

        _hotUpdateView.RefreshUI(prgs,
            $"���ؽ���::{currentDownloadBytes / 1024 / 1024}M / {totalDownloadBytes / 1024 / 1024}M ��{prgs * 100}%�� ");

        Debug.Log($"�ļ�����:: {totalDownloadCount} �������ļ���::{currentDownloadCount} �ܴ�С::{totalDownloadBytes / 1024.0f / 1024} M " +
           $"  �����ش�С::{currentDownloadBytes / 1024}KB");
    }

    //����������������
    private void OnDownloadErrorCallback(string fileName, string error) {
        Debug.Log($"����ʧ��::{fileName}  Error::{error}");
    }

    //�����������������۳ɹ���ʧ�ܣ�
    private void OnDownloadOverCallback(bool isSucceed) {
        Debug.Log("����" + (isSucceed ? " �ɹ� " : "ʧ��") + " ....");
    }

}




internal class GameDecryptionServices : IDecryptionServices {
    public ulong LoadFromFileOffset(DecryptFileInfo fileInfo) {
        return 32;
    }

    public byte[] LoadFromMemory(DecryptFileInfo fileInfo) {
        throw new NotImplementedException();
    }

    public Stream LoadFromStream(DecryptFileInfo fileInfo) {
        return new FileStream(fileInfo.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read); ;
    }

    public uint GetManagedReadBufferSize() {
        return 1024;
    }
}

internal class RemoteServices : IRemoteServices {

    private readonly string _defaultHostServer;
    private readonly string _fallbackHostServer;

    public RemoteServices(string defaultHostServer, string fallbackHostServer) {
        _defaultHostServer = defaultHostServer;
        _fallbackHostServer = fallbackHostServer;
    }


    public string GetRemoteFallbackURL(string fileName) {
        return $"{_fallbackHostServer}/{fileName}";
    }

    public string GetRemoteMainURL(string fileName) {
        return $"{_defaultHostServer}/{fileName}";
    }
}

internal class GameDeliveryQueryServices : IDeliveryQueryServices {
    public DeliveryFileInfo GetDeliveryFileInfo(string packageName, string fileName) {
        throw new NotImplementedException();
    }

    public bool QueryDeliveryFiles(string packageName, string fileName) {
        return false;
    }
}



internal class GameQueryServices : IBuildinQueryServices {

    public bool QueryStreamingAssets(string packageName, string fileName) {
        string filePath = Path.Combine(Application.streamingAssetsPath, "yoo", packageName, fileName);
        return File.Exists(filePath);
    }
}