using UnityEngine;

/**
 * Title: 管理所有的Tips
 * Description:
 */


public class TipsMgr : Singleton<TipsMgr> {



    public void ShowSystemTips(string msg) {

        ResourceMgr.Instance.LoadPrefabAsync("UIPrefabs/SystemTips", (GameObject go) => {
            if (go == null) { return; }

            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.localPosition = new Vector2(0, 160);
            go.transform.localScale = Vector3.one;

            SystemTips tips = go.GetComponent<SystemTips>();
            if (tips != null) {

                tips.RefreshUI(msg);
            }

        });


    }



}
