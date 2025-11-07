using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Title: 管理所有的Tips
 * Description:
 */


public class TipsMgr : Singleton<TipsMgr>
{



    public void ShowSystemTips(string msg)
    {

        ResourceMgr.Instance.LoadPrefabAsync("UIPrefabs/TipsDialog/SystemTips", (GameObject go) =>
        {
            if (go == null) { return; }

            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.localPosition = new Vector2(0, 160);
            go.transform.localScale = Vector3.one;

            SystemTips tips = go.GetComponent<SystemTips>();
            if (tips != null)
            {

                tips.RefreshUI(msg);
            }

        });


    }

    public void ShowBuyGoodsDialog()
    {

        ResourceMgr.Instance.LoadPrefabAsync("UIPrefabs/TipsDialog/BuyDialog", (GameObject go) =>
        {
            if (go == null) { return; }

            go.SetParent(GameObject.Find("Canvas").transform);

        });

    }


    private List<GameObject> _itemTipsLst = new List<GameObject>();

    public void ShowItemTips(Vector3 mousePos)
    {

        ResourceMgr.Instance.LoadPrefabAsync("UIPrefabs/TipsDialog/EquipItemTips", (GameObject go) =>
        {
            if (go == null) { return; }
            _itemTipsLst.Add(go);

            go.SetParent(GameObject.Find("Canvas").transform);

            RectTransform rectTrans = go.transform as RectTransform;

            Vector2 pos = UIRoot.Instance.ScreenPointToViewPoint(mousePos);

            pos.y -= rectTrans.rect.height / 2;
            pos.x += rectTrans.rect.width / 2;

            rectTrans.anchoredPosition = pos;
        });

    }

    internal void CloseItemTips()
    {

        if (_itemTipsLst.Count <= 0) { return; }


        for (int i = 0; i < _itemTipsLst.Count; i++)
        {
            GameObject.Destroy(_itemTipsLst[i]);
            _itemTipsLst.RemoveAt(i);
        }


    }
}
