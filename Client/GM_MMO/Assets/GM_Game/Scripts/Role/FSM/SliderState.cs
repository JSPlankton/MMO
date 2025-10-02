using UnityEngine;

/**
 * Title:
 * Description:
 */


public class SliderState : RoleFSMStateBase
{
    public SliderState(RoleCtrlBase roleCtrl, Animator animator) : base(roleCtrl, animator)
    {
    }

    public override void OnEnter()
    {
        _animator.SetInteger(_roleCtrl._actionId, 41);
    }

    public override void OnExit()
    {

    }

}
