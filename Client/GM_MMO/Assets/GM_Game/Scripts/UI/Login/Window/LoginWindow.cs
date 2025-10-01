using Google.Protobuf;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Title: ��¼����
 * Description:
 */


public class LoginWindow : WindowBase
{

    [SerializeField, Header("�˺������")] private TMP_InputField _iptAcct;
    [SerializeField, Header("���������")] private TMP_InputField _iptPasd;
    [SerializeField, Header("��ס�˺�Toggle")] private Toggle _togRememberAcct;
    [SerializeField, Header("�û�Э��Toggle")] private Toggle _togAgreement;


    private void Awake()
    {

        //�û�Э��
        int agreement = PlayerPrefs.GetInt("Agreement");
        if (agreement == 1)
        {
            _togAgreement.isOn = true;
        }

        //�Ƿ��ס�˺�
        string acct = PlayerPrefs.GetString("PlayerAccount");
        if (!string.IsNullOrEmpty(acct))
        {
            _togRememberAcct.isOn = true;
            _iptAcct.text = acct;
        }
    }


    public void OnGotoRegistBtnClicked()
    {
        //��תע��Window.
        UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.RegistWindow);
        // UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.ServerListWindow);
    }

    public void OnLoginBtnClicked()
    {

        //1.�ж�������Ƿ�Ϊ�գ�
        if (string.IsNullOrEmpty(_iptAcct.text))
        {
            Debug.Log("�˺�Ϊ��..");
            TipsMgr.Instance.ShowSystemTips("�˺Ų���Ϊ��..");
            return;
        }
        if (string.IsNullOrEmpty(_iptPasd.text))
        {
            TipsMgr.Instance.ShowSystemTips("���벻��Ϊ��..");
            Debug.Log("����Ϊ��..");
            return;
        }

        //2.�ж��Ƿ�ѡ���û�Э�飬����й�ѡ������������ڱ��أ�

        if (!_togAgreement.isOn)
        {
            TipsMgr.Instance.ShowSystemTips("���Ķ�����ѡ�û�Э��..");
            Debug.Log("û��ͬ���û�Э��..");
            return;
        }

        PlayerPrefs.SetInt("Agreement", 1);

        //3.�ж��Ƿ�ѡ�˼�ס�˺�Toglle, ����й�ѡ���򱣴��ڱ��أ�

        if (_togRememberAcct.isOn)
        {
            PlayerPrefs.SetString("PlayerAccount", _iptAcct.text);
        }
        else
        {
            PlayerPrefs.SetString("PlayerAccount", "");
        }

        //PlayerPrefs:: ����Unity�ٷ��ṩ�����ڴ洢�Ͷ�ȡ���ݵ��࣬��ʵ�������ݳ־û���
        //�����ݱ���Ӳ�̣���Ҫ��ʱ����ȥ��ȡ��������̾ͽ����ݵĳ־û���
        //���Դ洢3���������ͣ�float,int,sting,�Լ�ֵ�Ե���ʽ�洢��

        PlayerPrefs.Save();

        //4.��������֤�� ֻ�з�������֤ͨ���˲ſ��Ե�¼��
        //TODO

        LoginReq req = new LoginReq()
        {
            UserName = _iptAcct.text,
            Password = _iptPasd.text,
        };

        NetSocketMgr.Client.SendData(NetDefine.CMD_LoginCode, req.ToByteString());

        //Debug.Log("��¼�ɹ�..");
        ////��תѡ�����������
        //UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.GameServerWindow);

    }



}
