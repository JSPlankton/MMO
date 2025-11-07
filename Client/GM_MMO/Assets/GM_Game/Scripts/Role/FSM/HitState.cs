using UnityEngine;

/**
 * Title:
 * Description:
 */


public class HitState : RoleFSMStateBase
{
    public HitState(RoleCtrlBase roleCtrl, Animator animator) : base(roleCtrl, animator)
    {
    }


    public override void OnEnter()
    {
        _animator.SetInteger(_roleCtrl._actionId, 5);
    }


    public override void OnExit()
    {
    }


}
