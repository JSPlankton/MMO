using cfg;
using System;
using System.Collections.Generic;
using System.IO;


/// <summary>
/// 数据表管理类
/// </summary>
public class LubanMgr : Singleton<LubanMgr>
{
    private Dictionary<int, SkillInfo> _skillInfoDic;
    public void Init()
    {
        Tables tables = new Tables((string file) => new Luban.ByteBuf(File.ReadAllBytes($"F:/Unity/MMO/Server/GM_Servers/NetWork/LubanManager/Tb/{file}.bytes")));

        _skillInfoDic = tables.TbSkillInfo.DataMap;
    }

    #region 技能相关

    public Dictionary<int, SkillInfo> GetSkillInfos()
    {
        return _skillInfoDic;
    }

    public SkillInfo GetSkillInfoById(int skillId)
    {

        if (_skillInfoDic.ContainsKey(skillId))
        {
            return _skillInfoDic[skillId];
        }
        return null;
    }

    public Dictionary<int, SkillInfo> GetSkillInfosByJobId(int jobId)
    {
        Dictionary<int, SkillInfo> jobSkillInfos = new Dictionary<int, SkillInfo>();
        foreach (var item in _skillInfoDic)
        {
            if (item.Value.JobId == jobId)
            {
                jobSkillInfos.Add(item.Key, item.Value);
            }
        }

        return jobSkillInfos;
    }

    #endregion
}
