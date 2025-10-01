using Google.Protobuf;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Title: 登录窗口
 * Description:
 */


public class LoginWindow : WindowBase
{

    [SerializeField, Header("账号输入框")] private TMP_InputField _iptAcct;
    [SerializeField, Header("密码输入框")] private TMP_InputField _iptPasd;
    [SerializeField, Header("记住账号Toggle")] private Toggle _togRememberAcct;
    [SerializeField, Header("用户协议Toggle")] private Toggle _togAgreement;


    private void Awake()
    {

        //用户协议
        int agreement = PlayerPrefs.GetInt("Agreement");
        if (agreement == 1)
        {
            _togAgreement.isOn = true;
        }

        //是否记住账号
        string acct = PlayerPrefs.GetString("PlayerAccount");
        if (!string.IsNullOrEmpty(acct))
        {
            _togRememberAcct.isOn = true;
            _iptAcct.text = acct;
        }
    }


    public void OnGotoRegistBtnClicked()
    {
        //跳转注册Window.
        UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.RegistWindow);
        // UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.ServerListWindow);
    }

    public void OnLoginBtnClicked()
    {

        //1.判断输入框是否为空，
        if (string.IsNullOrEmpty(_iptAcct.text))
        {
            Debug.Log("账号为空..");
            TipsMgr.Instance.ShowSystemTips("账号不能为空..");
            return;
        }
        if (string.IsNullOrEmpty(_iptPasd.text))
        {
            TipsMgr.Instance.ShowSystemTips("密码不能为空..");
            Debug.Log("密码为空..");
            return;
        }

        //2.判断是否勾选了用户协议，如果有勾选，则把它保存在本地，

        if (!_togAgreement.isOn)
        {
            TipsMgr.Instance.ShowSystemTips("请阅读并勾选用户协议..");
            Debug.Log("没有同意用户协议..");
            return;
        }

        PlayerPrefs.SetInt("Agreement", 1);

        //3.判断是否勾选了记住账号Toglle, 如果有勾选，则保存在本地，

        if (_togRememberAcct.isOn)
        {
            PlayerPrefs.SetString("PlayerAccount", _iptAcct.text);
        }
        else
        {
            PlayerPrefs.SetString("PlayerAccount", "");
        }

        //PlayerPrefs:: 它是Unity官方提供的用于存储和读取数据的类，他实现了数据持久化，
        //把数据保存硬盘，需要的时候再去读取，这个过程就叫数据的持久化，
        //可以存储3种数据类型，float,int,sting,以键值对的形式存储。

        PlayerPrefs.Save();

        //4.服务器验证， 只有服务器验证通过了才可以登录，
        //TODO

        LoginReq req = new LoginReq()
        {
            UserName = _iptAcct.text,
            Password = _iptPasd.text,
        };

        NetSocketMgr.Client.SendData(NetDefine.CMD_LoginCode, req.ToByteString());

        //Debug.Log("登录成功..");
        ////跳转选择服务器界面
        //UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.GameServerWindow);

    }



}
