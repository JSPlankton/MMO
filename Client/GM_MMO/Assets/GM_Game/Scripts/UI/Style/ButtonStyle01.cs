using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Title:按钮和特效和动效
 * Description:
 */


public class ButtonStyle01 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerUpHandler, IPointerDownHandler {

    [SerializeField, Header("按钮的默认缩放")] private float _btnDefaultScale = 1;
    [SerializeField, Header("按钮按下时的缩放")] private float _btnDownScale = 0.85f;

    [SerializeField, Header("UI特效对象")] GameObject _effectGo;

    //鼠标按下时触发
    public void OnPointerDown(PointerEventData eventData) {

        transform.DOScale(_btnDownScale, 0.05f);
    }

    //鼠标案件抬起时触发
    public void OnPointerUp(PointerEventData eventData) {
        transform.DOScale(_btnDefaultScale, 0.05f);
    }

    //鼠标进入时触发
    public void OnPointerEnter(PointerEventData eventData) {
        if (_effectGo != null) {
            _effectGo.Show();
        }
    }

    //鼠标离开时事件
    public void OnPointerExit(PointerEventData eventData) {
        if (_effectGo != null) {
            _effectGo.Show(false);
        }
    }

}
