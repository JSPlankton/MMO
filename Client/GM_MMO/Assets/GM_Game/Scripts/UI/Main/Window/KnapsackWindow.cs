using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using YooAsset;

/**
 * Title:背包模块
 * Description:背包window和slot UI，背包window的拖拽，物品的交换
 */


public class KnapsackWindow : WindowBase, IDragHandler
{

    [SerializeField, Header("背包Slot的父组件")] private Transform _content;

    [SerializeField, Header("金币")] private TMP_Text _texGold;
    [SerializeField, Header("灵石")] private TMP_Text _texLS;


    private RectTransform _rectTransform;

    //模拟物品图片
    string[] _spriteNames = { "Item_2001", "Item_2002", "item_2003", "item_2005", "Item_2201", "Item_2202",
        "Item_2301", "Item_2401", "Item_2501", "Item_2601", };

    private void Start()
    {
        _rectTransform = transform as RectTransform;
        //模拟生成多个背包Slot对象

        AddItemSlot();

    }

    private void AddItemSlot()
    {
        Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.PrefabPath}UIPrefabs/KnapsackSlotWidget")
            .Completed += (AssetOperationHandle handle) =>
            {

                //模拟生成100个Slot对象
                for (int i = 0; i < 100; i++)
                {
                    GameObject go = handle.InstantiateSync();
                    if (go == null) { return; }

                    go.SetParent(_content);

                    KnapsackSlotWidget widget = go.GetComponent<KnapsackSlotWidget>();
                    if (widget != null)
                    {
                        //模拟10个物品信息数据
                        if (i < 10)
                        {
                            widget.RefreshUI(i + 1, _spriteNames[i]);
                        }
                        else
                        {
                            widget.RefreshUI(0, "");
                        }

                    }
                }
            };

    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    /// <param name="obj"></param>
    public override void RefreshUI(object obj)
    {

    }

    /// <summary>
    /// 拖拽中的事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {

        //PointerEventData 鼠标指针相关的数据

        //eventData.delta 鼠标指针的偏移
        _rectTransform.anchoredPosition += eventData.delta;



    }
}
