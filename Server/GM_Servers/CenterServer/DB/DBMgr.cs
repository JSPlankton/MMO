using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class DBMgr : Singleton<DBMgr>
{

    public SqlSugarClient InitDB()
    {

        ConnectionConfig connectionConfig = new ConnectionConfig()
        {
            ConnectionString = "Server=localhost;Port=3306;DataBase=gm_game;User=root;Password=123456;",
            DbType = DbType.MySql,
            IsAutoCloseConnection = true,
        };

        SqlSugarClient db = new SqlSugarClient(connectionConfig);

        //建库
        db.DbMaintenance.CreateDatabase();//达梦和Oracle不支持建库

        //建表（看文档迁移）
        db.CodeFirst.InitTables(typeof(AccountTable), typeof(GameServerTable), typeof(RoleTable)); //所有库都支持

        //for (int i = 0; i < 20; i++)
        //{
        //    GameServerTable gameServerTable = new GameServerTable()
        //    {
        //        ServerName = (i + 1) + "区 九州大陆",
        //        RunState = 1,
        //        IsNew = 1,
        //        IPHost = NetDefine.IPHost,
        //        Port = NetDefine.GateServerPort,
        //        CreateDate = DateTime.Now,
        //        UpdateDate = DateTime.Now,
        //    };
        //    db.Insertable(gameServerTable).ExecuteCommand();
        //}

        return db;
    }

}

