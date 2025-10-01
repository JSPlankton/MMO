using System;
using UnityEngine;

/**
 * Title:������ɫ��ѡ���ɫ
 * Description:
 */


public class CreateRoleView : UIBase
{

    [SerializeField, Header("������ɫWindow")] private CreateRoleWindow _createRoleWindow;
    [SerializeField, Header("ѡ���ɫWindow")] private SelectRoleWindow _selectRoleWindow;

    public override void InitView()
    {
        base.InitView();

        windowDic.Add(WindowType.CreateRoleWindow, _createRoleWindow);
        windowDic.Add(WindowType.SelectRoleWindow, _selectRoleWindow);

    }

    public void RegistCreateRoleBtnClicked(Action<string> action)
    {
        _createRoleWindow.CreateRoleBtnClickAction = action;
    }


}
