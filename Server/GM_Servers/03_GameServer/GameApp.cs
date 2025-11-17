using NetWork.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03_GameServer
{
    internal class GameApp
    {
        static void Main(string[] args)
        {
            //游戏逻辑服务器作为客户端  去连接中心服务器
            NetClient client = new NetClient(NetDefine.IPHost, NetDefine.CenterServerPort, ClientType.GameServer);
            client.StartConnect();

            //游戏逻辑服务器  开启服务端
            NetServer server = new NetServer(client);
            server.StartServer(NetDefine.IPHost, NetDefine.GameServerPort);

            Game_LoginCtrl loginCtrl = new Game_LoginCtrl();
            Game_RoleCtrl roleCtrl = new Game_RoleCtrl();
            //注册指令集
            server.RegistCommand(NetDefine.CMD_LoginGameServerCode, loginCtrl);
            server.RegistCommand(NetDefine.CMD_CreateRoleCode, loginCtrl);
            server.RegistCommand(NetDefine.CMD_StartGameCode, loginCtrl);

            client.RegistCommand(NetDefine.CMD_StartGameCode, loginCtrl);
            client.RegistCommand(NetDefine.CMD_LoginGameServerCode, loginCtrl);
            client.RegistCommand(NetDefine.CMD_CreateRoleCode, loginCtrl);

            server.RegistCommand(NetDefine.CMD_EnterWroldCode, roleCtrl);
            client.RegistCommand(NetDefine.CMD_RoleSkillInfoCode, roleCtrl);

            while (true)
            {
                Thread.Sleep(1);
            }
        }
    }
}
