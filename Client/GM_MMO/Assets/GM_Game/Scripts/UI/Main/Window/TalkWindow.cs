using UnityEngine;

/**
 * Title:
 * Description:
 */


public class TalkWindow : WindowBase
{



    public void OnShoppingBtnClicked()
    {

        //œ‘ æπ∫¬Ú…Ã∆∑Window
        UIRoot.Instance.MainViewCtrl.ShowMainWindow(WindowType.ShopWindow);
        CloseWindow();
    }



}
