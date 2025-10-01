using Google.Protobuf;
using System;
using UnityEngine;
using YooAsset;

/**
 * Title:��¼ģ�������
 * Description: ����ͼ������ʾ�����أ��Լ����ݵĲ������������
 */


public class LoginCtrl : CtrlBase
{

    private LoginView _loginView;


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

        //ע�����¼�

        _loginView.RegistGameServerBtnClicked(OnGameServerBtnClicked);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    private void OnGameServerBtnClicked(GameServer server)
    {
        LoginGameServerReq req = new LoginGameServerReq()
        {
            AccountId = Global.Instance.LoginInfo.AccountId,
            GameServerId = server.ServerId,
        };

        NetSocketMgr.Client.SendData(NetDefine.CMD_LoginGameServerCode, req.ToByteString());
    }

    /// <summary>
    /// ��¼��Ϸ��������������
    /// </summary>
    /// <param name="data"></param>
    private void OnLoginGameServerHandle(ByteString data)
    {

        LoginGameServerRet ret = LoginGameServerRet.Parser.ParseFrom(data);

        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            Debug.Log("��¼��Ϸ������::" + ret.ToString());

            Global.Instance.YooPackage.LoadSceneAsync("Assets/GM_Game/Scenes/Scene_CreateRole")
                .Completed += (SceneOperationHandle handle) =>
                {
                    UIRoot.Instance.LoginViewCtrl.ShowView(false);

                    if (ret.CreateRoleInfo != null)
                    {
                        //1.�Ƿ��Ѿ��н�ɫ�� �н�ɫ.. ��תѡ���ɫ��UI��
                        UIRoot.Instance.CreateRoleViewCtrl.ShowWindow(WindowType.SelectRoleWindow, ret.CreateRoleInfo);
                    }
                    else
                    {
                        //2.�����δ������ɫ����ô��ת������ɫUI����
                        UIRoot.Instance.CreateRoleViewCtrl.ShowWindow(WindowType.CreateRoleWindow);
                    }
                };
        }
    }

    /// <summary>
    /// ��ȡ�������б�������
    /// </summary>
    /// <param name="data"></param>
    private void OnGetServerListHandle(ByteString data)
    {
        GetServerListRet ret = GetServerListRet.Parser.ParseFrom(data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            Debug.Log("��ȡ����������::" + ret.ToString());

            ShowWindow(WindowType.ServerListWindow, ret);
        }

    }

    /// <summary>
    /// ��¼��������������
    /// </summary>
    /// <param name="data"></param>
    private void OnLoginHandle(ByteString data)
    {

        LoginRet ret = LoginRet.Parser.ParseFrom(data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            Debug.Log("��¼�ɹ�..." + ret.ToString());
            TipsMgr.Instance.ShowSystemTips("��¼�ɹ�����");
            Global.Instance.LoginInfo = ret;
            ShowWindow(WindowType.GameServerWindow, ret.GameServer);
        }
        else
        {
            Debug.Log("��¼ʧ��..." + ret.ToString());
            TipsMgr.Instance.ShowSystemTips("��¼ʧ�ܣ���");
        }

    }

    /// <summary>
    /// �������˷��ػ�����ע��Ľ��
    /// </summary>
    /// <param name="data"></param>
    private void OnRegistHandle(ByteString data)
    {
        RegistRet ret = RegistRet.Parser.ParseFrom(data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {

            Debug.Log("ע��ɹ�...");
            TipsMgr.Instance.ShowSystemTips("ע��ɹ�..���¼����");
            ShowWindow(WindowType.LoginWindow);

        }
        else
        {
            Debug.Log("ע��ʧ��..." + ret.ToString());
            TipsMgr.Instance.ShowSystemTips("ע��ʧ��..");
        }


    }
}
