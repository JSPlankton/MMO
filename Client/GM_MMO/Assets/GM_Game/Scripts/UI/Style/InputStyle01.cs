using DG.Tweening;
using TMPro;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class InputStyle01 : MonoBehaviour {

    [SerializeField, Header("ռλ��")] private TMP_Text _texPlaceholder;

    [SerializeField, Header("��������ʱ��")] private float _duretion = 0.1f;
    [SerializeField, Header("�ƶ�ƫ����")] private float _moveY = 20;

    private float _posY;

    private void Start() {

        _posY = _texPlaceholder.rectTransform.anchoredPosition.y;

        TMP_InputField ipt = GetComponent<TMP_InputField>();

        //Ĭ������£���������Ϊ�գ� ��ռλ��λ��Ĭ�����Ϸ�
        if (!string.IsNullOrEmpty(ipt.text)) {
            //DOTween
            _texPlaceholder.rectTransform.DOAnchorPosY(_posY + _moveY, _duretion);
        }

        //�������ѡ�е�ʱ�� ռλ�������ƶ�20
        ipt.onSelect.AddListener((string str) => {

            if (string.IsNullOrEmpty(ipt.text)) {
                _texPlaceholder.rectTransform.DOAnchorPosY(_posY + _moveY, _duretion);
            }
        });

        //������ѡ�е�ʱ��ص�Ĭ��λ�ã�
        ipt.onDeselect.AddListener((string str) => {
            if (string.IsNullOrEmpty(ipt.text)) {
                _texPlaceholder.rectTransform.DOAnchorPosY(_posY, _duretion);
            }
        });

    }


}
