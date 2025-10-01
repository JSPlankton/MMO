using Codice.Client.Common.GameUI;
using Google.Protobuf;
using System;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class CreateRoleCtrl : CtrlBase
{

    private CreateRoleView _createRoleView;

    public CreateRoleCtrl(UIBase view) : base(view)
    {
        _createRoleView = view as CreateRoleView;
        _createRoleView.InitView();

        RegistCommand();
    }

    public void RegistCommand()
    {
        SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_CreateRoleCode, OnCreateRoleHandle);


        _createRoleView.RegistCreateRoleBtnClicked(OnCreateRoleBtnClicked);
    }

    private void OnCreateRoleBtnClicked(string nickname)
    {
        CreateRoleReq req = new CreateRoleReq()
        {
            AccountId = Global.Instance.LoginInfo.AccountId,
            GameServerId = Global.Instance.LoginInfo.GameServer.ServerId,
            Nickname = nickname,
            JobId = 1, //Ĭ����1�� Ŀǰֻ�н���ְҵ
        };

        NetSocketMgr.Client.SendData(NetDefine.CMD_CreateRoleCode, req.ToByteString());
    }

    /// <summary>
    /// ������ɫ ��������
    /// </summary>
    /// <param name="data"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnCreateRoleHandle(ByteString data)
    {
        CreateRoleRet ret = CreateRoleRet.Parser.ParseFrom(data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            Debug.Log("������ɫ�ɹ�:" + ret.ToString());

            TipsMgr.Instance.ShowSystemTips("������ɫ�ɹ�..");

            ShowWindow(WindowType.SelectRoleWindow, ret);

        }
    }
}