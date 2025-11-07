using UnityEngine;

/**
 * Title:主城场景相关的View
 * Description: 角色信息面板，聊天窗口，小地图，背包信息窗口，技能信息窗口，NPC等等相关的UI
 */


public class MainView : UIBase
{

    [SerializeField, Header("角色当前信息Window")] private RoleCurrtInfoWindow _roleCurrtInfoWindow;
    [SerializeField, Header("技能信息Window")] private SkillInfoWindow _skillInfoWindow;
    [SerializeField, Header("背包Window")] private KnapsackWindow _knapsackWindow;
    [SerializeField, Header("交谈相关的Window")] private TalkWindow _talkWindow;
    [SerializeField, Header("商品Window")] private ShopWindow _shopWindow;
    [SerializeField, Header("角色属性Window")] private RoleAttributeWindow _roleAttributeWindow;

    public override void InitView()
    {
        base.InitView();

        windowDic[WindowType.RoleCurrtInfowWindow] = _roleCurrtInfoWindow;
        windowDic[WindowType.SkillInfoWindow] = _skillInfoWindow;
        windowDic[WindowType.KnapsackWindow] = _knapsackWindow;
        windowDic[WindowType.TalkWindow] = _talkWindow;
        windowDic[WindowType.ShopWindow] = _shopWindow;
        windowDic[WindowType.RoleAttributeWindow] = _roleAttributeWindow;


        _roleCurrtInfoWindow.InitWindow();

    }


}
