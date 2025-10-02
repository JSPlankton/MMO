using System.Collections.Generic;
using UnityEngine;

/**
 * Title: 角色有限状态机
 * Description:
 */


public class RoleFSM
{


    RoleCtrlBase _roleCtrl;

    //用于存储角色所有的状态，统一管理
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
    /// 获取状态
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
    /// 改变角色状态
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(RoleState newState)
    {
        if (!_stateDic.ContainsKey(newState))
        {
            return;
        }

        //角色如果当前的状态 等于要改变的状态 那么就return,  特殊情况
        if (_roleCtrl._roleState == newState && newState != RoleState.Attck)
        {
            return;
        }


        //退出当前状态
        _stateDic[_roleCtrl._roleState].OnExit();

        //角色状态赋值
        _roleCtrl._roleState = newState;

        //进入新的状态
        _stateDic[newState].OnEnter();


    }




}
