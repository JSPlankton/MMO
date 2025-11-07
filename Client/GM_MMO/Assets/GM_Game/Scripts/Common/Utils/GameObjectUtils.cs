using UnityEngine;

/**
 * Title:GameObject扩展
 * Description:
 */


public static class GameObjectUtils
{


    public static void Show(this GameObject go, bool isActive = true)
    {
        if (go == null) return;
        go.SetActive(isActive);

    }

    public static void Show(this Transform trans, bool isActive = true)
    {
        if (trans == null) return;
        trans.gameObject.SetActive(isActive);

    }

    /// <summary>
    /// GameObject设置父组件，位置，旋转和缩放信息
    /// </summary>
    /// <param name="go"></param>
    /// <param name="parent"></param>
    /// <param name="pos"></param>
    /// <param name="angle"></param>
    public static void SetParent(this GameObject go, Transform parent, Vector3 pos = default, Vector3 angle = default)
    {

        if (go == null || parent == null) return;

        go.transform.SetParent(parent);
        go.transform.localPosition = pos;
        go.transform.localEulerAngles = angle;
        go.transform.localScale = Vector3.one;

    }


    /// <summary>
    /// 朝向目标
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="target"></param>
    public static void LookAtTarget(this Transform trans, Transform target)
    {

        if (trans == null || target == null)
        {
            return;
        }

        Vector3 dir = (target.position - trans.position).normalized;
        dir.y = 0;

        trans.rotation = Quaternion.LookRotation(dir);


    }


}
