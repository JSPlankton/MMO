using System.Collections.Generic;
using UnityEngine;

/**
 * Title: ��ɫ����״̬��
 * Description:
 */


public class RoleFSM
{


    RoleCtrlBase _roleCtrl;

    //���ڴ洢��ɫ���е�״̬��ͳһ����
    private Dictionary<RoleState, RoleFSMStateBase> _stateDic;

    public RoleFSM(RoleCtrlBase roleCtrl, Animator animator)
    {
        _roleCtrl = roleCtrl;

        _stateDic = new Dictionary<RoleState, RoleFSMStateBase>();


        _stateDic[RoleState.Idle] = new IdleState(roleCtrl, animator);
        _stateDic[RoleState.Run] = new RunState(roleCtrl, animator);
        _stateDic[RoleState.FastRun] = new FastRunState(roleCtrl, animator);
        _stateDic[RoleState.Jump] = new JumpState(roleCtrl, animator);
        _stateDic[RoleState.Slider] = new SliderState(roleCtrl, animator);
        _stateDic[RoleState.Attck] = new AttackState(roleCtrl, animator);
        //....
    }

    /// <summary>
    /// ��ȡ״̬
    /// </summary>
    /// <param name="roleState"></param>
    /// <returns></returns>
    public RoleFSMStateBase GetRoleFSMState(RoleState roleState)
    {
        if (_stateDic.ContainsKey(roleState))
        {
            return _stateDic[roleState];
        }
        return null;
    }

    /// <summary>
    /// �ı��ɫ״̬
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(RoleState newState)
    {
        if (!_stateDic.ContainsKey(newState))
        {
            return;
        }

        //��ɫ�����ǰ��״̬ ����Ҫ�ı��״̬ ��ô��return,  �������
        if (_roleCtrl._roleState == newState && newState != RoleState.Attck)
        {
            return;
        }


        //�˳���ǰ״̬
        _stateDic[_roleCtrl._roleState].OnExit();

        //��ɫ״̬��ֵ
        _roleCtrl._roleState = newState;

        //�����µ�״̬
        _stateDic[newState].OnEnter();


    }




}
