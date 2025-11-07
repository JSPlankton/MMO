using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Title:
 * Description:
 */

[RequireComponent(typeof(CanvasGroup))]
public class UIDragWidget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    private Image _imgDrag;
    //默认的父组件
    public Transform _deflutParent;

    //拖拽的类型
    public DragType _dragType;


    //拖拽Widget 在拖拽的过程中， 有可能被其他UI挡住，所以在拖拽中的时候，把它的父组件设置为canvas
    private Canvas _canvas;

    private CanvasGroup _canvasGroup;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = transform as RectTransform;
        _imgDrag = GetComponent<Image>();
        _deflutParent = transform.parent;
        _canvas = UIRoot.Instance._canvas;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 开始拖拽事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //启动射线检测
        _canvasGroup.blocksRaycasts = false;
        gameObject.SetParent(_canvas.transform);
    }

    /// <summary>
    /// 拖拽中事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = UIRoot.Instance.ScreenPointToViewPoint(eventData.position);
    }

    /// <summary>
    /// 拖拽结束事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        if (transform.parent == _canvas.transform)
        {

            gameObject.SetParent(_deflutParent);
        }

    }
}
