using System;
using UniRx;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class JumpState : RoleFSMStateBase
{

    private IDisposable _obs;

    public JumpState(RoleCtrlBase roleCtrl, Animator animator) : base(roleCtrl, animator)
    {
    }

    public override void OnEnter()
    {
        _animator.SetInteger(_roleCtrl._actionId, 21);

        _obs = Observable.EveryUpdate().Subscribe(_ =>
        {

            if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump_Loop") && _roleCtrl.CheckShereGround())
            {
                _animator.SetInteger(_roleCtrl._actionId, 23);
                _obs.Dispose();
            }
        });

    }

    public override void OnExit()
    {
        if (_obs != null) { _obs.Dispose(); }
    }

}
