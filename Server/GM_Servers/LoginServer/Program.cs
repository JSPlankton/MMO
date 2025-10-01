using Google.Protobuf;
using NetWork.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginServer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //登录服务端  去连接中心服务器
            NetClient client = new NetClient(NetDefine.IPHost, NetDefine.CenterServerPort, ClientType.LoginServer);
            client.StartConnect();

            //登录服务器  开启服务端
            NetServer server = new NetServer(client);
            server.StartServer(NetDefine.IPHost, NetDefine.LoginServerPort);


            LoginCtrl loginCtrl = new LoginCtrl();
            //注册指令集
            server.RegistCommand(NetDefine.CMD_RegistCode, loginCtrl);
            server.RegistCommand(NetDefine.CMD_LoginCode, loginCtrl);
            server.RegistCommand(NetDefine.CMD_GetServerListCode, loginCtrl);
            server.RegistCommand(NetDefine.CMD_LoginGameServerCode, loginCtrl);
            server.RegistCommand(NetDefine.CMD_CreateRoleCode, loginCtrl);

            client.RegistCommand(NetDefine.CMD_RegistCode, loginCtrl);
            client.RegistCommand(NetDefine.CMD_LoginCode, loginCtrl);
            client.RegistCommand(NetDefine.CMD_GetServerListCode, loginCtrl);
            client.RegistCommand(NetDefine.CMD_LoginGameServerCode, loginCtrl);
            client.RegistCommand(NetDefine.CMD_CreateRoleCode, loginCtrl);

            //new Timer(_ =>
            //{
            //    //模拟
            //    RegistReq req = new RegistReq()
            //    {
            //        UserName = "aaaaaa",
            //        PhoneNum = "13000000000",
            //        Password = "12345"
            //    };
            //    client.SendData(NetDefine.CMD_RegistCode, req.ToByteString());

            //}, null, 5000, Timeout.Infinite);


            while (true)
            {
                Thread.Sleep(1);
            }

        }
    }
}
