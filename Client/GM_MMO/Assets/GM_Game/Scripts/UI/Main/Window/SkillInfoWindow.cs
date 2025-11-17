using Google.Protobuf.Collections;
using TMPro;
using UnityEngine;
using YooAsset;

/**
 * Title:
 * Description:
 */


public class SkillInfoWindow : WindowBase
{

    [SerializeField, Header("技能信息列表父组件")] private Transform _content;

    [SerializeField, Header("职业")] private TMP_Text _texJob;
    [SerializeField, Header("技能升级点")] private TMP_Text _texPoint;


    //模拟生成技能Item
    private void Start()
    {

    }


    public override void RefreshUI(object obj)
    {

        MainRoleInfo mainRoleInfo = Global.Instance.MRInfo;
        if (mainRoleInfo != null)
        {
            string jobStr = "";
            if (mainRoleInfo.BaseInfo.JobId == 1)
            {
                jobStr = "剑修";
            }

            _texJob.SetText(jobStr);
            _texPoint.SetText($"{mainRoleInfo.SkillUpPoint}");
        }

        //根据服务端返回角色所学的技能信息 来更新UI.. todo

        RepeatedField<RoleSkillInfo> roleSkillInfoLst = obj as RepeatedField<RoleSkillInfo>;
        if (roleSkillInfoLst != null)
        {


            Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.PrefabPath}UIPrefabs/SkillItenWidget")
                .Completed += (AssetOperationHandle handle) =>
            {

                for (int i = 0; i < roleSkillInfoLst.Count; i++)
                {
                    GameObject go = handle.InstantiateSync();
                    if (go == null) { return; }

                    go.SetParent(_content);

                    SkillItemWidget widget = go.GetComponent<SkillItemWidget>();
                    if (widget != null)
                    {
                        widget.RefreshUI(roleSkillInfoLst[i]);
                    }
                }
            };
        }

    }





}