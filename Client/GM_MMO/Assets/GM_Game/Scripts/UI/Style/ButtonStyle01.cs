using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Title:��ť����Ч�Ͷ�Ч
 * Description:
 */


public class ButtonStyle01 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerUpHandler, IPointerDownHandler {

    [SerializeField, Header("��ť��Ĭ������")] private float _btnDefaultScale = 1;
    [SerializeField, Header("��ť����ʱ������")] private float _btnDownScale = 0.85f;

    [SerializeField, Header("UI��Ч����")] GameObject _effectGo;

    //��갴��ʱ����
    public void OnPointerDown(PointerEventData eventData) {

        transform.DOScale(_btnDownScale, 0.05f);
    }

    //��갸��̧��ʱ����
    public void OnPointerUp(PointerEventData eventData) {
        transform.DOScale(_btnDefaultScale, 0.05f);
    }

    //������ʱ����
    public void OnPointerEnter(PointerEventData eventData) {
        if (_effectGo != null) {
            _effectGo.Show();
        }
    }

    //����뿪ʱ�¼�
    public void OnPointerExit(PointerEventData eventData) {
        if (_effectGo != null) {
            _effectGo.Show(false);
        }
    }

}
