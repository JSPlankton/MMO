using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Title:ѡ���ɫWindow
 * Description:
 */


public class SelectRoleWindow : WindowBase
{


    [SerializeField, Header("ͷ��ͼƬ")] private Image _imgHead;
    [SerializeField, Header("�ǳ�")] private TMP_Text _texNickname;
    [SerializeField, Header("�ȼ���ְҵ")] private TMP_Text _texJobLevel;


    public override void RefreshUI(object obj)
    {
        CreateRoleRet createRole = obj as CreateRoleRet;
        if (createRole != null)
        {
            _texNickname.SetText(createRole.Nickname);

            string jobStr = "";
            if (createRole.JobId == 1)
            {

                jobStr = "����";
            } // == 2   todo


            _texJobLevel.SetText($"ְҵ:{jobStr}    Lv.{createRole.Level}");


        }
    }

    public void OnStartGameBtnClicked()
    {

        //�������ǣ� ��ʼ��������Ϸ. todo

    }


}