using UnityEngine;

/**
 * Title:
 * Description:
 */


public class MiniMapHelper : MonoBehaviour
{

    public static MiniMapHelper Instance;

    private void Awake()
    {
        Instance = this;
    }


}
