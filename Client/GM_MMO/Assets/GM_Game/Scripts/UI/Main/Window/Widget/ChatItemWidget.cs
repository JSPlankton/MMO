using TMPro;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class ChatItemWidget : MonoBehaviour
{

    [SerializeField, Header("聊天频道")] private TMP_Text _texChannel;
    [SerializeField, Header("昵称")] private TMP_Text _texNickname;
    [SerializeField, Header("聊天消息")] private TMP_Text _texMsg;


    public void RefreshUI(string channel, string nickname, string msg)
    {
        _texChannel.SetText(channel);
        _texNickname.SetText(nickname);
        _texMsg.SetText(msg);

    }

}
