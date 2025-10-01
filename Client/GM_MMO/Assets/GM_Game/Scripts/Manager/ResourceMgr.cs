using System;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

/**
 * Title:
 * Description:
 */


public class ResourceMgr : Singleton<ResourceMgr> {


    private Dictionary<string, AssetOperationHandle> prefabDic = new Dictionary<string, AssetOperationHandle>();

    /// <summary>
    /// º”‘ÿPrefab
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadPrefabAsync(string path, Action<GameObject> callback) {

        if (prefabDic.ContainsKey(path)) {
            callback?.Invoke(prefabDic[path].InstantiateSync());
        }
        else {
            Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.PrefabPath}{path}")
               .Completed += (AssetOperationHandle handle) => {
                   GameObject go = handle.InstantiateSync();

                   if (!prefabDic.ContainsKey(path)) {
                       prefabDic.Add(path, handle);
                   }

                   callback?.Invoke(go);
               };
        }



    }



}
