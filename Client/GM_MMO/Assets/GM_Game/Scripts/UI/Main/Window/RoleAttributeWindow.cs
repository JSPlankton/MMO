using System;
using UnityEngine;
using UnityEngine.EventSystems;
using YooAsset;

/**
 * Title:
 * Description:
 */


public class RoleAttributeWindow : WindowBase, IDragHandler
{


    [SerializeField, Header("已穿戴的装备Slot的父组件")] private Transform _transEquips;

    private RectTransform _transRect;


    private void Start()
    {
        _transRect = transform as RectTransform;
        //生成装备Slot
        AddWeraEquipSlot();

    }

    /// <summary>
    /// 添加穿戴装备slot
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void AddWeraEquipSlot()
    {


        Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.PrefabPath}UIPrefabs/EquipSlotWidget")
            .Completed += (AssetOperationHandle handle) =>
            {

                //模拟生成10个装备Slot

                for (int i = 0; i < 10; i++)
                {

                    GameObject go = handle.InstantiateSync();
                    if (go == null) { return; }

                    go.SetParent(_transEquips);
                }
            };
    }

    /// <summary>
    /// 拖拽中事件
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnDrag(PointerEventData eventData)
    {
        _transRect.anchoredPosition += eventData.delta;
    }
}
