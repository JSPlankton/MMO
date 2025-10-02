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

        //初始化YooAsset
        InitYooAsset();
    }

    /// <summary>
    /// 初始化YooAsset
    /// </summary>
    private void InitYooAsset()
    {
        // 初始化资源系统
        YooAssets.Initialize();

        // 创建默认的资源包
        _package = YooAssets.CreatePackage("DefaultPackage");

        // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
        YooAssets.SetDefaultPackage(_package);

        StartCoroutine(InitPackage());
    }


    private IEnumerator InitPackage()
    {
        //编辑器模式
        EditorSimulateModeParameters editorParameters = new EditorSimulateModeParameters();
        editorParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild("DefaultPackage");
        InitializationOperation operation = _package.InitializeAsync(editorParameters);

        gameObject.AddComponent<Global>();


        yield break;
    }


}
