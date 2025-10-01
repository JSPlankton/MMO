using Google.Protobuf;
using TMPro;
using UnityEngine;

/**
 * Title:ע�ᴰ��
 * Description:
 */


public class RegistWindow : WindowBase
{

    [SerializeField, Header("�˺������")] private TMP_InputField _iptAcct;
    [SerializeField, Header("�ֻ����������")] private TMP_InputField _iptMobile;
    [SerializeField, Header("��֤�������")] private TMP_InputField _iptVerify;
    [SerializeField, Header("���������")] private TMP_InputField _iptPasd;
    [SerializeField, Header("ȷ�����������")] private TMP_InputField _iptSurePasd;

    public void OnRegistBtnClicked()
    {

        //1.�ж�������Ƿ�Ϊ�ա�
        if (string.IsNullOrEmpty(_iptAcct.text))
        {
            Debug.Log("�˺�Ϊ��..");
            return;
        }

        if (string.IsNullOrEmpty(_iptMobile.text))
        {
            Debug.Log("�ֻ�����Ϊ��..");
            return;
        }

        if (string.IsNullOrEmpty(_iptVerify.text))
        {
            Debug.Log("��֤��Ϊ��..");
            return;
        }

        if (string.IsNullOrEmpty(_iptPasd.text))
        {
            Debug.Log("����Ϊ��..");
            return;
        }

        if (string.IsNullOrEmpty(_iptSurePasd.text))
        {
            Debug.Log("ȷ������Ϊ��..");
            return;
        }

        //2.��֤�˺ţ��ֻ����룬����ĺϷ��ԡ�

        //3.�ж������ȷ���Ƿ�һ�£�
        if (!_iptPasd.text.Equals(_iptSurePasd.text))
        {
            Debug.Log("������������벻һ��..");
            return;
        }



        //4����ʼע��
        //TODO
        //  Debug.Log("ע��ɹ�..");
        //Show(false);

        RegistReq req = new RegistReq()
        {
            UserName = _iptAcct.text,
            PhoneNum = _iptMobile.text,
            Password = _iptPasd.text,
        };

        NetSocketMgr.Client.SendData(NetDefine.CMD_RegistCode, req.ToByteString());

    }

    public void OnVerifyCodeBtnClicked()
    {

        Debug.Log("��ȡ��֤��ɹ�..");
    }

    public void OnBackBtnClicked()
    {
        UIRoot.Instance.LoginViewCtrl.ShowWindow(WindowType.LoginWindow);
    }


}
