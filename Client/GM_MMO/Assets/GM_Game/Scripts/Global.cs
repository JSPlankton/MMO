using UnityEngine;
using YooAsset;

/**
 * Title:
 * Description:
 */


public class Global : MonoBehaviour
{

    public static Global Instance;

    private ResourcePackage _package;
    public ResourcePackage YooPackage { get => _package; }

    //µÇÂ¼ÐÅÏ¢
    public LoginRet LoginInfo { get; set; }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
        _package = YooAssets.GetPackage("DefaultPackage");


        NetSocketMgr.Instance.Init();

    }

    private void OnApplicationQuit()
    {


        NetSocketMgr.Instance.Disconnect();

    }


}
