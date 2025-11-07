using TMPro;
using UnityEngine;
using YooAsset;
using static TMPro.TMP_Dropdown;

/**
 * Title:
 * Description:
 */


public class ChatWindow : WindowBase
{


    [SerializeField, Header("消息展示框")] private Transform _content;

    [SerializeField, Header("发送消息频道选择")] private TMP_Dropdown _dropdown;
    [SerializeField, Header("发送消息的内容")] private TMP_InputField _iptChat;


    private void Start()
    {

        _iptChat.onSelect.AddListener((string value) =>
        {
            //当消息输入框获取焦点时，就禁用输入事件
            PlayerInputCtrl.Instance.OnDisable();
        });

        _iptChat.onDeselect.AddListener((string value) =>
        {
            //当消息输入框失去焦点时，就启用输入事件
            PlayerInputCtrl.Instance.OnEnable();
        });

    }

    public void OnSendBrnClicked()
    {

        //1. 判断发送框内容是否为空
        if (string.IsNullOrEmpty(_iptChat.text))
        {
            TipsMgr.Instance.ShowSystemTips("请输入聊天内容..");
            return;
        }

        //2.获取聊天频道
        OptionData optionData = _dropdown.options[_dropdown.value];

        if (optionData == null)
        {
            return;
        }

        //发送数据到服务端验证.. todo


        //模拟默认发送消息成功
        Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.PrefabPath}UIPrefabs/ChatItemWidget")
            .Completed += (AssetOperationHandle handle) =>
            {


                GameObject go = handle.InstantiateSync();
                if (go == null) { return; }

                go.SetParent(_content);

                ChatItemWidget widget = go.GetComponent<ChatItemWidget>();
                if (widget != null)
                {
                    widget.RefreshUI(optionData.text, "昵称:", _iptChat.text);
                }

                _iptChat.text = "";
            };

    }

}
