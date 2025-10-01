using Google.Protobuf;
using System;
using TMPro;
using UnityEngine;
using YooAsset;

/**
 * Title: 登录服务器窗口
 * Description:
 */


public class GameServerWindow : WindowBase
{

    [SerializeField] private TMP_Text _texRunState;
    [SerializeField] private TMP_Text _texServerName;

    private GameServer _gameServer;
    public Action<GameServer> GameServerBtnClickAction;

    public override void RefreshUI(object obj)
    {
        GameServer gameServer = obj as GameServer;
        if (gameServer != null)
        {
            _gameServer = gameServer;
            Color color = Color.white;
            string runState = "";
            if (gameServer.RunState == 1)
            {
                color = Color.red;
                runState = "爆满";
            }
            else if (gameServer.RunState == 2)
            {
                color = Color.yellow;
                runState = "拥挤";
            }
            else if (gameServer.RunState == 3)
            {
                color = Color.green;
                runState = "正常";
            }

            _texRunState.color = color;
            _texRunState.SetText(runState);


            string str = "";
            if (gameServer.IsNew == 1)
            {
                str = "(新服)";
            }


            _texServerName.SetText(gameServer.ServerName + str);

        }
    }

    public void OnGotoServerListBtnClicked()
    {
        //todo
        // UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.ServerListWindow);

        GetServerListReq req = new GetServerListReq()
        {
            ServerId = 0,   //获取所有数据
        };
        NetSocketMgr.Client.SendData(NetDefine.CMD_GetServerListCode, req.ToByteString());

    }



    public void OnGameServerBtnClicked()
    {
        GameServerBtnClickAction?.Invoke(_gameServer);
    }


}