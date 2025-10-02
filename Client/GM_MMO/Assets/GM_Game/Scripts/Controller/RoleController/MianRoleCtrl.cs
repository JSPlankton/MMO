using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Title: 主角的控制器类
 * Description:
 */


public class MianRoleCtrl : RoleCtrlBase
{


    private PlayerInputCtrl _inputCtrl;
    //角色移动速度
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
    /// 技能相关按键事件
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void SkillKey(string key)
    {
        switch (key)
        {
            case "F"://触发Slider动画
                ChangeState(RoleState.Slider);
                break;
            case "Q"://触发普通攻击动画
                ChangeState(RoleState.Attck);
                break;
        }
    }

    /// <summary>
    /// 按下空格键，角色跳跃
    /// </summary>
    private void Jumping()
    {
        //如果角色不在IDle状态，那么不能跳跃
        if (_roleState == RoleState.Idle || _roleState == RoleState.Run || _roleState == RoleState.FastRun)
        {

            //跳跃动画有一个起跳的动作，所有要延迟260毫秒后，在上升高度
            Observable.Timer(TimeSpan.FromMilliseconds(260)).Subscribe(_ =>
            {
                _verticalHeiht += 8;
            });
            ChangeState(RoleState.Jump);
        }
    }

    /// <summary>
    /// Shift按键是否按下， 如果按下，角色就要可以快速跑
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
    /// 角色移动
    /// </summary>
    private void PlayerMovement()
    {
        if (_roleState == RoleState.Attck) { return; }

        //W A S D键是否按下
        if (_inputCtrl.Movement != Vector2.zero)
        {
            //设置对象移动的目标偏移量
            Vector3 target = new Vector3(_inputCtrl.Movement.x, 0, _inputCtrl.Movement.y);
            target = target * Time.deltaTime * _moveSpeed;

            //如果角色跳跃时， 是不需要切换为Run,或则FastRun状态
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


            //从本地空间，转换成世界空间，
            target = Camera.main.transform.TransformDirection(target);
            target.y = 0;

            //对象的旋转
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target),
              _rotationSpeed * Time.deltaTime);

            //角色移动
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
