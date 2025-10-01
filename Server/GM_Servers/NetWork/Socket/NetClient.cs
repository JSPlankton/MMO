﻿
namespace NetWork.Socket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    public class NetClient : ServerBase
    {
        private string _host;
        private int _port;

        private Timer _reconnectTimer;
        //是否需要重连
        public bool _isNeedReconn = true;

        public NetClient(string ip, int port, ClientType clientType)
        {
            _host = ip;
            _port = port;
            _clientType = clientType;
            _connState = ConnState.Disconnected;
        }

        /// <summary>
        /// 开始连接服务端
        /// </summary>
        public void StartConnect()
        {
            try
            {
                if (_connState != ConnState.Disconnected) { return; }

                if (_socket == null)
                {
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }

                //开始连接服务端
                _socket.BeginConnect(_host, _port, OnConnectCB, null);
            }
            catch (Exception ex)
            {
                Disconnect();
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 连接服务端成功
        /// </summary>
        /// <param name="ar"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnConnectCB(IAsyncResult ar)
        {
            try
            {
                _socket.EndConnect(ar);
                _connState = ConnState.Connected;

                LogMsg.Info($"连接服务端成功:{_socket.RemoteEndPoint}");
                //开始接收服务端发来的数据
                BeginReceive();
            }
            catch (Exception ex)
            {
                Disconnect();
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 注册指令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="container"></param>
        public void RegistCommand(int cmd, IContainer container)
        {
            _cmdDic.Add(cmd, container);
        }

        protected override void HandleCommand(BasePackage basePackage)
        {

            if (_clientType == ClientType.Unity)
            {
                OnReceiveMsg?.Invoke(basePackage.ProtoCode, basePackage.Data);
                return;
            }

            IContainer container = _cmdDic[basePackage.ProtoCode];
            if (container == null)
            {
                LogMsg.Info("command not regist..");
                return;
            }

            container.OnClientCommand(this, basePackage);


        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public override void Disconnect()
        {
            SetReconnectTimer();
            base.Disconnect();
        }

        /// <summary>
        /// 设置重连时间
        /// </summary>
        private void SetReconnectTimer()
        {
            if (_reconnectTimer == null)
            {
                _reconnectTimer = new Timer(ReConn);
            }
            _reconnectTimer.Change(3000, 10000);

        }

        /// <summary>
        /// 断开重连
        /// </summary>
        /// <param name="state"></param>
        private void ReConn(object state)
        {
            if (_isNeedReconn)
            {
                StartConnect();
            }
        }
    }
}
