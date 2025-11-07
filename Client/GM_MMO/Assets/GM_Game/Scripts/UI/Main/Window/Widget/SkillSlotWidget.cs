using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Title:
 * Description:
 */


public class SkillSlotWidget : MonoBehaviour
{

    [SerializeField, Header("技能图标")] private Image _imgIcon;

    [SerializeField, Header("技能绑定的按键")] private TMP_Text _texKey;
    [SerializeField, Header("技能CD Mask")] private Image _imgMask;
    [SerializeField, Header("技能C")] private TMP_Text _texCD;


    public void RefreshUI(string key)
    {

        //todo 因为需要以后配置了技能表后，再来完善

        _texKey.SetText(key);

        //默认隐藏
        _imgMask.gameObject.Show(false);

    }


}
