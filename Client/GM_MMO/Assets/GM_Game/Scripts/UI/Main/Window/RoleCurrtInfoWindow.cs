using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

/**
 * Title:角色信息窗口
 * Description:头像信息，昵称，职业等级，HP,MP。
 */


public class RoleCurrtInfoWindow : WindowBase
{

    [SerializeField, Header("头像信息")] private Image _imgHead;
    [SerializeField, Header("角色昵称")] private TMP_Text _texNickname;
    [SerializeField, Header("角色职业等级信息")] private TMP_Text _texJobLevel;

    [SerializeField, Header("角色血量信息")] private Slider _sliderHP;
    [SerializeField, Header("角色法力值信息")] private Slider _sliderMP;


    [SerializeField, Header("技能槽父组件")] private Transform _skillSlotParent;


    private string[] _skillKey = { "Q", "E", "R", "F", "1", "2", "3", "4", "5", "6" };

    public override void InitWindow()
    {
        //加载Prefab资源   SkillSlotWidget
        Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.PrefabPath}UIPrefabs/SkillSlotWidget")
            .Completed += (AssetOperationHandle handle) =>
            {

                for (int i = 0; i < _skillKey.Length; i++)
                {
                    //生成SkillSlotWidget对象
                    GameObject go = handle.InstantiateSync();
                    if (go == null) { return; }
                    //设置父组件，位置，旋转，缩放
                    go.SetParent(_skillSlotParent);


                    SkillSlotWidget widget = go.GetComponent<SkillSlotWidget>();
                    if (widget != null)
                    {
                        widget.RefreshUI(_skillKey[i]);
                    }

                }

            };

    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    /// <param name="obj"></param>
    public override void RefreshUI(object obj)
    {

        //TODO  服务端返回角色相关的数据后， 才可以更新UI


    }



}
