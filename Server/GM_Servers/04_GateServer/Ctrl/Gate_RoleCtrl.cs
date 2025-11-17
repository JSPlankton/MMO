using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Gate_RoleCtrl : IContainer
{
    /// <summary>
    /// 网关服务器，作为客户端时，接收到游戏逻辑服务器发来的数据
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    public void OnClientCommand(ServerBase serverBase, BasePackage basePackage)
    {

        Session session = SessionMgr.Instance.GetSession(basePackage.UnitySessionId);


        switch (basePackage.ProtoCode)
        {
            case NetDefine.CMD_RoleSkillInfoCode:
                OnRoleSkillInfoResultHandle(session, basePackage);
                break;

            case NetDefine.CMD_RoleKnapsackInfoCode:
                OnRoleKnapsackInfoResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_RoleWearEquipInfoCode:
                OnRoleWearEquipInfoResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_RoleBuyItemCode:
                OnRoleBuyItemResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_RoleKnapsackSlotMoveCode:
                OnRoleKnapsackSlotMoveResultHandle(session, basePackage);
                break;

            case NetDefine.CMD_RoleKnapsackItemSplitCode:
                OnRoleKnapsackItemSplitResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_RoleKnapsackClearUpCode:
                OnRoleKnapsackClearUpResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_RolePutOnEquipCode:
                OnRolePutOnEquipResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_RoleUnloadEquipCode:
                OnRoleUnloadEquipResultHandle(session, basePackage);
                break;
        }

    }

    /// <summary>
    /// 卸载装备返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleUnloadEquipResultHandle(Session session, BasePackage basePackage)
    {
        UnloadEquipRet ret = UnloadEquipRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRoleUnloadEquipResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 穿戴装备返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRolePutOnEquipResultHandle(Session session, BasePackage basePackage)
    {
        PutOnEquipRet ret = PutOnEquipRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRolePutOnEquipResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 整理背包返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackClearUpResultHandle(Session session, BasePackage basePackage)
    {
        RoleKnapsackInfoRet ret = RoleKnapsackInfoRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRoleKnapsackClearUpResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 物品拆分返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackItemSplitResultHandle(Session session, BasePackage basePackage)
    {
        RoleKnapsackInfoRet ret = RoleKnapsackInfoRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRoleKnapsackItemSplitResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 角色移动背包格子返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackSlotMoveResultHandle(Session session, BasePackage basePackage)
    {
        RoleKnapsackInfoRet ret = RoleKnapsackInfoRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRoleKnapsackSlotMoveResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 角色购买物品返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleBuyItemResultHandle(Session session, BasePackage basePackage)
    {
        RoleBuyItemRet ret = RoleBuyItemRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRoleBuyItemResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }


    /// <summary>
    /// 返回角色穿戴的装备信息数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleWearEquipInfoResultHandle(Session session, BasePackage basePackage)
    {
        RoleWearEquipInfoRet ret = RoleWearEquipInfoRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRoleWearEquipInfoResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 返回角色背包数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackInfoResultHandle(Session session, BasePackage basePackage)
    {
        RoleKnapsackInfoRet ret = RoleKnapsackInfoRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRoleKnapsackInfoResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }


    private void OnRoleSkillInfoResultHandle(Session session, BasePackage basePackage)
    {
        RoleSkillInfoRet ret = RoleSkillInfoRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnRoleSkillInfoResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    public void OnInit()
    {
    }

    /// <summary>
    /// 网关服务器，未作服务端时，接收到Unity端用户发来的数据
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    public void OnServerCommand(ServerBase serverBase, BasePackage basePackage)
    {
        switch (basePackage.ProtoCode)
        {
            case NetDefine.CMD_EnterWroldCode:
                OnEnterWroldHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_RoleBuyItemCode:
                OnRoleBuyItemHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_RoleKnapsackSlotMoveCode:
                OnRoleKnapsackSlotMoveHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_RoleKnapsackItemSplitCode:
                OnRoleKnapsackItemSplitHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_RoleKnapsackClearUpCode:
                OnRoleKnapsackClearUpHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_RolePutOnEquipCode:
                OnRolePutOnEquipHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_RoleUnloadEquipCode:
                OnRoleUnloadEquipHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_RoleMoveCode:
                OnRoleMoveHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_RoleChangeStateCode:
                OnRoleChangeStateHandle(serverBase, basePackage);
                break;
        }
    }

    /// <summary>
    /// 角色改变状态请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleChangeStateHandle(ServerBase serverBase, BasePackage basePackage)
    {
        RoleChangeStateReq req = RoleChangeStateReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnRoleChangeStateHandle::" + req.ToString());
    }

    /// <summary>
    /// 角色移动请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleMoveHandle(ServerBase serverBase, BasePackage basePackage)
    {
        RoleMoveReq req = RoleMoveReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnRoleMoveHandle::" + req.ToString());
    }

    /// <summary>
    /// 卸载装备请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleUnloadEquipHandle(ServerBase serverBase, BasePackage basePackage)
    {
        UnloadEquipReq req = UnloadEquipReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnRoleUnloadEquipHandle::" + req.ToString());
    }

    /// <summary>
    /// 穿戴装备请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRolePutOnEquipHandle(ServerBase serverBase, BasePackage basePackage)
    {
        PutOnEquipReq req = PutOnEquipReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnRolePutOnEquipHandle::" + req.ToString());
    }

    /// <summary>
    /// 整理背包请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackClearUpHandle(ServerBase serverBase, BasePackage basePackage)
    {
        KnapsackClearUpReq req = KnapsackClearUpReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnRoleKnapsackClearUpHandle::" + req.ToString());
    }

    /// <summary>
    /// 背包中物品的拆分请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackItemSplitHandle(ServerBase serverBase, BasePackage basePackage)
    {
        KnapsackItemSplitReq req = KnapsackItemSplitReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnRoleKnapsackItemSplitHandle::" + req.ToString());
    }

    /// <summary>
    /// 角色请求移动背包格子
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackSlotMoveHandle(ServerBase serverBase, BasePackage basePackage)
    {
        KnapsackSlotMoveReq req = KnapsackSlotMoveReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnRoleKnapsackSlotMoveHandle::" + req.ToString());
    }

    /// <summary>
    /// 角色购买商品请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleBuyItemHandle(ServerBase serverBase, BasePackage basePackage)
    {
        RoleBuyItemReq req = RoleBuyItemReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnRoleBuyItemHandle::" + req.ToString());
    }

    /// <summary>
    /// 角色进入游戏世界请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnEnterWroldHandle(ServerBase serverBase, BasePackage basePackage)
    {
        EnterWroldReq req = EnterWroldReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证数据的合法性

        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnEnterWroldHandle::" + req.ToString());
    }
}
