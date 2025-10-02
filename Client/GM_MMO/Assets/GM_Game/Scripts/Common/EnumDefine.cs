
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
}