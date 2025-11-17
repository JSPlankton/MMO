using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 处理登录模块相关的数据库业务
/// </summary>
public class LoginModle
{
    SqlSugarClient _db;
    public LoginModle(SqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 创建角色请求处理
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    internal CreateRoleRet CreateRole(CreateRoleReq req)
    {
        CreateRoleRet ret = new CreateRoleRet();

        RoleTable roleTable = _db.Queryable<RoleTable>().Where(v => v.ServerId == req.GameServerId && v.Nickname == req.Nickname).First();
        if (roleTable != null)
        {
            //昵称已经存在
            ret.CmdCode = CmdCode.NicknameExist;
        }
        else
        {
            RoleTable role = new RoleTable()
            {
                AccountID = req.AccountId,
                Money = 10000, //默认是0
                Nickname = req.Nickname,
                JobID = req.JobId,
                Level = 1,//默认1
                Exp = 0,
                SkillUpPoint = 6,//用于测试
                MaxHP = 1000,
                CurrHP = 1000,
                MaxMP = 2000,
                CurrMP = 2000,
                Atk = 12,
                Def = 5,
                Crit = 6,
                Dodeg = 7,
                Hit = 8,
                Penet = 6,
                Pos = "",//角色默认位置
                CameraOffset = "",
                MapId = 1,
                ServerId = req.GameServerId,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            int id = _db.Insertable(role).ExecuteReturnIdentity();
            if (id > 0)
            {
                //为角色创建技能信息
                CreateRoleSKillInfo(id, role.JobID);
                ret.RoleId = id;
                ret.Nickname = role.Nickname;
                ret.JobId = role.JobID;
                ret.Level = role.Level;
            }
            else
            {
                ret.CmdCode = CmdCode.ServerError;
            }

        }


        return ret;
    }

    /// <summary>
    /// 为角色创建技能信息
    /// </summary>
    /// <param name="roleId"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void CreateRoleSKillInfo(int roleId, int jobId)
    {
        //删除技能表的数据
        var roleSkillTables = _db.Queryable<RoleSkillTable>().Where(v => v.RoleId == roleId).ToList();
        if (roleSkillTables != null && roleSkillTables.Count > 0)
        {
            _db.Deleteable(roleSkillTables).ExecuteCommand();
            roleSkillTables.Clear();
        }
        else
        {
            roleSkillTables = new List<RoleSkillTable>();
        }

        //为角色添加技能数据
        var skillInfoDic = LubanMgr.Instance.GetSkillInfosByJobId(jobId);
        foreach (var item in skillInfoDic) {
            RoleSkillTable roleSkillTable = new RoleSkillTable() {
                RoleId = roleId,
                SkillId = item.Value.Id,
                Level = 0,
                BindKey = "",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            //添加默认普通技能
            if (roleSkillTable.SkillId == 10010)
            {
                roleSkillTable.Level = 1;
                roleSkillTable.BindKey = "Q";
            }
            roleSkillTables.Add(roleSkillTable);
        }
        _db.Insertable(roleSkillTables).ExecuteCommand();
    }

    /// <summary>
    /// 请求获取服务器列表处理
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    internal GetServerListRet GetServerList(GetServerListReq req)
    {
        GetServerListRet ret = new GetServerListRet();

        if (req.ServerId == 0)
        {
            //查询服务器表
            List<GameServerTable> gameServerTables = _db.Queryable<GameServerTable>().ToList();
            if (gameServerTables != null && gameServerTables.Count > 0)
            {
                for (int i = 0; i < gameServerTables.Count; i++)
                {
                    GameServer gameServer = new GameServer()
                    {
                        ServerId = gameServerTables[i].Id,
                        ServerName = gameServerTables[i].ServerName,
                        RunState = gameServerTables[i].RunState,
                        IsNew = gameServerTables[i].IsNew,
                        IpHost = gameServerTables[i].IPHost,
                        Prot = gameServerTables[i].Port,
                    };
                    ret.GameServers.Add(gameServer);
                }
            }
            else
            {
                ret.CmdCode = CmdCode.ServerError;
            }
        }
        return ret;
    }

    /// <summary>
    /// 登录请求处理
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    internal LoginRet Login(LoginReq req)
    {
        LoginRet ret = new LoginRet();


        AccountTable accountTable = _db.Queryable<AccountTable>().Where(v => v.UserName == req.UserName).First();
        if (accountTable == null)
        {
            ret.CmdCode = CmdCode.AcctNotExist;//账号不存在
        }
        else
        {
            if (accountTable.Passwrod.Equals(req.Password))
            {
                if (accountTable.State != 1)
                {//账号是被禁用
                    ret.CmdCode = CmdCode.AcctDisable;//账号禁用
                }
                else
                {
                    //todo 判断账号是否已经登录。

                    //登录成功
                    GameServerTable gameServer = _db.Queryable<GameServerTable>().Where(v => v.Id == accountTable.LastLoginServerId).First();
                    if (gameServer != null)
                    {
                        ret.GameServer = new GameServer()
                        {
                            ServerId = gameServer.Id,
                            ServerName = gameServer.ServerName,
                            RunState = gameServer.RunState,
                            IsNew = gameServer.IsNew,
                            IpHost = gameServer.IPHost,
                            Prot = gameServer.Port,
                        };
                    }
                    ret.AccountId = accountTable.Id;
                }
            }
            else
            { //密码错误
                ret.CmdCode = CmdCode.PasswordError;
            }
        }
        return ret;
    }

    /// <summary>
    /// 请求登录游戏服务器处理
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    internal LoginGameServerRet LoginGameServer(LoginGameServerReq req)
    {
        LoginGameServerRet ret = new LoginGameServerRet();

        AccountTable accountTable = _db.Queryable<AccountTable>().Where(v => v.Id == req.AccountId).First();
        if (accountTable != null)
        {

            GameServerTable gameServerTable = _db.Queryable<GameServerTable>().Where(v => v.Id == req.GameServerId).First();
            if (gameServerTable != null)
            {

                accountTable.LastLoginServerId = req.GameServerId;
                if (_db.Updateable(accountTable).ExecuteCommand() > 0)
                {
                    //查询角色表， 当前用户是否已经创建了角色， 如果没有创建，那么返回默认数据
                    RoleTable roleTable = _db.Queryable<RoleTable>().Where(v => v.AccountID == req.AccountId).First();
                    if (roleTable != null)
                    {
                        ret.CreateRoleInfo = new CreateRoleRet()
                        {
                            RoleId = roleTable.Id,
                            Nickname = roleTable.Nickname,
                            JobId = roleTable.JobID,
                            Level = roleTable.Level,
                        };
                    }
                }
                else
                {
                    ret.CmdCode = CmdCode.ServerError;//服务端发送错误
                }
            }
            else
            {
                ret.CmdCode = CmdCode.ReqParamError;//请求参数错误
            }
        }
        else
        {
            ret.CmdCode = CmdCode.AcctNotExist;//账户不存在
        }
        return ret;
    }

    /// <summary>
    /// 注册账号
    /// </summary>
    /// <param name="req"></param>
    internal RegistRet RegistAccount(RegistReq req)
    {
        RegistRet ret = new RegistRet();
        //1.判断是否已经注册
        List<AccountTable> accountTables = _db.Queryable<AccountTable>().Where(v => v.UserName == req.UserName).ToList();
        if (accountTables.Count > 0)
        {
            ret.CmdCode = CmdCode.AcctExist;//账号已存在
        }
        else
        {
            //注册
            AccountTable accountTable = new AccountTable()
            {
                UserName = req.UserName,
                PhoneNum = req.PhoneNum,
                Passwrod = req.Password,
                LastLoginServerId = 1,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
            //插入数据
            int id = _db.Insertable(accountTable).ExecuteCommand();
            if (id <= 0)
            {
                ret.CmdCode = CmdCode.ServerError;//暂时..
            }
        }
        return ret;
    }

    /// <summary>
    /// 开始游戏请求处理
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    internal StartGameRet StartGame(StartGameReq req)
    {
        StartGameRet ret = new StartGameRet();

        RoleTable role = _db.Queryable<RoleTable>().Where(v => v.Id == req.RoleId).First();
        if (role != null)
        {
            //角色基础信息
            RoleBaseInfo baseInfo = new RoleBaseInfo()
            {
                RoleId = role.Id,
                AccountId = role.AccountID,
                Nickname = role.Nickname,
                Level = role.Level,
                JobId = role.JobID,
                MaxHp = role.MaxHP,
                CurrHp = role.CurrHP,
                MaxMp = role.MaxMP,
                CurrMp = role.CurrMP,
                Akt = role.Atk,
                Def = role.Def,
                Crit = role.Crit,
                Dodge = role.Dodeg,
                Hit = role.Hit,
                Penet = role.Penet,
                Pos = role.Pos,
                MapId = role.MapId,
            };

            //主角信息
            MainRoleInfo mainRoleInfo = new MainRoleInfo()
            {
                BaseInfo = baseInfo,
                Money = role.Money,
                Exp = role.Exp,
                SkillUpPoint = role.SkillUpPoint,
                CameraOffset = role.CameraOffset,
                ServerId = role.ServerId,
            };

            ret.MainRoleInfo = mainRoleInfo;
            //存储当开始游戏的角色信息数据
            //Center_Global.Instance.AddRole(role.Id, mainRoleInfo);
        }
        else
        {
            ret.CmdCode = CmdCode.RoleNotExist;//角色不存在
        }


        return ret;
    }
}
