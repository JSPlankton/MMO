using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Game_RoleCtrl : IContainer
{
    /// <summary>
    ///游戏逻辑服务器，作为客户端时，接收到中心服务器发来的数据
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    public void OnClientCommand(ServerBase serverBase, BasePackage basePackage)
    {

        Session session = SessionMgr.Instance.GetSession(basePackage.GateSessionId);


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
            case NetDefine.CMD_EnterWroldCode:
                OnEnterWroldResultHandle(session, basePackage);
                break;
        }

    }

    /// <summary>
    /// 角色进入游戏世界返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnEnterWroldResultHandle(Session session, BasePackage basePackage)
    {
        MainRoleInfo mrInfo = MainRoleInfo.Parser.ParseFrom(basePackage.Data);
        if (mrInfo != null)
        {
            ////保存当前角色信息数据了..
            //OnlineRole onlineRole = new OnlineRole()
            //{
            //    UnitySessionId = basePackage.UnitySessionId,
            //    GateSessionId = basePackage.GateSessionId,
            //    MRInfo = mrInfo,
            //};
            ////存储当前玩家的数据到服务端
            //Game_Global.Instance.AddOnlineRole(mrInfo.BaseInfo.RoleId, onlineRole);

            ////1.把当前玩家的数据  同步给其他在线玩家
            //Game_WorldBC.Instance.RoleEnterWorldBC(mrInfo);
            ////2.把其他在线玩家的数据，同步给当前玩家
            //Game_WorldBC.Instance.OtherOnlineRoleBC(session, basePackage, mrInfo);
        }
    }

    /// <summary>
    /// 返回的角色穿戴的装备信息数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleWearEquipInfoResultHandle(Session session, BasePackage basePackage)
    {
        RoleWearEquipInfoRet ret = RoleWearEquipInfoRet.Parser.ParseFrom(basePackage.Data);

        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            //缓存角色穿戴的装备信息数据
            //OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(ret.RoleId);
            //if (onlineRole != null)
            //{
            //    onlineRole.WearEquipInfo = ret;
            //}
        }

        LogMsg.Info("OnRoleWearEquipInfoResultHandle::" + ret.ToString());
        //把数据发送到网关服务器。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 返回角色的背包数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnRoleKnapsackInfoResultHandle(Session session, BasePackage basePackage)
    {
        RoleKnapsackInfoRet ret = RoleKnapsackInfoRet.Parser.ParseFrom(basePackage.Data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            //缓存角色的背包信息数据
            //OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(ret.RoleId);
            //if (onlineRole != null)
            //{
            //    onlineRole.KnapsackInfo = ret;
            //}
        }
        LogMsg.Info("OnRoleKnapsackInfoResultHandle::" + ret.ToString());
        //把数据发送到网关服务器。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 角色技能信息返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnRoleSkillInfoResultHandle(Session session, BasePackage basePackage)
    {
        RoleSkillInfoRet ret = RoleSkillInfoRet.Parser.ParseFrom(basePackage.Data);
        if (ret != null && ret.CmdCode == CmdCode.Succeed)
        {
            //缓存角色的技能信息数据
            //OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(ret.RoleId);
            //if (onlineRole != null)
            //{
            //    onlineRole.SkillInfo = ret;
            //}
        }
        LogMsg.Info("OnRoleSkillInfoResultHandle::" + ret.ToString());
        //把数据发送到网关服务器。
        session.SendData(basePackage);

    }

    public void OnInit()
    {
    }

    /// <summary>
    /// 游戏逻辑务器，作为服务端时，接收到网关服务器发来的数据
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

        LogMsg.Info("OnRoleChangeStateHandle::" + req.ToString());

        //获取到当前角色的信息数据
        if (req != null)
        {
            //OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(req.RoleId);
            //if (onlineRole != null)
            //{
            //    //更新当前角色的状态信息 TODO
            //    //onlineRole.MRInfo.BaseInfo.Pos = $"{req.PosX}_{req.PosY}_{req.PosZ}_{req.RotateY}";

            //    //把当前玩家的状态信息数据， 同步给附近的其他在线玩家
            //    Game_WorldBC.Instance.RoleChangeStateWorldBC(onlineRole.MRInfo, req);

            //}
        }
    }

    /// <summary>
    /// 角色移动请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleMoveHandle(ServerBase serverBase, BasePackage basePackage)
    {
        RoleMoveReq req = RoleMoveReq.Parser.ParseFrom(basePackage.Data);

        LogMsg.Info("OnRoleMoveHandle::" + req.ToString());

        //获取到当前角色的信息数据
        if (req != null)
        {
            //OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(req.RoleId);
            //if (onlineRole != null)
            //{
            //    //更新当前角色的位置信息
            //    onlineRole.MRInfo.BaseInfo.Pos = $"{req.PosX}_{req.PosY}_{req.PosZ}_{req.RotateY}";

            //    //把当前玩家的位置信息数据， 同步给附近的其他玩家
            //    Game_WorldBC.Instance.RoleMoveWorldBC(onlineRole.MRInfo, req);

            //}
        }

    }

    /// <summary>
    /// 卸载装备请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleUnloadEquipHandle(ServerBase serverBase, BasePackage basePackage)
    {
        UnloadEquipReq req = UnloadEquipReq.Parser.ParseFrom(basePackage.Data);

        LogMsg.Info("OnRoleUnloadEquipHandle::" + req.ToString());

        //卸载装备的逻辑
        //Game_RoleCtrlFun.Instance.RoleUnloadEquip(serverBase, basePackage, req);
    }

    /// <summary>
    /// 穿戴装备请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRolePutOnEquipHandle(ServerBase serverBase, BasePackage basePackage)
    {
        PutOnEquipReq req = PutOnEquipReq.Parser.ParseFrom(basePackage.Data);

        LogMsg.Info("OnRolePutOnEquipHandle::" + req.ToString());

        //穿戴装备逻辑
        //Game_RoleCtrlFun.Instance.RolePutOnEquip(serverBase, basePackage, req);
    }

    /// <summary>
    /// 整理背包的请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackClearUpHandle(ServerBase serverBase, BasePackage basePackage)
    {
        KnapsackClearUpReq req = KnapsackClearUpReq.Parser.ParseFrom(basePackage.Data);

        LogMsg.Info("OnRoleKnapsackClearUpHandle::" + req.ToString());

        //整理背包的逻辑
        //Game_RoleCtrlFun.Instance.RoleKnapsackClearUp(serverBase, basePackage, req);
    }

    /// <summary>
    /// 物品拆分请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackItemSplitHandle(ServerBase serverBase, BasePackage basePackage)
    {
        KnapsackItemSplitReq req = KnapsackItemSplitReq.Parser.ParseFrom(basePackage.Data);

        LogMsg.Info("OnRoleKnapsackItemSplitHandle::" + req.ToString());

        //物品拆分逻辑
        //Game_RoleCtrlFun.Instance.RoleKnapsackItemSplit(serverBase, basePackage, req);
    }

    /// <summary>
    /// 角色移动背包格子请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleKnapsackSlotMoveHandle(ServerBase serverBase, BasePackage basePackage)
    {
        KnapsackSlotMoveReq req = KnapsackSlotMoveReq.Parser.ParseFrom(basePackage.Data);

        LogMsg.Info("OnRoleKnapsackSlotMoveHandle::" + req.ToString());

        //物品移动逻辑
        //Game_RoleCtrlFun.Instance.RoleKnapsackSlotMove(serverBase, basePackage, req);
    }

    /// <summary>
    /// 角色请求购买商品
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRoleBuyItemHandle(ServerBase serverBase, BasePackage basePackage)
    {
        RoleBuyItemReq req = RoleBuyItemReq.Parser.ParseFrom(basePackage.Data);

        LogMsg.Info("OnRoleBuyItemHandle::" + req.ToString());

        //购买商品逻辑
        //Game_RoleCtrlFun.Instance.RoleBuyItem(serverBase, basePackage, req);

    }

    /// <summary>
    /// 角色请求进入游戏世界 
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnEnterWroldHandle(ServerBase serverBase, BasePackage basePackage)
    {
        EnterWroldReq req = EnterWroldReq.Parser.ParseFrom(basePackage.Data);

        //就要把收到的数据发送给中心服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnEnterWroldHandle::" + req.ToString());
    }
}
