using UnityEngine;
using cfg;
using Google.Protobuf;
using Google.Protobuf.Collections;
using System;
using UnityEngine;


public class MainCtrl : CtrlBase
{

    private MainView _mainView;

    public MainCtrl(UIBase view) : base(view)
    {

        _mainView = view as MainView;
        _mainView.InitView();

        RegistCommand();
    }

    private void RegistCommand()
    {
        SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RoleSkillInfoCode, OnRoleSkillInfoHandle);
        // SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RoleKnapsackInfoCode, OnRoleKnapsackInfoHandle);
        // SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RoleWearEquipInfoCode, OnRoleWearEquipInfoHandle);
        // SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RoleBuyItemCode, OnRoleBuyItemHandle);
        // SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RoleKnapsackSlotMoveCode, OnRoleKnapsackSlotMoveHandle);
        // SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RoleKnapsackItemSplitCode, OnRoleKnapsackSlotMoveHandle);
        // SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RoleKnapsackClearUpCode, OnRoleKnapsackSlotMoveHandle);
        // SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RolePutOnEquipCode, OnRolePutOnEquipHandle);
        // SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_RoleUnloadEquipCode, OnRoleUnloadEquipHandle);
    }
    
    /// <summary>
    /// 服务端返回的角色技能数据
    /// </summary>
    /// <param name="data"></param>
    private void OnRoleSkillInfoHandle(ByteString data)
    {
        RoleSkillInfoRet ret = RoleSkillInfoRet.Parser.ParseFrom(data);
        if (ret != null)
        {
            Debug.Log("OnRoleSkillInfoHandle::" + ret.ToString());
            //更新技能相关信息的UI
            UIRoot.Instance.MainViewCtrl.RefreshWindow(WindowType.SkillInfoWindow, ret.RoleSkillInfoLst);
            //更新技能槽相关信息的UI
            UIRoot.Instance.MainViewCtrl.RefreshWindow(WindowType.RoleCurrtInfowWindow, ret.RoleSkillInfoLst);
        }

    }
}
