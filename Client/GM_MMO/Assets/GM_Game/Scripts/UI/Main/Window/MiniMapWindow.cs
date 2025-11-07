using UnityEngine;
using UnityEngine.UI;

/**
 * Title:
 * Description:
 */


public class MiniMapWindow : WindowBase
{

    [SerializeField, Header("小地图背景图片")] private Image _imgMiniMap;
    [SerializeField, Header("小地图箭头图片")] private Image _imgArrow;

    //暂时在Unity中赋值
    [SerializeField] private RoleCtrlBase _mainRole;

    private MiniMapHelper _mapHelper;
    //小地图背景图片大小
    private float _mapSize = 1024;

    private void Start()
    {
        _mapHelper = MiniMapHelper.Instance;
    }



    private void Update()
    {

        if (_mainRole != null)
        {
            _mapHelper.transform.position = _mainRole.transform.position;

            //实时更新小地图的位置 ， 根据角色信息的位置来更新
            _imgMiniMap.rectTransform.anchoredPosition = new Vector2(_mapHelper.transform.localPosition.x * -_mapSize,
                _mapHelper.transform.localPosition.z * -_mapSize);

            //实时的更新箭头如片的旋转信息， 根据角色的旋转
            _imgArrow.transform.localEulerAngles = new Vector3(0, 0, 360 - _mainRole.transform.localEulerAngles.y + 90);

        }

        //Render texture   性能消耗非常的高

    }

    public void OnPluseBtnClicked()
    {



        _mapSize = Mathf.Clamp(_mapSize * 1.1f, 512, 2048);

        _imgMiniMap.rectTransform.sizeDelta = new Vector2(_mapSize, _mapSize);


    }


    public void OnDecBtnClicked()
    {
        _mapSize = Mathf.Clamp(_mapSize / 1.1f, 512, 2048);
        _imgMiniMap.rectTransform.sizeDelta = new Vector2(_mapSize, _mapSize);

    }


}
