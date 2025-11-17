using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class NetDefine
{
    public const string IPHost = "127.0.0.1";//本机IP
    public const int CenterServerPort = 10110;//中心服务器端口号
    public const int LoginServerPort = 10120;//登录服务器的端口号
    public const int GameServerPort = 10130;//游戏服务器的端口号
    public const int GateServerPort = 10140;//网关服务器的端口号

    public const ushort CMD_ErrCode = 10001;//错误码，

    public const ushort CMD_RegistCode = 11010;//注册请求码

    public const ushort CMD_LoginCode = 11020;//登录请求码

    public const ushort CMD_GetServerListCode = 11030;//获取服务器列表请求码

    public const ushort CMD_LoginGameServerCode = 11040;//登录游戏服务器请求码

    public const ushort CMD_CreateRoleCode = 11050;//创建角色请求码

    public const ushort CMD_StartGameCode = 11060;//开始游戏请求码

    public const ushort CMD_EnterWroldCode = 11070;//角色进入游戏世界请求码

    public const ushort CMD_RoleSkillInfoCode = 11080;//角色技能信息返回码

    public const ushort CMD_SyncRoleEnterWorld = 11090;//同步角色进入游戏世界数据

    public const ushort CMD_SyncOtherOnlineRole = 11100;//同步其他在线玩家的数据

    public const ushort CMD_RoleKnapsackInfoCode = 11110;//角色背包信息返回码

    public const ushort CMD_RoleWearEquipInfoCode = 11120;//角色穿戴的装备信息返回码

    public const ushort CMD_RoleBuyItemCode = 11130;//角色购买商品请求码

    public const ushort CMD_RoleExitGameCode = 11140;//角色退出游戏请求码

    public const ushort CMD_UpdateRoleInofCode = 11150;//更新角色信息请求码

    public const ushort CMD_UpdateRoleSkillInfoCode = 11160;//更新角色的技能信息请求码

    public const ushort CMD_UpdateRoleKnapsackInfoCode = 11170;//更新角色的背包信息请求码

    public const ushort CMD_UpdateRoleWearEquipInfoCode = 11180;//更新角色穿戴的装备信息请求码

    public const ushort CMD_RoleKnapsackSlotMoveCode = 11190;//角色移动背包格子请求码

    public const ushort CMD_RoleKnapsackItemSplitCode = 11200;//角色背包中物品的拆分请求码

    public const ushort CMD_RoleKnapsackClearUpCode = 11210;//角色背包整理请求码

    public const ushort CMD_RolePutOnEquipCode = 11220;//角色穿戴装备请求码

    public const ushort CMD_RoleUnloadEquipCode = 11230;//角色卸载装备请求码

    public const ushort CMD_RoleMoveCode = 11240;//角色移动请求码

    public const ushort CMD_SyncOtherRoleMove = 11250;//同步其他玩家移动

    public const ushort CMD_RoleChangeStateCode = 11260;//角色改变状态请求码

    public const ushort CMD_SyncOtherRoleChangeState = 11270;//同步其他玩家改变状态

    public const ushort CMD_SyncOtherRoleExitGame = 11280;//同步其他玩家退出游戏

    public const ushort CMD_MonsterGenerateCode = 11290;//生成怪兽返回码
}


/// <summary>
/// 连接状态
/// </summary>
public enum ConnState
{

    Connected,
    Disconnected,

}


/// <summary>
/// 客户端类型
/// </summary>
public enum ClientType
{
    Unity,
    LoginServer,
    GameServer,
    GateServer,
}