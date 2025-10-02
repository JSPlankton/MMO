using System;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

/**
 * Title:
 * Description:
 */


public class ResourceMgr : Singleton<ResourceMgr>
{


    private Dictionary<string, AssetOperationHandle> _prefabDic = new Dictionary<string, AssetOperationHandle>();
    private Dictionary<string, AssetOperationHandle> _effectDic = new Dictionary<string, AssetOperationHandle>();



    /// <summary>
    /// 加载Prefab
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadPrefabAsync(string path, Action<GameObject> callback)
    {

        if (_prefabDic.ContainsKey(path))
        {
            callback?.Invoke(_prefabDic[path].InstantiateSync());
        }
        else
        {
            Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.PrefabPath}{path}")
               .Completed += (AssetOperationHandle handle) =>
               {
                   GameObject go = handle.InstantiateSync();

                   if (!_prefabDic.ContainsKey(path))
                   {
                       _prefabDic.Add(path, handle);
                   }

                   callback?.Invoke(go);
               };
        }
    }

    /// <summary>
    /// 加载特效
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadEffectbAsync(string path, Action<GameObject> callback)
    {

        if (_effectDic.ContainsKey(path))
        {
            callback?.Invoke(_effectDic[path].InstantiateSync());
        }
        else
        {
            Global.Instance.YooPackage.LoadAssetAsync($"{ConstDefine.EffectPath}{path}")
               .Completed += (AssetOperationHandle handle) =>
               {
                   GameObject go = handle.InstantiateSync();

                   if (!_effectDic.ContainsKey(path))
                   {
                       _effectDic.Add(path, handle);
                   }

                   callback?.Invoke(go);
               };
        }
    }



}
