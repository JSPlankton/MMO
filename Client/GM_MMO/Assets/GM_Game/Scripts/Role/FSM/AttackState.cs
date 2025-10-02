using System;
using UniRx;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class AttackState : RoleFSMStateBase
{


    private int _atkIndex = 30;
    private IDisposable _obs;

    public AttackState(RoleCtrlBase roleCtrl, Animator animator) : base(roleCtrl, animator)
    {
    }

    public override void OnEnter()
    {
        _atkIndex++;

        if (_obs != null) { _obs.Dispose(); }

        _obs = Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(_ =>
        {
            _atkIndex = 30;
        });


        _animator.SetInteger(_roleCtrl._actionId, _atkIndex);

        if (_atkIndex >= 33)
        {
            _atkIndex = 30;
        }
    }

    public override void OnExit()
    {

        if (_obs != null) { _obs.Dispose(); }
    }

}
