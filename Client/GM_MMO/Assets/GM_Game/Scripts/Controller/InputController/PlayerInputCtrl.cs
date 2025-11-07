using System;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Title:
 * Description:
 */


public class PlayerInputCtrl : MonoBehaviour
{

    public static PlayerInputCtrl Instance;


    private PlayerInput _input;
    //Shift相关事件
    public Action<bool> ShiftKeyIsPressEvent;
    //跳跃相关事件
    public Action Jumping;
    //技能相关的按键按下
    public Action<string> SkillKeyEvent;

    //主城UI相关的按键按下
    public Action<string> MainUIKeyEvent;

    public Vector2 Movement
    {
        get => _input.Player.Movement.ReadValue<Vector2>();
    }


    private void Awake()
    {
        Instance = this;
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

        _input.Player.Jump.started += ctx =>
        {
            Jumping?.Invoke();
        };


        //当键盘的某一个键按下的时候，
        Keyboard.current.onTextInput += c =>
        {

            if (!_input.asset.enabled) { return; }

            string key = c.ToString().ToUpper();
            switch (key)
            {
                case "Q":
                case "E":
                case "R":
                case "F":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                    SkillKeyEvent?.Invoke(key);
                    break;

                case "L":
                case "B":
                case "C":
                    MainUIKeyEvent?.Invoke(key);
                    break;

            }

        };

    }

    public void OnEnable()
    {
        _input.asset.Enable();
    }

    public void OnDisable()
    {
        _input.asset.Disable();
    }


}
