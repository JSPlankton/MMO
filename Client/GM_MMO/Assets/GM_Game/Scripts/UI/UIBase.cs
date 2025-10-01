using System.Collections.Generic;
using UnityEngine;

/**
 * Title:UI����
 * Description:����������UI��Base��
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
    /// ����WindowType����Window
    /// </summary>
    /// <param name="windowType"></param>
    /// <returns></returns>
    public WindowBase GetWindow(WindowType windowType)
    {

        return windowDic[windowType];
    }

    /// <summary>
    /// ����WindowType��ʾWindow
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
