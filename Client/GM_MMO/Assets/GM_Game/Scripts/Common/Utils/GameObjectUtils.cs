using UnityEngine;

/**
 * Title:GameObject��չ
 * Description:
 */


public static class GameObjectUtils {


    public static void Show(this GameObject go, bool isActive = true) {
        if (go == null) return;
        go.SetActive(isActive);

    }

}
