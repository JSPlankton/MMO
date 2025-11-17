using cfg;
using Google.Protobuf;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Game_RoleCtrlFun : Singleton<Game_RoleCtrlFun>
{
    /*
    internal void RoleBuyItem(ServerBase serverBase, BasePackage basePackage, RoleBuyItemReq req)
    {

        if (req == null) { return; }

        RoleBuyItemRet ret = new RoleBuyItemRet();

        //1.获取当前购买商品的角色的信息
        OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(req.RoleId);
        if (onlineRole != null)
        {
            //2.获取物品数据
            ItemInfo itemInfo = LubanMgr.Instance.GetItemInfoById(req.ItemId);
            if (itemInfo != null)
            {
                //3.判断角色的游戏币是否足够
                if (onlineRole.MRInfo.Money > itemInfo.Price * req.Count)
                {
                    //4.找到一个空的背包格子对象
                    RoleKnapsackSlot emptySlot = GetEmptyKnapsackSlot(onlineRole.KnapsackInfo);
                    if (emptySlot != null)     //购买商品成功
                    {
                        emptySlot.Count = req.Count;
                        emptySlot.ItemId = itemInfo.Id;

                        onlineRole.MRInfo.Money -= itemInfo.Price * req.Count;

                        ret.Money = onlineRole.MRInfo.Money;
                        ret.KnapsackSlot = emptySlot;

                    }
                    else
                    {
                        ret.CmdCode = CmdCode.KnapsackFull; //背包已满
                    }
                }
                else
                {
                    ret.CmdCode = CmdCode.MoneyDeficit;//游戏币不足
                }
            }
            else
            {
                ret.CmdCode = CmdCode.ReqParamError;//请求参数错误
            }
        }
        else
        {
            ret.CmdCode = CmdCode.RoleNotExist;//角色不存在
        }

        Session session = SessionMgr.Instance.GetSession(basePackage.GateSessionId);
        session.SendData(basePackage, NetDefine.CMD_RoleBuyItemCode, ret.ToByteString());

    }

    /// <summary>
    /// 找出一个空的背包格子对象
    /// </summary>
    /// <returns></returns>
    private RoleKnapsackSlot GetEmptyKnapsackSlot(RoleKnapsackInfoRet knapsackInfo)
    {
        for (int i = 0; i < knapsackInfo.RoleKnapsackSlotLst.Count; i++)
        {
            if (knapsackInfo.RoleKnapsackSlotLst[i].Count == 0)//找到第一个空的背包格子对象
            {
                return knapsackInfo.RoleKnapsackSlotLst[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 物品拆分
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <param name="req"></param>
    internal void RoleKnapsackItemSplit(ServerBase serverBase, BasePackage basePackage, KnapsackItemSplitReq req)
    {
        RoleKnapsackInfoRet ret = new RoleKnapsackInfoRet();

        if (req != null)
        {
            //1.找到当前玩家
            OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(req.RoleId);
            if (onlineRole != null)
            {
                //2.找到要拆分的背包格子数据
                RoleKnapsackSlot splitSlot = onlineRole.KnapsackInfo.RoleKnapsackSlotLst[req.SlotNo];
                //3.找到当前背包格子下的物品数据
                ItemInfo itemInfo = LubanMgr.Instance.GetItemInfoById(splitSlot.ItemId);
                if (itemInfo.BCanBeStacked == 1 && splitSlot.Count >= 2)
                {
                    //4.拆分物品

                    //找出一个空的背包格子
                    RoleKnapsackSlot emptySlot = GetEmptyKnapsackSlot(onlineRole.KnapsackInfo);
                    if (emptySlot != null)
                    {
                        emptySlot.Count = 1;
                        emptySlot.ItemId = splitSlot.ItemId;

                        splitSlot.Count -= 1;

                        ret.RoleKnapsackSlotLst.Add(emptySlot);
                        ret.RoleKnapsackSlotLst.Add(splitSlot);

                    }
                    else
                    {
                        ret.CmdCode = CmdCode.KnapsackFull;
                    }
                }
                else
                {
                    ret.CmdCode = CmdCode.ReqParamError;
                }
            }
            else
            {
                ret.CmdCode = CmdCode.RoleNotExist;
            }
        }
        else
        {
            ret.CmdCode = CmdCode.ReqParamError;
        }


        Session session = SessionMgr.Instance.GetSession(basePackage.GateSessionId);
        session.SendData(basePackage, NetDefine.CMD_RoleKnapsackItemSplitCode, ret.ToByteString());


    }

    /// <summary>
    /// 角色移动背包格子
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <param name="req"></param>
    internal void RoleKnapsackSlotMove(ServerBase serverBase, BasePackage basePackage, KnapsackSlotMoveReq req)
    {
        RoleKnapsackInfoRet ret = new RoleKnapsackInfoRet();

        if (req != null)
        {
            //1.获取当前角色信息
            OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(req.RoleId);
            if (onlineRole != null)
            {
                //2.获取背包格子
                RoleKnapsackSlot dragSlot = onlineRole.KnapsackInfo.RoleKnapsackSlotLst[req.DragSlotNo];
                RoleKnapsackSlot targetSlot = onlineRole.KnapsackInfo.RoleKnapsackSlotLst[req.TargetSlotNo];

                LogMsg.Info("onlineRole.KnapsackInfo::" + onlineRole.KnapsackInfo.ToString());
                //3.目标背包格子是否有物品数据
                if (targetSlot.Count > 0)
                {
                    //4.判断目标背包格子下的物品 是否可以叠加
                    ItemInfo itemInfo = LubanMgr.Instance.GetItemInfoById(targetSlot.ItemId);

                    if (itemInfo.BCanBeStacked == 1)//可以叠加
                    {
                        //5.判断 拖拽中的背包格子的物品 和目标背包格子的物品是同一物品
                        if (dragSlot.ItemId == targetSlot.ItemId)
                        {
                            targetSlot.Count += dragSlot.Count;
                            dragSlot.Count = 0;
                            dragSlot.ItemId = 0;
                        }
                        else
                        {
                            //交换数据
                            SwopItem(ref dragSlot, ref targetSlot);
                        }

                    }
                    else
                    {
                        //交换数据
                        SwopItem(ref dragSlot, ref targetSlot);
                    }
                }
                else
                {
                    //交换数据
                    SwopItem(ref dragSlot, ref targetSlot);
                }
                LogMsg.Info("onlineRole.KnapsackInfo::" + onlineRole.KnapsackInfo.ToString());
                ret.RoleKnapsackSlotLst.Add(dragSlot);
                ret.RoleKnapsackSlotLst.Add(targetSlot);
            }
            else
            {
                ret.CmdCode = CmdCode.RoleNotExist;
            }
        }
        else
        {
            ret.CmdCode = CmdCode.ReqParamError;
        }

        Session session = SessionMgr.Instance.GetSession(basePackage.GateSessionId);
        session.SendData(basePackage, NetDefine.CMD_RoleKnapsackSlotMoveCode, ret.ToByteString());

    }

    /// <summary>
    /// 交换数据
    /// </summary>
    /// <param name="dragSlot"></param>
    /// <param name="targetSlot"></param>

    private void SwopItem(ref RoleKnapsackSlot dragSlot, ref RoleKnapsackSlot targetSlot)
    {
        int tempCount = dragSlot.Count;
        int tempItemId = dragSlot.ItemId;

        dragSlot.Count = targetSlot.Count;
        dragSlot.ItemId = targetSlot.ItemId;

        targetSlot.Count = tempCount;
        targetSlot.ItemId = tempItemId;

    }

    /// <summary>
    /// 背包整理
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <param name="req"></param>
    internal void RoleKnapsackClearUp(ServerBase serverBase, BasePackage basePackage, KnapsackClearUpReq req)
    {

        RoleKnapsackInfoRet ret = new RoleKnapsackInfoRet();

        if (req != null)
        {

            //1.获取到当前的玩家数据
            OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(req.RoleId);
            if (onlineRole != null)
            {
                //2.获取到当前玩家的背包数据，进行整理
                RepeatedField<RoleKnapsackSlot> roleKnapsackSlots = onlineRole.KnapsackInfo.RoleKnapsackSlotLst;
                //3.循环当前角色的背包格子数据，进行比较或者合并
                for (int i = 0; i < roleKnapsackSlots.Count; i++)
                {
                    RoleKnapsackSlot currSlot = roleKnapsackSlots[i];
                    if (currSlot.Count > 0)
                    {
                        //要比较的所有背包格子对象
                        for (int j = i + 1; j < roleKnapsackSlots.Count; j++)
                        {
                            RoleKnapsackSlot compareSlot = roleKnapsackSlots[j];
                            if (compareSlot.Count > 0)
                            {
                                //获取到当前背包格子和要比较的背包格子里面的物品数据， 进行判断
                                ItemInfo currItem = LubanMgr.Instance.GetItemInfoById(currSlot.ItemId);
                                ItemInfo compareItem = LubanMgr.Instance.GetItemInfoById(compareSlot.ItemId);

                                //判断当前背包格子的数据和要比较的背包格子数据 是否是同一物品，并且是可以叠加
                                if (currSlot.ItemId == compareSlot.ItemId && currItem.BCanBeStacked == 1)
                                {
                                    //合并
                                    currSlot.Count += compareSlot.Count;
                                    compareSlot.Count = 0;
                                    compareSlot.ItemId = 0;
                                }
                                else
                                {
                                    //进行比较
                                    if (CompareItem(currItem, compareItem))
                                    {
                                        //物品交换
                                        SwopItem(ref currSlot, ref compareSlot);
                                    }
                                }
                            }
                        }

                        //判断当前背包格子对象的前面一个格子是否是空格子，  找出一个有序的空背包格子对象来存放该物品
                        if (i > 0 && roleKnapsackSlots[i - 1].Count == 0)
                        {

                            RoleKnapsackSlot emptySlot = GetEmptyKnapsackSlot(onlineRole.KnapsackInfo);
                            if (emptySlot != null)
                            {
                                //物品交换
                                SwopItem(ref currSlot, ref emptySlot);
                            }
                        }
                    }
                }
                //返回当前角色的背包数据
                ret = onlineRole.KnapsackInfo;
            }
            else
            {
                ret.CmdCode = CmdCode.RoleNotExist;
            }
        }
        else
        {
            ret.CmdCode = CmdCode.ReqParamError;
        }

        Session session = SessionMgr.Instance.GetSession(basePackage.GateSessionId);
        session.SendData(basePackage, NetDefine.CMD_RoleKnapsackClearUpCode, ret.ToByteString());
    }

    private bool CompareItem(ItemInfo currItem, ItemInfo compareItem)
    {

        //1.判断2个物品的类型
        if (currItem.Type < 10 && compareItem.Type >= 10)
        {
            return true;
        }

        if (currItem.Type >= 10 && compareItem.Type < 10)
        {
            return false;
        }

        //2.判断2个物品的等级
        if (currItem.Grade < compareItem.Grade)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 穿戴装备处理逻辑
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <param name="req"></param>
    internal void RolePutOnEquip(ServerBase serverBase, BasePackage basePackage, PutOnEquipReq req)
    {

        PutOnEquipRet ret = new PutOnEquipRet();

        if (req != null)
        {
            //获取到当前角色的信息数据
            OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(req.RoleId);
            if (onlineRole != null)
            {
                //获取要穿戴的装备格子数据， 以及 拖拽过来的背包格子数据
                RoleWearEquipSlot wearEquipSlot = onlineRole.WearEquipInfo.RoleWearEquipLst[req.WearEquipSlotNo];
                RoleKnapsackSlot knapsackSlot = onlineRole.KnapsackInfo.RoleKnapsackSlotLst[req.KnapsackSlotNo];

                //获取拖拽中背包格子的物品数据
                ItemInfo knaspackItem = LubanMgr.Instance.GetItemInfoById(knapsackSlot.ItemId);
                if (knaspackItem != null && wearEquipSlot.Type == knaspackItem.Type)
                {
                    ItemInfo equipItem = LubanMgr.Instance.GetItemInfoById(wearEquipSlot.ItemId);
                    if (equipItem == null)//当前装备格子下没有穿戴装备
                    {
                        equipItem = new ItemInfo();
                        wearEquipSlot.ItemId = knapsackSlot.ItemId;
                        knapsackSlot.Count = 0;
                        knapsackSlot.ItemId = 0;
                    }
                    else//已经穿戴了装备
                    {
                        //交换数据
                        int tempItemId = wearEquipSlot.ItemId;
                        wearEquipSlot.ItemId = knapsackSlot.ItemId;
                        knapsackSlot.ItemId = tempItemId;
                    }
                    LogMsg.Info("onlineRole1::" + onlineRole.MRInfo);
                    //更新当前角色的属性信息
                    UpdateRoleAttrInfo(onlineRole, knaspackItem, equipItem);
                    LogMsg.Info("onlineRole2::" + onlineRole.MRInfo);

                    ret.WearEquipSlot = wearEquipSlot;
                    ret.KnapsackSlot = knapsackSlot;
                    ret.BaseInfo = onlineRole.MRInfo.BaseInfo;
                }
                else
                {
                    ret.CmdCode = CmdCode.ReqParamError;
                }
            }
            else
            {
                ret.CmdCode = CmdCode.RoleNotExist;
            }
        }
        else
        {
            ret.CmdCode = CmdCode.ReqParamError;
        }

        Session session = SessionMgr.Instance.GetSession(basePackage.GateSessionId);
        session.SendData(basePackage, NetDefine.CMD_RolePutOnEquipCode, ret.ToByteString());
    }

    /// <summary>
    /// 更新角色属性信息
    /// </summary>
    /// <param name="onlineRole"></param>
    /// <param name="knaspackItem"></param>
    /// <param name="equipItem"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void UpdateRoleAttrInfo(OnlineRole onlineRole, ItemInfo knaspackItem, ItemInfo equipItem)
    {
        onlineRole.MRInfo.BaseInfo.MaxHp += (knaspackItem.HP - equipItem.HP);
        onlineRole.MRInfo.BaseInfo.CurrHp += (knaspackItem.HP - equipItem.HP);

        onlineRole.MRInfo.BaseInfo.MaxMp += (knaspackItem.MP - equipItem.MP);
        onlineRole.MRInfo.BaseInfo.CurrMp += (knaspackItem.MP - equipItem.MP);

        onlineRole.MRInfo.BaseInfo.Akt += (knaspackItem.Atk - equipItem.Atk);
        onlineRole.MRInfo.BaseInfo.Def += (knaspackItem.Def - equipItem.Def);
        onlineRole.MRInfo.BaseInfo.Crit += (knaspackItem.Crit - equipItem.Crit);
        onlineRole.MRInfo.BaseInfo.Dodge += (knaspackItem.Dodeg - equipItem.Dodeg);
        onlineRole.MRInfo.BaseInfo.Hit += (knaspackItem.Hit - equipItem.Hit);
        onlineRole.MRInfo.BaseInfo.Penet += (knaspackItem.Penet - equipItem.Penet);
    }

    /// <summary>
    /// 卸载装备逻辑处理
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <param name="req"></param>
    /// <exception cref="NotImplementedException"></exception>
    internal void RoleUnloadEquip(ServerBase serverBase, BasePackage basePackage, UnloadEquipReq req)
    {
        UnloadEquipRet ret = new UnloadEquipRet();


        if (req != null)
        {
            //获取到当前角色的信息数据
            OnlineRole onlineRole = Game_Global.Instance.GetOnlineRoleById(req.RoleId);
            if (onlineRole != null)
            {
                //获取要穿戴的装备格子数据， 以及 拖拽过来的背包格子数据
                RoleWearEquipSlot wearEquipSlot = onlineRole.WearEquipInfo.RoleWearEquipLst[req.WearEquipSlotNo];
                RoleKnapsackSlot knapsackSlot = onlineRole.KnapsackInfo.RoleKnapsackSlotLst[req.KnapsackSlotNo];

                if (knapsackSlot.Count > 0)
                {
                    knapsackSlot = GetEmptyKnapsackSlot(onlineRole.KnapsackInfo);
                }

                if (knapsackSlot != null)
                {
                    ItemInfo equipItem = LubanMgr.Instance.GetItemInfoById(wearEquipSlot.ItemId);

                    knapsackSlot.ItemId = wearEquipSlot.ItemId;
                    knapsackSlot.Count = 1;

                    wearEquipSlot.ItemId = 0;

                    //更新当前角色的属性信息
                    UpdateRoleAttrInfo(onlineRole, new ItemInfo(), equipItem);


                    ret.WearEquipSlot = wearEquipSlot;
                    ret.KnapsackSlot = knapsackSlot;
                    ret.BaseInfo = onlineRole.MRInfo.BaseInfo;
                }
                else
                {
                    ret.CmdCode = CmdCode.KnapsackFull;
                }
            }
            else
            {
                ret.CmdCode = CmdCode.RoleNotExist;
            }
        }
        else
        {
            ret.CmdCode = CmdCode.ReqParamError;
        }

        Session session = SessionMgr.Instance.GetSession(basePackage.GateSessionId);
        session.SendData(basePackage, NetDefine.CMD_RoleUnloadEquipCode, ret.ToByteString());

    }

    */
}
