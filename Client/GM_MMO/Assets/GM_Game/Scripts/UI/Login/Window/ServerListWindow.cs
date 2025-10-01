using Google.Protobuf.Collections;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using YooAsset;

/**
 * Title:�������б���
 * Description:
 */


public class ServerListWindow : WindowBase
{

    [SerializeField, Header("����������")] private TMP_Text _texServerName;

    [SerializeField, Header("Item������")] private Transform _itemParentTrans;

    private RepeatedField<GameServer> _gameServers;
    private GameServer _gameServer;

    public override void RefreshUI(object obj)
    {

        GetServerListRet ret = obj as GetServerListRet;
        if (ret != null && ret.GameServers != null && ret.GameServers.Count > 0)
        {
            //����������ݺ��»�ȡ��������һ���� ��ô��return;
            if (_gameServers != null && _gameServers.SequenceEqual(ret.GameServers))
            {
                return;
            }

            if (Global.Instance.LoginInfo != null)
            {
                SetServerName(Global.Instance.LoginInfo.GameServer.ServerName);
            }

            _gameServers = ret.GameServers;
            GenerateServerListItem();
        }

    }

    private void SetServerName(string serverName)
    {
        _texServerName.SetText(serverName);
    }


    /// <summary>
    /// ����ServerList Item
    /// </summary>
    private async void GenerateServerListItem()
    {
        //todo
        AssetOperationHandle handle = Global.Instance.YooPackage.LoadAssetAsync("Assets/GM_Game/Prefabs/UIPrefabs/ServerListItemWidget");
        await handle.Task;
        for (int i = 0; i < _gameServers.Count; i++)
        {
            GameObject go = handle.InstantiateSync();
            go.transform.parent = _itemParentTrans;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;


            GameServerItem item = go.GetComponent<GameServerItem>();
            if (item != null)
            {
                item.RefreshUI(_gameServers[i]);
                item.OnItemClickedCB = OnItemClicked;
            }

        }
    }



    /// <summary>
    /// �������б�Item�����
    /// </summary>
    /// <param name="server"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    private void OnItemClicked(GameServer server)
    {
        _gameServer = server;
        SetServerName(server.ServerName);
    }

    public void OnCloseBtnClicked()
    {
        UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.GameServerWindow);

    }

    public void OnConfirmBtnClicked()
    {

        UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.GameServerWindow, _gameServer);

    }


}
