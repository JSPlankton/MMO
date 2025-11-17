using MySqlX.XDevAPI;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenterServer
{
    internal class CenterApp
    {
        static void Main(string[] args)
        {

            //开启服务端
            NetServer server = new NetServer(null);
            server.StartServer(NetDefine.IPHost, NetDefine.CenterServerPort);
            //初始化数据库
            SqlSugarClient db = DBMgr.Instance.InitDB();
            //初始化数据表管理类
            LubanMgr.Instance.Init();

            Center_LoginCtrl loginCtrl = new Center_LoginCtrl(new LoginModle(db));
            Center_RoleCtrl roleCtrl = new Center_RoleCtrl(new Center_RoleModel(db));
            //注册指令集
            server.RegistCommand(NetDefine.CMD_RegistCode, loginCtrl);//注册接口指令集
            server.RegistCommand(NetDefine.CMD_LoginCode, loginCtrl);//登录接口指令集
            server.RegistCommand(NetDefine.CMD_GetServerListCode, loginCtrl);//获取服务器列表接口指令集
            server.RegistCommand(NetDefine.CMD_LoginGameServerCode, loginCtrl);//登录游戏服务器接口指令集
            server.RegistCommand(NetDefine.CMD_CreateRoleCode, loginCtrl);//创建角色接口指令集
            server.RegistCommand(NetDefine.CMD_StartGameCode, loginCtrl);//开始游戏接口指令集

            server.RegistCommand(NetDefine.CMD_EnterWroldCode, roleCtrl);//角色进入游戏世界请求接口指令集

            while (true)
            {
                Thread.Sleep(1);
            }



        }
    }
}
