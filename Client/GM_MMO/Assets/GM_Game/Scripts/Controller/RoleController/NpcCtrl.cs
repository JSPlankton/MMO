using log4net.Util;
using System;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class NpcCtrl : RoleCtrlBase
{
    internal void OpenTalk(RoleCtrlBase mainRole)
    {

        //如果角色和NPC的距离过远，就弹框提示
        if (Vector3.Distance(transform.position, mainRole.transform.position) > 10)
        {

            TipsMgr.Instance.ShowSystemTips("距离过远，请靠近些..");
            return;
        }


        //朝向角色
        transform.LookAtTarget(mainRole.transform);

        //显示TalkWindow
        UIRoot.Instance.MainViewCtrl.ShowMainWindow(WindowType.TalkWindow);

    }
}
