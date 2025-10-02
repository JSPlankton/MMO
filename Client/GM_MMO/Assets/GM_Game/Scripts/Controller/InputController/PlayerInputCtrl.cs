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
    //Shift����¼�
    public Action<bool> ShiftKeyIsPressEvent;
    //��Ծ����¼�
    public Action Jumping;
    //������صİ�������
    public Action<string> SkillKeyEvent;

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
    /// ע�������¼�
    /// </summary>
    private void RegistInputEvent()
    {

        //Shift��������ʱ
        _input.Player.Shift.started += (InputAction.CallbackContext ctx) =>
        {
            ShiftKeyIsPressEvent?.Invoke(true);
        };
        //Shift����̧��ʱ
        _input.Player.Shift.canceled += ctx =>
        {
            ShiftKeyIsPressEvent?.Invoke(false);
        };

        _input.Player.Jump.started += ctx =>
        {
            Jumping?.Invoke();
        };


        //�����̵�ĳһ�������µ�ʱ��
        Keyboard.current.onTextInput += c =>
        {

            string key = c.ToString().ToUpper();
            switch (key)
            {
                case "Q":
                case "E":
                case "R":
                case "F":
                    SkillKeyEvent?.Invoke(key);
                    break;
            }

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
