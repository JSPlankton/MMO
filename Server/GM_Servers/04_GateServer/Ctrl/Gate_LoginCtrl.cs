using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Gate_LoginCtrl : IContainer
{

    /// <summary>
    /// 网关服务器作为客户端时， 收到游戏逻辑服务求发来的处理结果的数据
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    public void OnClientCommand(ServerBase serverBase, BasePackage basePackage)
    {

        Session session = SessionMgr.Instance.GetSession(basePackage.UnitySessionId);

        switch (basePackage.ProtoCode)
        {
            case NetDefine.CMD_LoginGameServerCode:
                OnLoginGameServerResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_CreateRoleCode:
                OnCreateRoleResultHandle(session, basePackage);
                break;
        }

    }

    public void OnInit()
    {
    }


    /// <summary>
    /// 网关服务器 作为服务端的时候，收到客户端发来的数据
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    public void OnServerCommand(ServerBase serverBase, BasePackage basePackage)
    {
        switch (basePackage.ProtoCode)
        {
            case NetDefine.CMD_LoginGameServerCode:
                OnLoginGameServerHandle(serverBase, basePackage);
                break;
            case NetDefine.CMD_CreateRoleCode:
                OnCreateRoleHandle(serverBase, basePackage);
                break;
        }
    }

    /// <summary>
    /// 请求登录服务器
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnLoginGameServerHandle(ServerBase serverBase, BasePackage basePackage)
    {

        LoginGameServerReq req = LoginGameServerReq.Parser.ParseFrom(basePackage.Data);
        //todo 验证请求数据的合法性
        //就要把收到的数据发送给游戏逻辑服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnLoginGameServerHandle::" + req.ToString());

    }

    /// <summary>
    /// 登录游戏服务器返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoginGameServerResultHandle(Session session, BasePackage basePackage)
    {
        LoginGameServerRet ret = LoginGameServerRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnLoginGameServerResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 创建角色请求 返回数据
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnCreateRoleResultHandle(Session session, BasePackage basePackage)
    {
        CreateRoleRet ret = CreateRoleRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnCreateRoleResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 请求创建角色
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnCreateRoleHandle(ServerBase serverBase, BasePackage basePackage)
    {
        CreateRoleReq req = CreateRoleReq.Parser.ParseFrom(basePackage.Data);
        //todo 请求的用户和密码的合法性
        //就要把收到的数据发送给游戏逻辑服务器
        //  req.Nickname 是否合法
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnCreateRoleHandle::" + req.ToString());
    }
}
