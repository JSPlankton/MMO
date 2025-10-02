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

    //����״̬
    public virtual void OnEnter() { }


    //�˳�״̬
    public virtual void OnExit() { }



}
