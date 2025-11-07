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


    public void RefreshUI()
    {


    }


}
