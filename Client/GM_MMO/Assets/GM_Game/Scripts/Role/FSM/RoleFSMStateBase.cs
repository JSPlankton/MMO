using UnityEngine;

/**
 * Title:
 * Description:
 */


public class RoleFSMStateBase
{


    protected RoleCtrlBase _roleCtrl;
    protected Animator _animator;


    public RoleFSMStateBase(RoleCtrlBase roleCtrl, Animator animator)
    {
        _roleCtrl = roleCtrl;
        _animator = animator;
    }

    //½øÈë×´Ì¬
    public virtual void OnEnter() { }


    //ÍË³ö×´Ì¬
    public virtual void OnExit() { }



}
