using System;
using UnityEngine;
using YooAsset;

/**
 * Title:
 * Description:
 */


public class ShopWindow : WindowBase
{

    [SerializeField, Header("商品列表父组件")] private Transform _content;


    private void Start()
    {

        //模拟生成商品Item

        AddGoodsItem();


    }

    /// <summary>
    /// 添加商品
    /// </summary>
    private void AddGoodsItem()
    {

        Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.PrefabPath}UIPrefabs/GoodsItemWidget")
            .Completed += (AssetOperationHandle handle) =>
            {


                //模拟生成20条数据
                for (int i = 0; i < 20; i++)
                {

                    GameObject go = handle.InstantiateSync();
                    go.SetParent(_content);

                    GoodsItemWidget widget = go.GetComponent<GoodsItemWidget>();
                    if (widget != null)
                    {
                        widget.RefreshUI();
                    }
                }

            };
    }
}
