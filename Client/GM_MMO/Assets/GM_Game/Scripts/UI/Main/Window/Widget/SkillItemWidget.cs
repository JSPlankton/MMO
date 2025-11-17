using cfg;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Title:
 * Description:
 */


public class SkillItemWidget : MonoBehaviour
{

    [SerializeField, Header("技能图标")] private Image _imgIcon;
    [SerializeField, Header("技能Mask")] private Image _imgMask;

    [SerializeField, Header("技能名称")] private TMP_Text _texSkillName;
    [SerializeField, Header("技能等级")] private TMP_Text _texSkillLevel;
    [SerializeField, Header("技能简介")] private TMP_Text _texSkillDesc;


    public void RefreshUI(RoleSkillInfo roleSkillInfo)
    {

        if (roleSkillInfo != null)
        {


            if (roleSkillInfo.Level > 0)
            {
                _imgMask.gameObject.Show(false);
            }

            _texSkillLevel.SetText($"技能等级:{roleSkillInfo.Level}");


            SkillInfo skillInfo = LubanMgr.Instance.GetSkillInfoById(roleSkillInfo.SkillId);
            if (skillInfo != null)
            {
                //设置技能的图标
                ResourceMgr.Instance.LoadSpriteAsync(skillInfo.Icon, (Sprite sprite) =>
                {
                    if (sprite == null) { return; }
                    _imgIcon.sprite = sprite;
                });


                _texSkillName.SetText(skillInfo.Name);
                _texSkillDesc.SetText(skillInfo.Desc);

            }


        }


    }


}