using Google.Protobuf;
using System;
using TMPro;
using UnityEngine;

/**
 * Title: 创建角色Window
 * Description:目前只有一个角色，所以创建的角色的职业默认是剑修
 */


public class CreateRoleWindow : WindowBase
{


    [SerializeField, Header("昵称输入框")] private TMP_InputField _iptNickname;

    public Action<string> CreateRoleBtnClickAction;

    public void OnCreateRoleBtnClicked()
    {

        //判断输入框.
        if (string.IsNullOrEmpty(_iptNickname.text))
        {
            TipsMgr.Instance.ShowSystemTips("请输入昵称...");
            return;
        }

        //验证昵称的合法性  todo

        CreateRoleBtnClickAction?.Invoke(_iptNickname.text);

    }

}