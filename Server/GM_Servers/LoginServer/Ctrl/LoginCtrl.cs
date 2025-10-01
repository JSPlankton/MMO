using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class LoginCtrl : IContainer
{
    /// <summary>
    /// 登录服务器作为客户端时， 收到中心服务发来的处理结果的数据
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    public void OnClientCommand(ServerBase serverBase, BasePackage basePackage)
    {

        Session session = SessionMgr.Instance.GetSession(basePackage.UnitySessionId);

        switch (basePackage.ProtoCode)
        {
            case NetDefine.CMD_RegistCode:
                OnRegistResultHandle(session, basePackage);
                break;

            case NetDefine.CMD_LoginCode:
                OnLoginResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_GetServerListCode:
                OnGetServerListResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_LoginGameServerCode:
                OnLoginGameServerResultHandle(session, basePackage);
                break;
            case NetDefine.CMD_CreateRoleCode:
                OnCreateRoleResultHandle(session, basePackage);
                break;
        }

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
    /// 获取服务器列表数据返回
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    private void OnGetServerListResultHandle(Session session, BasePackage basePackage)
    {

        GetServerListRet ret = GetServerListRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnGetServerListResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);

    }

    /// <summary>
    /// 登录请求结果返回
    /// </summary>
    /// <param name="session"></param>
    /// <param name="basePackage"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoginResultHandle(Session session, BasePackage basePackage)
    {
        LoginRet ret = LoginRet.Parser.ParseFrom(basePackage.Data);
        //todoa   ret.CmdCode != CmdCode.Succeed    session.SendError()
        LogMsg.Info("OnLoginResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }
        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    /// <summary>
    /// 注册结果处理
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRegistResultHandle(Session session, BasePackage basePackage)
    {
        RegistRet ret = RegistRet.Parser.ParseFrom(basePackage.Data);
        LogMsg.Info("OnRegistResultHandle::" + ret.ToString());
        if (ret.CmdCode != CmdCode.Succeed)
        {
            session.SendError(basePackage, ret.CmdCode);
            return;
        }

        //把数据发送到Unity端。
        session.SendData(basePackage);
    }

    public void OnInit()
    {
    }


    /// <summary>
    /// 登录服务器 作为服务端的时候，收到客户端发来的数据
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
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
    /// 请求创建角色
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnCreateRoleHandle(ServerBase serverBase, BasePackage basePackage)
    {
        CreateRoleReq req = CreateRoleReq.Parser.ParseFrom(basePackage.Data);
        //todo 请求的用户和密码的合法性
        //就要把收到的数据发送给中心服务器
        //  req.Nickname 是否合法
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnCreateRoleHandle::" + req.ToString());
    }

    /// <summary>
    /// 请求登录服务器
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnLoginGameServerHandle(ServerBase serverBase, BasePackage basePackage)
    {

        LoginGameServerReq req = LoginGameServerReq.Parser.ParseFrom(basePackage.Data);
        //todo 请求的用户和密码的合法性
        //就要把收到的数据发送给中心服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnLoginGameServerHandle::" + req.ToString());

    }

    /// <summary>
    /// 请求获取服务器列表
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnGetServerListHandle(ServerBase serverBase, BasePackage basePackage)
    {
        GetServerListReq req = GetServerListReq.Parser.ParseFrom(basePackage.Data);
        //todo 请求的用户和密码的合法性
        //就要把收到的数据发送给中心服务器
        serverBase._client.SendData(basePackage);
        LogMsg.Info("OnGetServerListHandle::" + req.ToString());

    }

    /// <summary>
    /// 登录请求
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnLoginHandle(ServerBase serverBase, BasePackage basePackage)
    {
        LoginReq req = LoginReq.Parser.ParseFrom(basePackage.Data);

        //就要把收到的数据发送给中心服务器

        //todo 请求的用户和密码的合法性

        //用户频繁登录..
        long timer = DataUtils.Instance.GetLoginMilliseconds(req.UserName);
        if (timer > 0 && DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - timer < 300)
        {
            serverBase.SendError(basePackage, CmdCode.UserOftenLogin);
            return;
        }


        DataUtils.Instance.AddLoginMilliseconds(req.UserName, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        serverBase._client.SendData(basePackage);

        LogMsg.Info("OnLoginHandle::" + req.ToString());
    }

    /// <summary>
    /// 处理注册事件
    /// </summary>
    /// <param name="serverBase"></param>
    /// <param name="basePackage"></param>
    private void OnRegistHandle(ServerBase serverBase, BasePackage basePackage)
    {

        RegistReq req = RegistReq.Parser.ParseFrom(basePackage.Data);
        //todo ，验证合法性

        //验证用户名是否合法
        if (!DataUtils.IsValidUserName(req.UserName))
        {
            serverBase.SendError(basePackage, CmdCode.UserNameIllegal);
            return;
        }

        //验证手机号码合法性
        if (!DataUtils.IsValidMobile(req.PhoneNum))
        {
            serverBase.SendError(basePackage, CmdCode.PhoneNumIllegal);
            return;
        }

        //验证密码
        if (req.Password.Length < 4 || req.Password.Length > 16)
        {
            serverBase.SendError(basePackage, CmdCode.PasswordIllegal);
            return;
        }


        //就要把收到的数据发送给中心服务器
        serverBase._client.SendData(basePackage);

        LogMsg.Info("OnRegistHandle::" + req.ToString());
    }
}
