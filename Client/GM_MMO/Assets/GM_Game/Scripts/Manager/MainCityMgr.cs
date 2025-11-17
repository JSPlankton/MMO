using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using UnityEngine;

public class MainCityMgr : MonoSingleton<MainCityMgr>
{

    private MainRoleInfo _mrInfo;

    protected override void OnAwake()
    {
        base.OnAwake();

        _mrInfo = Global.Instance.MRInfo;
    }

    protected override void OnStart()
    {
        base.OnStart();
        
        //创建主角
        CreateRole(RoleType.MainRole, _mrInfo.BaseInfo, "Role/Role_JX");

        //2.向服务端发送  主角已经进入游戏世界，  把自己的数据发送给附近的其他玩家， 其他玩家的数据发送给自己，
        //再把角色的技能相关的数据返回回来
        //再把角色的背包相关的数据返回回来
        //再把角色穿戴的装备信息返回回来
        //

        EnterWroldReq req = new EnterWroldReq()
        {
            RoleId = _mrInfo.BaseInfo.RoleId,
        };
        NetSocketMgr.Client.SendData(NetDefine.CMD_EnterWroldCode, req.ToByteString());
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="mainRole"></param>
    /// <param name="mrInfoBaseInfo"></param>
    /// <param name="roleRoleJ"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void CreateRole(RoleType roleType, RoleBaseInfo baseInfo, string prefabPath)
    {
        ResourceMgr.Instance.LoadPrefabAsync(prefabPath, role =>
        {
            if (role == null)
            {
                return;
            }

            role.transform.localPosition = new Vector3(74.9700012f, 19.9829998f, 82.9400024f);
            RoleCtrlBase roleCtrl = role.GetComponent<RoleCtrlBase>();
            if (roleCtrl != null)
            {
                roleCtrl.InitCtrl(roleType, baseInfo);
            }
        });
    }
}
