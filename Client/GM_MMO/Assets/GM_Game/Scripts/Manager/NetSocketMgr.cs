using Google.Protobuf;
using System;
using System.Threading;
using NetWork.Socket;
using UnityEditor.PackageManager;
using UnityEngine;

/**
 * Title:网络模块的管理类
 * Description:
 */


public class NetSocketMgr : Singleton<NetSocketMgr>
{

    private static NetClient _client;

    public static NetClient Client { get => _client; }


    private SynchronizationContext synchronizationContext;

    public void Init()
    {
        synchronizationContext = SynchronizationContext.Current;

        ConnectServer(NetDefine.IPHost, NetDefine.LoginServerPort);
        NetErrorMsgMgr.Instance.Init();
    }

    /// <summary>
    /// 开始连接服务端
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="connSucceed"></param>
    /// <param name="connFailed"></param>
    public void ConnectServer(string host, int port, Action connSucceed = null, Action connFailed = null)
    {
        Disconnect();

        _client = new NetClient(host, port, ClientType.Unity);
        _client.OnReceiveMsg += OnReceiveMsgHandle;
        if (null != connSucceed)
        {
            _client.OnConnSucceed = connSucceed;
        }
        if (null != connFailed)
        {
            _client.OnConnFailed = connFailed;
        }
        _client.StartConnect();

    }

    /// <summary>
    /// 收到服务端发来的数据
    /// </summary>
    /// <param name="arg1"></param>
    /// <param name="string"></param>
    private void OnReceiveMsgHandle(int protoCode, ByteString data)
    {


        //把子线程切换回主线程
        synchronizationContext.Post(_ =>
        {
            //
            SocketDispatcher.Instance.DispatcherEvent(protoCode, data);
        }, null);

    }


    public void Disconnect()
    {

        if (_client != null)
        {

            _client._isNeedReconn = false;
            _client.Disconnect();
            _client = null;
        }

    }

}
