using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 角色技能数据库表 用于存储角色已经学会或未学会的技能
/// </summary>

[SugarTable("role_skill", tableDescription:"角色技能表")]
internal class RoleSkillTable
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 IsPrimaryKey:表示是否是主键，IsIdentity:表示是否自增长
    public int Id { get; set; }

    //状态
    [SugarColumn(DefaultValue = "1", IsOnlyIgnoreInsert = true)]
    public byte State { get; set; }

    //角色ID
    public int RoleId { get; set; }

    //技能ID
    public int SkillId { get; set; }
    //技能等级
    public int Level { get; set; }
    //绑定的键盘按键
    public string BindKey { get; set; }

    //创建时间
    public DateTime CreateDate { get; set; }

    //更新时间
    public DateTime UpdateDate { get; set; }
}