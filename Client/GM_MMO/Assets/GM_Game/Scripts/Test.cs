using System.Collections;
using UnityEngine;
using YooAsset;



/**
 * Title:
 * Description:
 */


public class Test : MonoBehaviour
{

    private ResourcePackage _package;

    private void Awake()
    {

        //��ʼ��YooAsset
        InitYooAsset();
    }

    /// <summary>
    /// ��ʼ��YooAsset
    /// </summary>
    private void InitYooAsset()
    {
        // ��ʼ����Դϵͳ
        YooAssets.Initialize();

        // ����Ĭ�ϵ���Դ��
        _package = YooAssets.CreatePackage("DefaultPackage");

        // ���ø���Դ��ΪĬ�ϵ���Դ��������ʹ��YooAssets��ؼ��ؽӿڼ��ظ���Դ�����ݡ�
        YooAssets.SetDefaultPackage(_package);

        StartCoroutine(InitPackage());
    }


    private IEnumerator InitPackage()
    {
        //�༭��ģʽ
        EditorSimulateModeParameters editorParameters = new EditorSimulateModeParameters();
        editorParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild("DefaultPackage");
        InitializationOperation operation = _package.InitializeAsync(editorParameters);

        gameObject.AddComponent<Global>();


        yield break;
    }


}
