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
        SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_StartGameCode, OnStartGameHandle);

        _createRoleView.RegistCreateRoleBtnClicked(OnCreateRoleBtnClicked);
        _createRoleView.RegistStartGameBtnClicked(OnStartGameBtnClicked);
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
    /// 开始游戏请求
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnStartGameBtnClicked(int roleId)
    {
        Debug.Log("开始游戏请求 roleId :" + roleId.ToString());
        StartGameReq req = new StartGameReq()
        {
            RoleId = roleId
        };
        NetSocketMgr.Client.SendData(NetDefine.CMD_StartGameCode, req.ToByteString());
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
    
    /// <summary>
    /// 开始游戏 返回数据
    /// </summary>
    /// <param name="data"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnStartGameHandle(ByteString data)
    {
        StartGameRet ret = StartGameRet.Parser.ParseFrom(data);
        if (ret != null)
        {
            Debug.Log("OnStartGameHandle:" + ret.ToString());
            
            // 缓存主角信息
            Global.Instance.MRInfo = ret.MainRoleInfo;

            // 加载主城场景
            SceneMgr.Instance.LoadScene(SceneType.Scene_MianCity, () =>
            {
                UIRoot.Instance.CreateRoleViewCtrl.ShowView(false);
                UIRoot.Instance.InitMainCtrl();
            });

            // 隐藏创建角色相关view

            //初始化主城先关的ui控制器
        }
    }
}
