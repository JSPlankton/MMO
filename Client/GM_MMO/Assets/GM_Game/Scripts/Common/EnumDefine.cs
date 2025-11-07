
/// <summary>
/// 窗口类型
/// </summary>
public enum WindowType
{

    LoginWindow,//登录窗口
    RegistWindow,//注册窗口
    GameServerWindow,//服务器窗口
    ServerListWindow,//服务器列表窗口
    CreateRoleWindow,//创建角色Window
    SelectRoleWindow,//选择角色Window
    RoleCurrtInfowWindow,//角色当前信息window
    SkillInfoWindow,//技能信息window
    KnapsackWindow,//背包window
    TalkWindow,//交谈相关window
    ShopWindow,//商品window
    RoleAttributeWindow,//角色属性window
}

/// <summary>
/// 角色状态
/// </summary>
public enum RoleState
{
    Idle,
    Run,
    FastRun,
    Jump,
    Slider,
    Attck,
    Hit,
}


/// <summary>
/// 角色的类型
/// </summary>
public enum RoleType
{

    MainRole,
    Monster,
    NPC,
    OtherRole,//其他玩家

}

/// <summary>
/// 拖拽的类型
/// </summary>
public enum DragType
{

    KanpsackSlot,//拖拽的是背包Slot

}