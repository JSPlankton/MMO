using UnityEngine;

/**
 * Title:
 * Description:
 */


public class FastRunState : RoleFSMStateBase
{
    public FastRunState(RoleCtrlBase roleCtrl, Animator animator) : base(roleCtrl, animator)
    {
    }


    public override void OnEnter()
    {
        _animator.SetFloat("Movement", 3);
    }

    public override void OnExit()
    {

    }

}
