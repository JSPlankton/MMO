using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using Observable = UniRx.Observable;

public class SceneMgr : Singleton<SceneMgr>
{

    public SceneType _sceneType;

    private LoadingView _loadingView;

    private IDisposable _dis;
    
    public void Init(LoadingView loadingView)
    {
        _loadingView = loadingView;
    }

    public void LoadScene(SceneType sceneType, Action callback)
    {
        _sceneType = sceneType;
        
        //异步加载场景
        var handle = Global.Instance.YooPackage.LoadSceneAsync($"{ConstDefine.ScenePath}{sceneType.ToString()}");

        if (handle != null && _loadingView != null)
        {
            _loadingView.transform.SetAsLastSibling();
            _loadingView.Show();

            _dis = Observable.EveryUpdate().Subscribe(_ =>
            {
                _loadingView.RefreshUI(handle.Progress, $"加载场景中::{(int)(handle.Progress * 100)} %");

                if (handle.Progress >= 1)
                {
                    callback?.Invoke();
                    _loadingView.Show(false);
                    _dis.Dispose();
                }
            });
        }
    }
}
