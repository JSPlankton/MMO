using System;
using System.Drawing;
using UniRx;
using UnityEngine;

/**
 * Title: Role Ctrl基类
 * Description:
 */


public class RoleCtrlBase : MonoBehaviour
{


    protected Animator _animator;
    protected CharacterController _characterController;
    //角色当前状态
    public RoleState _roleState;

    public int _actionId = Animator.StringToHash("Action");
    //跟运动动画 位移速度
    private float _rootMotionSpeed;
    //角色有限状态机
    protected RoleFSM _fsm;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();


        _fsm = new RoleFSM(this, _animator);
        OnAwake();
    }


    private void Start()
    {

        OnStart();
    }

    private void OnAnimatorMove()
    {
        if (_roleState == RoleState.Slider)
        {
            _rootMotionSpeed = 5;
        }
        else
        {
            _rootMotionSpeed = 1;
        }

        if (_animator.deltaPosition != Vector3.zero)
        {
            _characterController.Move(_animator.deltaPosition * _rootMotionSpeed);
        }

    }


    private void Update()
    {

        //检测是否在地面
        IsGorund();

        OnUpdate();
    }



    public void ChangeState(RoleState state)
    {
        _fsm.ChangeState(state);

        //_roleState = state;
        //switch (_roleState)
        //{
        //    case RoleState.Idle:
        //        _animator.SetInteger(_actionId, 1);
        //        break;
        //    case RoleState.Jump:
        //        _animator.SetInteger(_actionId, 21);
        //        break;
        //    case RoleState.Slider:
        //        _animator.SetInteger(_actionId, 41);
        //        break;
        //    case RoleState.Attck:

        //        _atkIndex++;

        //        if (_obs != null) { _obs.Dispose(); }

        //        _obs = Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(_ =>
        //        {
        //            _atkIndex = 30;
        //        });


        //        _animator.SetInteger(_actionId, _atkIndex);

        //        if (_atkIndex >= 33)
        //        {
        //            _atkIndex = 30;
        //        }

        //        break;
        //    default:
        //        break;
        //}
    }


    //上升或则下降的速度
    protected float _verticalSpeed;
    //需要到达的高度
    protected float _verticalHeiht;

    /// <summary>
    /// 检测是否在地面
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void IsGorund()
    {
        //需要到达的高度， 大于 角色当前的高度， 角色需要上升
        if (_verticalHeiht > transform.localPosition.y && CheckShereGround())
        {
            _verticalSpeed = 20;
        }
        else if (_verticalHeiht < transform.localPosition.y && _verticalHeiht != -1000)//角色当前的高度 大于了 需要到达的高度后，角色就开始下降
        {
            _verticalHeiht = -1000;
            _verticalSpeed = -20;
        }


        //过渡的值
        _verticalSpeed -= Mathf.Abs(_verticalSpeed) * Time.deltaTime * 1.5f;

        //if (_verticalSpeed > -50)
        //{
        //    Debug.Log("_verticalSpeed::" + _verticalSpeed);
        //}

        _characterController.Move(transform.up * Time.deltaTime * _verticalSpeed);


        //检测是否在地面
        if (CheckShereGround())
        {


            _verticalSpeed = -100;
            _verticalHeiht = transform.localPosition.y;
        }

    }

    /// <summary>
    /// 检测是否在地面
    /// </summary>
    /// <returns></returns>
    public bool CheckShereGround()
    {
        //用于检测当前位置周围半径范围内所有的碰撞体，如果有碰撞则返回true
        Vector3 pos = transform.position + new Vector3(0, 0.1f, 0);
        return Physics.CheckSphere(pos, 0.2f, 1 << LayerMask.NameToLayer("Geometry"));
    }


    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

}
