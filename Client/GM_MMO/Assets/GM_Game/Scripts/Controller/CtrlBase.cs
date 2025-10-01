using System;
using UnityEngine;

/**
 * Title:������Base��.
 * Description:
 */


public class CtrlBase : IDisposable
{

    protected UIBase _view;

    public CtrlBase(UIBase view)
    {
        _view = view;
    }

    /// <summary>
    /// �Ƿ���ʾView
    /// </summary>
    /// <param name="isShow"></param>
    public virtual void ShowView(bool isShow = true)
    {
        _view.Show(isShow);
    }

    /// <summary>
    /// ��ʾWindow
    /// </summary>
    /// <param name="windowType"></param>
    public virtual void ShowWindow(WindowType windowType, object obj = null)
    {

        if (!_view.gameObject.activeSelf) { _view.Show(); }

        _view.ShowWindow(windowType, obj);
    }





    public void Dispose()
    {
    }
}
