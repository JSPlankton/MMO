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

    //登录信息
    public LoginRet LoginInfo { get; set; }
    
    //主角信息
    public MainRoleInfo MRInfo { get; set; }
    
    public MainRoleCtrl MRCtrl { get; set; }

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
