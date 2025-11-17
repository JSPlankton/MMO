using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Center_RoleCtrl : IContainer
{

    private Center_RoleModel _roleModel;

    public Center_RoleCtrl(Center_RoleModel roleModel)
    {
        _roleModel = roleModel;
    }


    public void OnClientCommand(ServerBase serverBase, BasePackage basePackage)
    {
    }

    public void OnInit()
    {
    }

    /// <summary>
    /// 中心服务器，接收到游戏服务器发来的数据请求
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
        }

    }

    /// <summary>
    /// 角色进入游戏世界请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnEnterWroldHandle(ServerBase serverBase, BasePackage basePackage)
    {
        EnterWroldReq req = EnterWroldReq.Parser.ParseFrom(basePackage.Data);
        LogMsg.Info("OnEnterWroldHandle::req" + req.ToString());

        //MainRoleInfo mrInfo = Center_Global.Instance.GetRoleById(req.RoleId);
        //if (mrInfo != null)
        //{

            //1.返回角色的技能信息
            RoleSkillInfoRet ret = _roleModel.RoleSkillInfo(req);
            LogMsg.Info("OnEnterWroldHandle::ret" + ret.ToString());
            serverBase.SendData(basePackage, NetDefine.CMD_RoleSkillInfoCode, ret.ToByteString());

            //    //2.返回角色的背包数据
            //    RoleKnapsackInfoRet knapsackInfoRet = _roleModel.RoleKnapsackInfo(req);
            //    LogMsg.Info("OnEnterWroldHandle::knapsackInfoRet" + knapsackInfoRet.ToString());
            //    serverBase.SendData(basePackage, NetDefine.CMD_RoleKnapsackInfoCode, knapsackInfoRet.ToByteString());

            //    //3.返回角色穿戴的装备信息
            //    RoleWearEquipInfoRet roleWearEquipInfoRet = _roleModel.RoleWearEquipInfo(req);
            //    LogMsg.Info("OnEnterWroldHandle::roleWearEquipInfoRet" + roleWearEquipInfoRet.ToString());
            //    serverBase.SendData(basePackage, NetDefine.CMD_RoleWearEquipInfoCode, roleWearEquipInfoRet.ToByteString());

            //    //返回当前角色的信息数据到游戏服务器， 然后在游戏服务器，把这个角色的信息数据存储起来，就可以进行同步或者其他操作了。
            //    serverBase.SendData(basePackage, NetDefine.CMD_EnterWroldCode, mrInfo.ToByteString());
        //}

    }



}
