using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Title:
 * Description:
 */


public class KnapsackSlotWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{


    [SerializeField, Header("物品的图标")] private Image _imgIcon;

    [SerializeField, Header("物品的数量")] private TMP_Text _texCount;

    [SerializeField, Header("鼠标进入效果")] private Image _imgEnter;
    [SerializeField, Header("物品特效")] private Image _imgFX;


    private int _count;
    private string _spriteName;

    /// <summary>
    /// 放置事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        UIDragWidget dragWidget = eventData.pointerDrag.GetComponent<UIDragWidget>();
        if (dragWidget != null)
        {

            if (dragWidget._dragType == DragType.KanpsackSlot)//拖拽的是背包Slot
            {
                //临时的 ，     交到服务端

                //数据的交换
                int tempCount = _count;
                string tempSpriteName = _spriteName;

                KnapsackSlotWidget dragSlot = dragWidget._deflutParent.GetComponent<KnapsackSlotWidget>();

                RefreshUI(dragSlot._count, dragSlot._spriteName);

                dragSlot.RefreshUI(tempCount, tempSpriteName);
            }

        }


    }

    /// <summary>
    /// 鼠标进入事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _imgEnter.gameObject.Show();

        //演示
        if (_count > 0)
        {
            TipsMgr.Instance.ShowItemTips(eventData.position);
        }

    }

    /// <summary>
    /// 鼠标离开事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _imgEnter.gameObject.Show(false);
        if (_count > 0)
        {
            TipsMgr.Instance.CloseItemTips();
        }

    }

    public void RefreshUI(int count, string spriteName)
    {
        _count = count;
        _spriteName = spriteName;

        if (count > 0)
        {
            _imgIcon.gameObject.Show();
            _texCount.gameObject.Show();

            _texCount.SetText($"{count}");

            ResourceMgr.Instance.LoadSpriteAsync($"Icon/Item/{spriteName}", (Sprite sprite) =>
            {

                _imgIcon.sprite = sprite;
            });

        }
        else
        {

            _imgIcon.gameObject.Show(false);
            _texCount.gameObject.Show(false);
        }

    }


}
