﻿using NetWork.Socket.com;
using NetWork.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class NetServer
{
    private Socket _socket;

    private Dictionary<int, IContainer> _cmdDic = new Dictionary<int, IContainer>();
    private NetClient _client;

    public NetServer(NetClient client)
    {
        _client = client;
    }

    public void StartServer(string ip, int port)
    {

        //创建Socket                  寻址方式：IPV4         数据传输方式：Stream  协议类型：TCP
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //设置Ip和端口
        EndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

        //绑定IP和端口
        _socket.Bind(endPoint);

        Console.WriteLine("服务器开启成功：" + endPoint.ToString());

        //设置Socket最大连接数
        _socket.Listen(100);


        Thread listenThread = new Thread(ListenConnectSocket);
        listenThread.IsBackground = true;
        listenThread.Start();
    }

    /// <summary>
    /// 开始监听客户端连接
    /// </summary>
    private void ListenConnectSocket()
    {
        _socket.BeginAccept(ClientConnectCB, null);
    }


    /// <summary>
    /// 有客户端连接
    /// </summary>
    /// <param name="ar"></param>
    private void ClientConnectCB(IAsyncResult ar)
    {

        try
        {
            Socket clientSocket = _socket.EndAccept(ar);

            LogMsg.Info($"客户端::{clientSocket.RemoteEndPoint} 连接成功... ");

            //开始接收客户端数据
            Session session = new Session(_cmdDic, _client);
            session.ReceiveData(clientSocket);

            //当处理完成当前客户端连接后，继续处理下一个客户端的连接请求
            ListenConnectSocket();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public void RegistCommand(int cmd, IContainer container)
    {
        _cmdDic.Add(cmd, container);
    }

}
