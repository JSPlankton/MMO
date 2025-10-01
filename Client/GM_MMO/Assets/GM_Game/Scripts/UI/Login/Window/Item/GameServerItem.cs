using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/**
 * Title:
 * Description:
 */


public class GameServerItem : MonoBehaviour
{

    [SerializeField] private Image _imgRunState;
    [SerializeField] private TMP_Text _texServerName;

    private GameServer _gameServer;

    public Action<GameServer> OnItemClickedCB;

    //点击次数
    private int _clickCount;

    internal void RefreshUI(GameServer gameServer)
    {
        _gameServer = gameServer;
        Color color = Color.white;
        if (gameServer.RunState == 1)
        {
            color = Color.red;
        }
        else if (gameServer.RunState == 2)
        {
            color = Color.yellow;
        }
        else if (gameServer.RunState == 3)
        {
            color = Color.green;
        }

        _imgRunState.color = color;


        string str = "";
        if (gameServer.IsNew == 1)
        {
            str = "(新服)";
        }


        _texServerName.SetText(gameServer.ServerName + str);

    }



    public void OnItemClicked()
    {
        _clickCount++;

        ResetCount();

        if (_clickCount >= 2)
        {//双击
            UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.GameServerWindow, _gameServer);
        }

        OnItemClickedCB?.Invoke(_gameServer);
    }

    //重置点击次数
    private void ResetCount()
    {

        Observable.Timer(TimeSpan.FromMilliseconds(300)).Subscribe(_ =>
        {
            _clickCount = 0;
        });

    }
}
