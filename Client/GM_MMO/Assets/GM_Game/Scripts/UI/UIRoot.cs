using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/**
 * Title:UI和控制器的管理类，
 * Description:管理所有的UI和控制器。
 */


public class UIRoot : MonoBehaviour {

    public static UIRoot Instance;

    [SerializeField, Header("登录View")] private LoginView _loginView;
    public LoginCtrl LoginViewCtrl;


    [SerializeField, Header("创建角色相关View")] private CreateRoleView _createRoleView;
    public CreateRoleCtrl CreateRoleViewCtrl;


    [SerializeField, Header("点击特效")] private ParticleSystem _clickFX;

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

        //输入系统
        //判断鼠标左键按下的，
        if (Mouse.current.leftButton.wasPressedThisFrame) {

            //判断点击的是否是UI
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


    //把屏幕坐标转为UI坐标，
    public Vector2 ScreenPointToViewPoint(Vector2 screenPos) {

        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                                                              screenPos, _canvas.worldCamera,
                                                              out pos);
        return pos;
    }

}
