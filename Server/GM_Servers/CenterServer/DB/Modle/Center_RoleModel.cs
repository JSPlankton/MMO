using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Center_RoleModel
{

    SqlSugarClient _db;
    public Center_RoleModel(SqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 获取角色穿戴的装备信息
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal RoleWearEquipInfoRet RoleWearEquipInfo(EnterWroldReq req)
    {
        RoleWearEquipInfoRet ret = new RoleWearEquipInfoRet();
        //获取角色穿戴装备表里面的数据
        //RoleWearEquipTable roleWearEquipTable = _db.Queryable<RoleWearEquipTable>().Where(v => v.RoleId == req.RoleId).First();
        //if (roleWearEquipTable != null)
        //{
        //    ret.RoleId = req.RoleId;
        //    //以|进行分割出来的是 所有的装备格子数据
        //    string[] slotArr = roleWearEquipTable.WearEquip.Split('|');
        //    for (int i = 0; i < slotArr.Length; i++)
        //    {
        //        string[] dataArr = slotArr[i].Split(',');
        //        //每个装备格子数据
        //        RoleWearEquipSlot slot = new RoleWearEquipSlot()
        //        {
        //            SlotNo = i,
        //            ItemId = int.Parse(dataArr[0]),
        //            Type = int.Parse(dataArr[1])
        //        };
        //        ret.RoleWearEquipLst.Add(slot);
        //    }
        //}
        //else
        //{
        //    ret.CmdCode = CmdCode.RoleNotExist;
        //}
        return ret;
    }

    /// <summary>
    /// 获取角色的背包信息
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal RoleKnapsackInfoRet RoleKnapsackInfo(EnterWroldReq req)
    {
        RoleKnapsackInfoRet ret = new RoleKnapsackInfoRet();

        //RoleKnapsackTable roleKnapsackTable = _db.Queryable<RoleKnapsackTable>().Where(v => v.RoleId == req.RoleId).First();
        //if (roleKnapsackTable != null)
        //{
        //    ret.RoleId = req.RoleId;
        //    //以|进行分割出来的是 所有的背包格子数据
        //    string[] slotArr = roleKnapsackTable.Knapsack.Split('|');
        //    for (int i = 0; i < slotArr.Length; i++)
        //    {
        //        string[] dataArr = slotArr[i].Split(',');
        //        //每个背包格子数据
        //        RoleKnapsackSlot slot = new RoleKnapsackSlot()
        //        {
        //            SlotNo = i,
        //            ItemId = int.Parse(dataArr[0]),
        //            Count = int.Parse(dataArr[1])
        //        };
        //        ret.RoleKnapsackSlotLst.Add(slot);
        //    }

        //}
        //else
        //{
        //    ret.CmdCode = CmdCode.RoleNotExist;
        //}
        return ret;
    }

    /// <summary>
    /// 角色技能信息
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    internal RoleSkillInfoRet RoleSkillInfo(EnterWroldReq req)
    {
        RoleSkillInfoRet ret = new RoleSkillInfoRet();

        //查询角色的技能信息
        List<RoleSkillTable> roleSkillTables = _db.Queryable<RoleSkillTable>().Where(v => v.RoleId == req.RoleId).ToList();
        if (roleSkillTables != null && roleSkillTables.Count > 0)
        {
            ret.RoleId = req.RoleId;
            for (int i = 0; i < roleSkillTables.Count; i++)
            {
                RoleSkillInfo roleSkillInfo = new RoleSkillInfo()
                {
                    SkillId = roleSkillTables[i].SkillId,
                    Level = roleSkillTables[i].Level,
                    BindKey = roleSkillTables[i].BindKey,
                };

                ret.RoleSkillInfoLst.Add(roleSkillInfo);
            }
        }
        else
        {
            ret.CmdCode = CmdCode.RoleNotExist;
        }


        return ret;
    }


}
