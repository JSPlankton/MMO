using UnityEngine;

/**
 * Title:
 * Description:
 */


public class IdleState : RoleFSMStateBase
{
    public IdleState(RoleCtrlBase roleCtrl, Animator animator) : base(roleCtrl, animator)
    {
    }


    public override void OnEnter()
    {
        _animator.SetInteger(_roleCtrl._actionId, 1);
        _animator.SetFloat("Movement", 0);

    }

    public override void OnExit()
    {

    }


}
