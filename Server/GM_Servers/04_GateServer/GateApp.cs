using NetWork.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _04_GateServer
{
    internal class GateApp
    {
        static void Main(string[] args)
        {
            //网关服务器作为客户端  去连接中心服务器
            NetClient client = new NetClient(NetDefine.IPHost, NetDefine.GameServerPort, ClientType.GateServer);
            client.StartConnect();

            //网关服务器  开启服务端
            NetServer server = new NetServer(client);
            server.StartServer(NetDefine.IPHost, NetDefine.GateServerPort);

            Gate_LoginCtrl loginCtrl = new Gate_LoginCtrl();
            //注册指令集
            server.RegistCommand(NetDefine.CMD_LoginGameServerCode, loginCtrl);
            server.RegistCommand(NetDefine.CMD_CreateRoleCode, loginCtrl);
            server.RegistCommand(NetDefine.CMD_StartGameCode, loginCtrl);

            client.RegistCommand(NetDefine.CMD_StartGameCode, loginCtrl);
            client.RegistCommand(NetDefine.CMD_LoginGameServerCode, loginCtrl);
            client.RegistCommand(NetDefine.CMD_CreateRoleCode, loginCtrl); 

            while (true)
            {
                Thread.Sleep(1);
            }
        }
    }
}
