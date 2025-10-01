using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Title:选择角色Window
 * Description:
 */


public class SelectRoleWindow : WindowBase
{


    [SerializeField, Header("头像图片")] private Image _imgHead;
    [SerializeField, Header("昵称")] private TMP_Text _texNickname;
    [SerializeField, Header("等级和职业")] private TMP_Text _texJobLevel;


    public override void RefreshUI(object obj)
    {
        CreateRoleRet createRole = obj as CreateRoleRet;
        if (createRole != null)
        {
            _texNickname.SetText(createRole.Nickname);

            string jobStr = "";
            if (createRole.JobId == 1)
            {

                jobStr = "剑修";
            } // == 2   todo


            _texJobLevel.SetText($"职业:{jobStr}    Lv.{createRole.Level}");


        }
    }

    public void OnStartGameBtnClicked()
    {

        //进入主城， 开始真正的游戏. todo

    }


}