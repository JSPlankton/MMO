using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/**
 * Title:UI�Ϳ������Ĺ����࣬
 * Description:�������е�UI�Ϳ�������
 */


public class UIRoot : MonoBehaviour {

    public static UIRoot Instance;

    [SerializeField, Header("��¼View")] private LoginView _loginView;
    public LoginCtrl LoginViewCtrl;


    [SerializeField, Header("������ɫ���View")] private CreateRoleView _createRoleView;
    public CreateRoleCtrl CreateRoleViewCtrl;


    [SerializeField, Header("�����Ч")] private ParticleSystem _clickFX;

    private Canvas _canvas;

    private void Awake() {
        Instance = this;

        DontDestroyOnLoad(this);

        InitCtrl();
    }

    private void Start() {
        _canvas = GetComponentInChildren<Canvas>();
    }

    private void Update() {

        //����ϵͳ
        //�ж����������µģ�
        if (Mouse.current.leftButton.wasPressedThisFrame) {

            //�жϵ�����Ƿ���UI
            if (EventSystem.current.IsPointerOverGameObject()) {

                _clickFX.transform.localPosition = ScreenPointToViewPoint(Mouse.current.position.ReadValue());
                _clickFX.Play();
            }

        }

    }

    private void InitCtrl() {

        if (_loginView != null) {
            LoginViewCtrl = new LoginCtrl(_loginView);
        }

        if (_createRoleView != null) {
            CreateRoleViewCtrl = new CreateRoleCtrl(_createRoleView);
        }

    }


    //����Ļ����תΪUI���꣬
    public Vector2 ScreenPointToViewPoint(Vector2 screenPos) {

        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                                                              screenPos, _canvas.worldCamera,
                                                              out pos);
        return pos;
    }

}
