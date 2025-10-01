using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[SugarTable("game_server", TableDescription = "服务器列表")]
internal class GameServerTable
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 IsPrimaryKey:表示是否是主键，IsIdentity:表示是否自增长
    public int Id { get; set; }

    //状态
    [SugarColumn(DefaultValue = "1", IsOnlyIgnoreInsert = true)]
    public byte State { get; set; }

    //服务器名称
    [SugarColumn(Length = 30)]
    public string ServerName { set; get; }
    //服务器运行状态 1.爆满 2.拥挤 3.正常
    public byte RunState { get; set; }

    //是否是新服
    public byte IsNew { get; set; }
    //服务器IP
    [SugarColumn(Length = 30)]
    public string IPHost { set; get; }

    public int Port { set; get; }
    //创建时间
    public DateTime CreateDate { get; set; }

    //更新时间
    public DateTime UpdateDate { get; set; }
}
