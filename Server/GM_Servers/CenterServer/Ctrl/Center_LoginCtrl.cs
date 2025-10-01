using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 中心服务器处理登录模块的相关逻辑
/// </summary>
public class Center_LoginCtrl : IContainer
{

    private LoginModle _loginModle;

    public Center_LoginCtrl(LoginModle loginModle)
    {
        _loginModle = loginModle;
    }

    public void OnClientCommand(ServerBase serverBase, BasePackage basePackage)
    {
    }

    public void OnInit()
    {
    }

    public void OnServerCommand(ServerBase serverBase, BasePackage basePackage)
    {

        switch (basePackage.ProtoCode)
        {

            case NetDefine.CMD_RegistCode:
                OnRegistHandle(serverBase, basePackage);
                break;

            case NetDefine.CMD_LoginCode:
                OnLoginHandle(serverBase, basePackage);
                break;

            case NetDefine.CMD_GetServerListCode:
                OnGetServerListHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_LoginGameServerCode:
                OnLoginGameServerHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_CreateRoleCode:
                OnCreateRoleHandle(serverBase, basePackage);
                break;

        }
    }

    /// <summary>
    /// 创建角色请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnCreateRoleHandle(ServerBase serverBase, BasePackage basePackage)
    {
        CreateRoleReq req = CreateRoleReq.Parser.ParseFrom(basePackage.Data);
        LogMsg.Info("OnCreateRoleHandle=>req::" + req.ToString());

        CreateRoleRet ret = _loginModle.CreateRole(req);
        LogMsg.Info("OnCreateRoleHandle=>ret::" + ret.ToString());
        serverBase.SendData(basePackage, basePackage.ProtoCode, ret.ToByteString());
    }

    /// <summary>
    /// 登录游戏服务器请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnLoginGameServerHandle(ServerBase serverBase, BasePackage basePackage)
    {

        LoginGameServerReq req = LoginGameServerReq.Parser.ParseFrom(basePackage.Data);
        LogMsg.Info("OnLoginGameServerHandle=>req::" + req.ToString());

        LoginGameServerRet ret = _loginModle.LoginGameServer(req);
        LogMsg.Info("OnLoginGameServerHandle=>ret::" + ret.ToString());
        serverBase.SendData(basePackage, basePackage.ProtoCode, ret.ToByteString());

    }

    /// <summary>
    /// 请求说去服务器列表
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnGetServerListHandle(ServerBase serverBase, BasePackage basePackage)
    {
        GetServerListReq req = GetServerListReq.Parser.ParseFrom(basePackage.Data);
        LogMsg.Info("OnGetServerListHandle=>req::" + req.ToString());

        GetServerListRet ret = _loginModle.GetServerList(req);
        LogMsg.Info("OnGetServerListHandle=>ret::" + ret.ToString());
        serverBase.SendData(basePackage, basePackage.ProtoCode, ret.ToByteString());
    }

    /// <summary>
    /// 登录请求处理
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnLoginHandle(ServerBase serverBase, BasePackage basePackage)
    {
        LoginReq req = LoginReq.Parser.ParseFrom(basePackage.Data);
        LogMsg.Info("OnLoginHandle=>req::" + req.ToString());

        LoginRet ret = _loginModle.Login(req);
        LogMsg.Info("OnLoginHandle=>ret::" + ret.ToString());
        serverBase.SendData(basePackage, basePackage.ProtoCode, ret.ToByteString());
    }

    /// <summary>
    /// 处理注册事件
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRegistHandle(ServerBase serverBase, BasePackage basePackage)
    {

        RegistReq req = RegistReq.Parser.ParseFrom(basePackage.Data);
        LogMsg.Info("OnRegistHandle=>req::" + req.ToString());

        RegistRet ret = _loginModle.RegistAccount(req);
        LogMsg.Info("OnRegistHandle=>ret::" + ret.ToString());

        serverBase.SendData(basePackage, basePackage.ProtoCode, ret.ToByteString());
    }
}
