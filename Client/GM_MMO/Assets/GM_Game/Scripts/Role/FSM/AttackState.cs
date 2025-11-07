using DG.Tweening;
using System;
using UniRx;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class AttackState : RoleFSMStateBase
{


    public int _atkIndex = 30;
    public int _atkType;
    private IDisposable _obs;

    public AttackState(RoleCtrlBase roleCtrl, Animator animator) : base(roleCtrl, animator)
    {
    }

    public override void OnEnter()
    {
        if (_atkType == 1)
        {
            _atkIndex++;

            if (_obs != null) { _obs.Dispose(); }

            _obs = Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(_ =>
            {
                _atkIndex = 30;
            });




            if (_atkIndex >= 33)
            {
                _atkIndex = 30;
            }
        }


        if (_roleCtrl._targetRole != null)
        {
            _roleCtrl.transform.LookAt(_roleCtrl._targetRole.transform);
        }

        _animator.SetInteger(_roleCtrl._actionId, _atkIndex);
    }

    public override void OnExit()
    {

        if (_obs != null) { _obs.Dispose(); }
    }

}
