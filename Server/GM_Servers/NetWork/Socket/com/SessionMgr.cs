using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class SessionMgr : Singleton<SessionMgr>
{
    private int _instanceInter;
    private Dictionary<int, Session> _sessionDic = new Dictionary<int, Session>();

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="session"></param>
    /// <param name="sessionId"></param>
    public void AddSession(Session session, int sessionId = -1)
    {

        if (sessionId <= 0)
        {
            sessionId = GteInstanceInter();
        }


        if (!_sessionDic.ContainsKey(sessionId))
        {
            session.SessionId = sessionId;
            _sessionDic.Add(sessionId, session);
        }
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sessionId"></param>
    public void RemoveSession(int sessionId)
    {

        if (_sessionDic.ContainsKey(sessionId))
        {
            _sessionDic.Remove(sessionId);
        }
    }

    /// <summary>
    /// 根据Id获取session
    /// </summary>
    /// <param name="sessionId"></param>
    /// <returns></returns>
    public Session GetSession(int sessionId)
    {
        if (_sessionDic.ContainsKey(sessionId))
        {
            return _sessionDic[sessionId];
        }
        return null;
    }

    /// <summary>
    /// 返回所有的连接Session
    /// </summary>
    /// <returns></returns>
    public int GetSessionCount()
    {
        return _sessionDic.Count;
    }


    public int GteInstanceInter()
    {
        return Interlocked.Increment(ref _instanceInter);
    }


}
