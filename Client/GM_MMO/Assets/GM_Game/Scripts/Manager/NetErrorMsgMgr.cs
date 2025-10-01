using Google.Protobuf;
using System;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class NetErrorMsgMgr : Singleton<NetErrorMsgMgr>
{

    public void Init()
    {

        //ע����������Ϣ
        SocketDispatcher.Instance.AddEventHandler(NetDefine.CMD_ErrCode, OnErrorMsgHandle);
    }

    /// <summary>
    /// ���������Ϣ
    /// </summary>
    /// <param name="data"></param>
    private void OnErrorMsgHandle(ByteString data)
    {

        ErrMsg errMsg = ErrMsg.Parser.ParseFrom(data);
        switch (errMsg.CmdCode)
        {
            case CmdCode.AcctExist:
                TipsMgr.Instance.ShowSystemTips("�˺��Ѵ���..");
                break;
            case CmdCode.ServerError:
                TipsMgr.Instance.ShowSystemTips("����˴���..");
                break;
            case CmdCode.AcctNotExist:
                TipsMgr.Instance.ShowSystemTips("�˺Ų�����..");
                break;
            case CmdCode.PasswordError:
                TipsMgr.Instance.ShowSystemTips("�������..");
                break;
            case CmdCode.AcctDisable:
                TipsMgr.Instance.ShowSystemTips("�˺��ѽ���..");
                break;
            case CmdCode.ReqParamError:
                TipsMgr.Instance.ShowSystemTips("�����������..");
                break;
            case CmdCode.NicknameExist:
                TipsMgr.Instance.ShowSystemTips("�ǳ��Ѿ�����..");
                break;
            case CmdCode.UserNameIllegal:
                TipsMgr.Instance.ShowSystemTips("�û������Ϸ�..");
                break;
            case CmdCode.PhoneNumIllegal:
                TipsMgr.Instance.ShowSystemTips("�ֻ��Ų��Ϸ�..");
                break;
            case CmdCode.PasswordIllegal:
                TipsMgr.Instance.ShowSystemTips("���벻�Ϸ�..");
                break;
            case CmdCode.UserOftenLogin:
                TipsMgr.Instance.ShowSystemTips("�û�Ƶ����¼,���Ե�..");
                break;

        }

    }
}
