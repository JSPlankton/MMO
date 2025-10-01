using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Title:��¼��ͼ
 * Description:���ǵ�¼ģ������Window�Ĺ�����ͼ��
 */


public class LoginView : UIBase
{

    [SerializeField, Header("��¼����")] private LoginWindow _loginWindow;
    [SerializeField, Header("ע�ᴰ��")] private RegistWindow _registWindow;
    [SerializeField, Header("����������")] private GameServerWindow _gameServerWindow;
    [SerializeField, Header("�������б���")] private ServerListWindow _serverListWindow;



    public override void InitView()
    {

        base.InitView();

        windowDic.Add(WindowType.LoginWindow, _loginWindow);
        windowDic.Add(WindowType.RegistWindow, _registWindow);
        windowDic.Add(WindowType.GameServerWindow, _gameServerWindow);
        windowDic.Add(WindowType.ServerListWindow, _serverListWindow);
    }

    public void RegistGameServerBtnClicked(Action<GameServer> action)
    {
        _gameServerWindow.GameServerBtnClickAction = action;
    }


}
