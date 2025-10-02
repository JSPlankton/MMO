using System;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Title:
 * Description:
 */


public class PlayerInputCtrl : MonoBehaviour
{

    private PlayerInput _input;

    public Action<bool> ShiftKeyIsPressEvent;


    public Vector2 Movement
    {
        get => _input.Player.Movement.ReadValue<Vector2>();
    }


    private void Awake()
    {

        _input = new PlayerInput();

        RegistInputEvent();

    }

    /// <summary>
    /// 注册输入事件
    /// </summary>
    private void RegistInputEvent()
    {

        //Shift按键按下时
        _input.Player.Shift.started += (InputAction.CallbackContext ctx) =>
        {
            ShiftKeyIsPressEvent?.Invoke(true);
        };
        //Shift按键抬起时
        _input.Player.Shift.canceled += ctx =>
        {
            ShiftKeyIsPressEvent?.Invoke(false);
        };


    }

    private void OnEnable()
    {
        _input.asset.Enable();
    }

    private void OnDisable()
    {
        _input.asset.Disable();
    }


}
