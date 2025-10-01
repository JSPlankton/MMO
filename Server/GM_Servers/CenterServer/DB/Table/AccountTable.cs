using SqlSugar;
using System;
/// <summary>
/// 用户表，当用户注册时存放注册信息的表
/// </summary>


[SugarTable("account", TableDescription = "用户表")]
internal class AccountTable
{

    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 IsPrimaryKey:表示是否是主键，IsIdentity:表示是否自增长
    public int Id { get; set; }

    //状态
    [SugarColumn(DefaultValue = "1", IsOnlyIgnoreInsert = true)]
    public byte State { get; set; }

    //用户名
    [SugarColumn(Length = 30)]
    public string UserName { get; set; }

    //手机号
    [SugarColumn(Length = 15)]
    public string PhoneNum { get; set; }

    //密码
    [SugarColumn(Length = 30)]
    public string Passwrod { get; set; }

    //用户最后登录的服务器Id
    public int LastLoginServerId { get; set; }

    //创建时间
    public DateTime CreateDate { get; set; }

    //更新时间
    public DateTime UpdateDate { get; set; }

}
