using UnityEngine;

/**
 * Title:
 * Description:
 */


public class RunState : RoleFSMStateBase
{
    public RunState(RoleCtrlBase roleCtrl, Animator animator) : base(roleCtrl, animator)
    {
    }

    public override void OnEnter()
    {
        _animator.SetFloat("Movement", 2);
    }

    public override void OnExit()
    {

    }

}
