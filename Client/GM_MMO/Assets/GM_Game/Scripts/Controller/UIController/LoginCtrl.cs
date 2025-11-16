using Google.Protobuf;
using System;
using UnityEngine;
using YooAsset;

/**
 * Title:登录模块控制器
 * Description: 对视图进行显示，隐藏，以及数据的操作都在这个类
 */


public class LoginCtrl : CtrlBase
{

    private LoginView _loginView;
    private int _serverId;


    public LoginCtrl(UIBase view) : base(view)
    {

        _loginView = view as LoginView;
        _loginView.InitView();

        RegistCommand();
    }

    private void RegistCommand()
    {

        SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RegistCode, OnRegistHandle);
        SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_LoginCode, OnLoginHandle);
        SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_GetServerListCode, OnGetServerListHandle);
        SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_LoginGameServerCode, OnLoginGameServerHandle);

        //注册点击事件

        _loginView.RegistGameServerBtnClicked(OnGameServerBtnClicked);

    }

    /// <summary>
    /// 开始登录游戏服
    /// </summary>
    /// <param name="server"></param>
    private void OnGameServerBtnClicked(GameServer server)
    {
        _serverId = server.ServerId;
        //开始连接网关服务器
        NetSocketMgr.Instance.ConnectServer(server.IpHost, server.Prot, OnConnSucced, OnConnFailed);
        
    }

    /// <summary>
    /// 连接服务端失败
    /// </summary>
    private void OnConnFailed()
    {
        TipsMgr.Instance.ShowSystemTips("连接服务器失败");
    }

    /// <summary>
    /// 连接服务端成功
    /// </summary>
    private void OnConnSucced()
    {
        LoginGameServerReq req = new LoginGameServerReq()
        {
            AccountId = Global.Instance.LoginInfo.AccountId,
            GameServerId = _serverId,
        };

        NetSocketMgr.Client.SendData(NetDefine.CMD_LoginGameServerCode, req.ToByteString());
    }

    /// <summary>
    /// 登录游戏服务器返回数据
    /// </summary>
    /// <param name="data"></param>
    private void OnLoginGameServerHandle(ByteString data)
    {

        LoginGameServerRet ret = LoginGameServerRet.Parser.ParseFrom(data);

        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            Debug.Log("登录游戏服务器::" + ret.ToString());
            
            SceneMgr.Instance.LoadScene(SceneType.Scene_CreateRole, () =>
            {
                UIRoot.Instance.LoginViewCtrl.ShowView(false);

                if (ret.CreateRoleInfo != null)
                {
                    //1.是否已经有角色， 有角色.. 跳转选择角色的UI。
                    UIRoot.Instance.CreateRoleViewCtrl.ShowWindow(WindowType.SelectRoleWindow, ret.CreateRoleInfo);
                }
                else
                {
                    //2.如果还未创建角色，那么跳转创建角色UI界面
                    UIRoot.Instance.CreateRoleViewCtrl.ShowWindow(WindowType.CreateRoleWindow);
                }
            });
            
        }
    }

    /// <summary>
    /// 获取服务器列表返回数据
    /// </summary>
    /// <param name="data"></param>
    private void OnGetServerListHandle(ByteString data)
    {
        GetServerListRet ret = GetServerListRet.Parser.ParseFrom(data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            Debug.Log("获取服务器数据::" + ret.ToString());

            ShowWindow(WindowType.ServerListWindow, ret);
        }

    }

    /// <summary>
    /// 登录请求结果返回数据
    /// </summary>
    /// <param name="data"></param>
    private void OnLoginHandle(ByteString data)
    {

        LoginRet ret = LoginRet.Parser.ParseFrom(data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            Debug.Log("登录成功..." + ret.ToString());
            TipsMgr.Instance.ShowSystemTips("登录成功！！");
            Global.Instance.LoginInfo = ret;
            ShowWindow(WindowType.GameServerWindow, ret.GameServer);
        }
        else
        {
            Debug.Log("登录失败..." + ret.ToString());
            TipsMgr.Instance.ShowSystemTips("登录失败！！");
        }

    }

    /// <summary>
    /// 处理服务端返回回来的注册的结果
    /// </summary>
    /// <param name="data"></param>
    private void OnRegistHandle(ByteString data)
    {
        RegistRet ret = RegistRet.Parser.ParseFrom(data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {

            Debug.Log("注册成功...");
            TipsMgr.Instance.ShowSystemTips("注册成功..请登录！！");
            ShowWindow(WindowType.LoginWindow);

        }
        else
        {
            Debug.Log("注册失败..." + ret.ToString());
            TipsMgr.Instance.ShowSystemTips("注册失败..");
        }


    }
}
