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
            JobId = 1, //默认是1， 目前只有剑修职业
        };

        NetSocketMgr.Client.SendData(NetDefine.CMD_CreateRoleCode, req.ToByteString());
    }

    /// <summary>
    /// 创建角色 返回数据
    /// </summary>
    /// <param name="data"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnCreateRoleHandle(ByteString data)
    {
        CreateRoleRet ret = CreateRoleRet.Parser.ParseFrom(data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            Debug.Log("创建角色成功:" + ret.ToString());

            TipsMgr.Instance.ShowSystemTips("创建角色成功..");

            ShowWindow(WindowType.SelectRoleWindow, ret);

        }
    }
}