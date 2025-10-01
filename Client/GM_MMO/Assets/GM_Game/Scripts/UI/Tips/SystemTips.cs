using DG.Tweening;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/**
 * Title:ϵͳ��ʾ��
 * Description:
 */


public class SystemTips : MonoBehaviour {

    [SerializeField, Header("��ʾ�ı�")] private TMP_Text _texMsg;
    [SerializeField, Header("��ɫ����")] private AnimationCurve _colorCurve;
    [SerializeField, Header("�ƶ�����")] private AnimationCurve _moveCurve;



    private void FixedUpdate() {

    }

    private void OnDestroy() {

    }

    public void RefreshUI(string msg) {


        _texMsg.SetText(msg);
        _texMsg.DOColor(Color.red, 2).SetEase(_colorCurve);


        RectTransform rectTrans = transform as RectTransform;
        rectTrans.DOAnchorPosY(rectTrans.anchoredPosition.y + UnityEngine.Random.Range(200, 260), 2)
            .SetEase(_moveCurve);

        //��ʱ���ٵ�ǰ����

        Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(v => {
            Destroy(gameObject);
        });



    }




}
