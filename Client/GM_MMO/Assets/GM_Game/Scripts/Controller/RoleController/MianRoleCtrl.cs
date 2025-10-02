using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Title: ���ǵĿ�������
 * Description:
 */


public class MianRoleCtrl : RoleCtrlBase
{


    private PlayerInputCtrl _inputCtrl;
    //��ɫ�ƶ��ٶ�
    private float _moveSpeed = 10;
    private float _rotationSpeed = 1000;

    private GhostEffect _ghostEffect;

    protected override void OnAwake()
    {
        _inputCtrl = GetComponent<PlayerInputCtrl>();
        _ghostEffect = GetComponent<GhostEffect>();

        _inputCtrl.ShiftKeyIsPressEvent += ShiftKeyIsPress;
        _inputCtrl.Jumping += Jumping;
        _inputCtrl.SkillKeyEvent += SkillKey;
    }

    /// <summary>
    /// ������ذ����¼�
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void SkillKey(string key)
    {
        switch (key)
        {
            case "F"://����Slider����
                ChangeState(RoleState.Slider);
                break;
            case "Q"://������ͨ��������
                ChangeState(RoleState.Attck);
                break;
        }
    }

    /// <summary>
    /// ���¿ո������ɫ��Ծ
    /// </summary>
    private void Jumping()
    {
        //�����ɫ����IDle״̬����ô������Ծ
        if (_roleState == RoleState.Idle || _roleState == RoleState.Run || _roleState == RoleState.FastRun)
        {

            //��Ծ������һ�������Ķ���������Ҫ�ӳ�260������������߶�
            Observable.Timer(TimeSpan.FromMilliseconds(260)).Subscribe(_ =>
            {
                _verticalHeiht += 8;
            });
            ChangeState(RoleState.Jump);
        }
    }

    /// <summary>
    /// Shift�����Ƿ��£� ������£���ɫ��Ҫ���Կ�����
    /// </summary>
    /// <param name="obj"></param>
    private void ShiftKeyIsPress(bool ispress)
    {
        if (ispress)
        {
            _moveSpeed = 18;
        }
        else
        {
            _moveSpeed = 10;
        }
    }


    protected override void OnUpdate()
    {

        if (_roleState == RoleState.Slider)
        {
            _ghostEffect.CreateGhostEffectObject(Color.white, 0.2f, 0.2f, 0.2f, 0.2f);
        }

        PlayerMovement();
    }

    /// <summary>
    /// ��ɫ�ƶ�
    /// </summary>
    private void PlayerMovement()
    {
        if (_roleState == RoleState.Attck) { return; }

        //W A S D���Ƿ���
        if (_inputCtrl.Movement != Vector2.zero)
        {
            //���ö����ƶ���Ŀ��ƫ����
            Vector3 target = new Vector3(_inputCtrl.Movement.x, 0, _inputCtrl.Movement.y);
            target = target * Time.deltaTime * _moveSpeed;

            //�����ɫ��Ծʱ�� �ǲ���Ҫ�л�ΪRun,����FastRun״̬
            if (_roleState != RoleState.Jump && _roleState != RoleState.Slider)
            {
                if (_moveSpeed == 10)
                {
                    ChangeState(RoleState.Run);
                }
                else if (_moveSpeed == 18)
                {
                    ChangeState(RoleState.FastRun);
                }
            }


            //�ӱ��ؿռ䣬ת��������ռ䣬
            target = Camera.main.transform.TransformDirection(target);
            target.y = 0;

            //�������ת
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target),
              _rotationSpeed * Time.deltaTime);

            //��ɫ�ƶ�
            _characterController.Move(target);

        }
        else
        {
            if (_roleState == RoleState.Run || _roleState == RoleState.FastRun)
            {
                ChangeState(RoleState.Idle);
            }
        }
    }
}
