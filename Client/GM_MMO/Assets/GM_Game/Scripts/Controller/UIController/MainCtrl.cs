using UnityEngine;

/**
 * Title:主城场景相关UI的控制器类
 * Description:
 */


public class MainCtrl : CtrlBase
{

    private MainView _mainView;

    public MainCtrl(UIBase view) : base(view)
    {

        _mainView = view as MainView;
        _mainView.InitView();

    }
}
