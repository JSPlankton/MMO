using Google.Protobuf;
using System;
using TMPro;
using UnityEngine;
using YooAsset;

/**
 * Title: ��¼����������
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
                runState = "����";
            }
            else if (gameServer.RunState == 2)
            {
                color = Color.yellow;
                runState = "ӵ��";
            }
            else if (gameServer.RunState == 3)
            {
                color = Color.green;
                runState = "����";
            }

            _texRunState.color = color;
            _texRunState.SetText(runState);


            string str = "";
            if (gameServer.IsNew == 1)
            {
                str = "(�·�)";
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
            ServerId = 0,   //��ȡ��������
        };
        NetSocketMgr.Client.SendData(NetDefine.CMD_GetServerListCode, req.ToByteString());

    }



    public void OnGameServerBtnClicked()
    {
        GameServerBtnClickAction?.Invoke(_gameServer);
    }


}