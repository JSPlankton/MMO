using Google.Protobuf;
using System;
using TMPro;
using UnityEngine;

/**
 * Title: ������ɫWindow
 * Description:Ŀǰֻ��һ����ɫ�����Դ����Ľ�ɫ��ְҵĬ���ǽ���
 */


public class CreateRoleWindow : WindowBase
{


    [SerializeField, Header("�ǳ������")] private TMP_InputField _iptNickname;

    public Action<string> CreateRoleBtnClickAction;

    public void OnCreateRoleBtnClicked()
    {

        //�ж������.
        if (string.IsNullOrEmpty(_iptNickname.text))
        {
            TipsMgr.Instance.ShowSystemTips("�������ǳ�...");
            return;
        }

        //��֤�ǳƵĺϷ���  todo

        CreateRoleBtnClickAction?.Invoke(_iptNickname.text);

    }

}