using Google.Protobuf;
using System.Collections.Generic;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public delegate void OnActionHandler(ByteString data);

public class SocketDispatcher : Singleton<SocketDispatcher>
{

    private Dictionary<int, OnActionHandler> _actionDic = new Dictionary<int, OnActionHandler>();

    /// <summary>
    /// ע���¼�
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void AddEventHandler(int protoCode, OnActionHandler handler)
    {
        if (!_actionDic.ContainsKey(protoCode) && handler != null)
        {
            _actionDic.Add(protoCode, handler);
        }
    }

    /// <summary>
    /// ɾ���¼�
    /// </summary>
    /// <param name="protoCode"></param>
    public void RemoveEventHandler(int protoCode)
    {
        if (_actionDic.ContainsKey(protoCode))
        {
            _actionDic.Remove(protoCode);
        }
    }

    /// <summary>
    /// �ɷ��¼�
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="data"></param>
    public void DispatcherEvent(int protoCode, ByteString data)
    {
        if (_actionDic.ContainsKey(protoCode))
        {
            _actionDic[protoCode]?.Invoke(data);
        }
    }



}
