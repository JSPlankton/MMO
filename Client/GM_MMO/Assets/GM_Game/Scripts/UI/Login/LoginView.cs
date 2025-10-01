using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Title:登录视图
 * Description:它是登录模块所有Window的管理视图类
 */


public class LoginView : UIBase
{

    [SerializeField, Header("登录窗口")] private LoginWindow _loginWindow;
    [SerializeField, Header("注册窗口")] private RegistWindow _registWindow;
    [SerializeField, Header("服务器窗口")] private GameServerWindow _gameServerWindow;
    [SerializeField, Header("服务器列表窗口")] private ServerListWindow _serverListWindow;



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
