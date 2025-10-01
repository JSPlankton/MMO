﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[SugarTable("role", TableDescription = "角色表")]

internal class RoleTable
{

    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 IsPrimaryKey:表示是否是主键，IsIdentity:表示是否自增长
    public int Id { get; set; }

    //状态
    [SugarColumn(DefaultValue = "1", IsOnlyIgnoreInsert = true)]
    public byte State { get; set; }

    //用户ID
    public int AccountID { get; set; }

    //游戏币
    public int Money { get; set; }

    //昵称
    [SugarColumn(Length = 30)]
    public string Nickname { get; set; }

    //职业
    public int JobID { get; set; }

    //等级
    public int Level { get; set; }

    //经验
    public int Exp { get; set; }

    //技能升级点
    public int SkillUpPoint { get; set; }


    //最大生命值
    public int MaxHP { get; set; }


    //当前生命值
    public int CurrHP { get; set; }

    //最大法力值
    public int MaxMP { get; set; }

    //当前的法力值
    public int CurrMP { get; set; }

    //攻击力
    public int Atk { get; set; }

    //防御力
    public int Def { get; set; }

    //暴击
    public int Crit { get; set; }

    //闪避
    public int Dodeg { get; set; }

    //命中
    public int Hit { get; set; }

    //穿透
    public int Penet { get; set; }

    //角色当前位置
    [SugarColumn(Length = 50)]
    public string Pos { get; set; }


    //摄像机偏移
    [SugarColumn(Length = 50)]
    public string CameraOffset { get; set; }

    //角色当前所在的地图
    public int MapId { get; set; }


    //角色所属的服务器
    public int ServerId { get; set; }

    //创建时间
    public DateTime CreateDate { get; set; }

    //更新时间
    public DateTime UpdateDate { get; set; }


}
