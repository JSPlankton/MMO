using System.Collections.Generic;
using UnityEngine;

/**
 * Title:UI基类
 * Description:适用于所有UI的Base类
 */


public class UIBase : MonoBehaviour
{

    protected Dictionary<WindowType, WindowBase> windowDic;

    public virtual void InitView()
    {
        windowDic = new Dictionary<WindowType, WindowBase>();
    }

    public virtual void Show(bool isShow = true)
    {
        gameObject.SetActive(isShow);
    }


    /// <summary>
    /// 根据WindowType返回Window
    /// </summary>
    /// <param name="windowType"></param>
    /// <returns></returns>
    public WindowBase GetWindow(WindowType windowType)
    {

        return windowDic[windowType];
    }

    /// <summary>
    /// 根据WindowType显示Window
    /// </summary>
    /// <param name="windowType"></param>
    public void ShowWindow(WindowType windowType, object obj = null)
    {

        if (windowDic == null || windowDic.Count <= 0) { return; }

        foreach (var item in windowDic)
        {
            item.Value.Show(item.Key == windowType, obj);
        }
    }



}
