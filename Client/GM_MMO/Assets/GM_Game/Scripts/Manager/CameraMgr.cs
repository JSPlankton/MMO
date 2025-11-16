using Cinemachine;
using DG.Tweening;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Title:
 * Description:
 */


public class CameraMgr : MonoBehaviour
{

    public static CameraMgr Intance;

    private CinemachineFreeLook _cinemachine;

    private Mouse _mouse;

    private RoleCtrlBase _mainRole;

    private void Awake()
    {
        Intance = this;
        _cinemachine = GetComponent<CinemachineFreeLook>();
        _mouse = Mouse.current;

    }

    public void InitCamera(RoleCtrlBase mainRole)
    {

        _mainRole = mainRole;
        _cinemachine.Follow = mainRole.transform;
        _cinemachine.LookAt = mainRole._lookAt;
        
        SetOrbites(12);
    }


    private void Update()
    {
        if (_isOpenRoleAttrWindow) { return; }

        //摄像机缩放功能
        //1.获取到鼠标滚轮事件
        if (_mouse.scroll.y.ReadValue() != 0)
        {
            SetOrbites(_cinemachine.m_Orbits[0].m_Height - _mouse.scroll.y.ReadValue() * 2 * Time.deltaTime);
        }


        //摄像机旋转功能
        CameraRotation();

    }

    /// <summary>
    /// 摄像机旋转功能
    /// </summary>
    private void CameraRotation()
    {

        //鼠标右键按下时
        if (_mouse.rightButton.isPressed)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (_mouse.delta.ReadValue() != Vector2.zero)
            {
                _cinemachine.m_YAxis.Value -= _mouse.delta.ReadValue().y * Time.deltaTime * _yAxisSpeed;
                _cinemachine.m_XAxis.Value += _mouse.delta.ReadValue().x * Time.deltaTime * _xAxisSpeed;
            }
        }
        //鼠标抬起时
        if (_mouse.rightButton.wasReleasedThisFrame)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }


    [SerializeField] private float _yAxisSpeed = 0.1f;
    [SerializeField] private float _xAxisSpeed = 10f;
    [SerializeField] private float _delayTimer = 0.6f;

    /// <summary>
    /// 设置轨道数据
    /// </summary>
    /// <param name="offset"></param>
    private void SetOrbites(float offset)
    {

        //限制缩放值
        offset = Mathf.Clamp(offset, 3f, 50f);

        //Top Rig
        DOTween.To(() => _cinemachine.m_Orbits[0].m_Height, x => _cinemachine.m_Orbits[0].m_Height = x, offset, _delayTimer);
        DOTween.To(() => _cinemachine.m_Orbits[0].m_Radius, x => _cinemachine.m_Orbits[0].m_Radius = x, offset * 0.25f, _delayTimer);


        DOTween.To(() => _cinemachine.m_Orbits[1].m_Height, x => _cinemachine.m_Orbits[1].m_Height = x, offset * 0.5f, _delayTimer);
        DOTween.To(() => _cinemachine.m_Orbits[1].m_Radius, x => _cinemachine.m_Orbits[1].m_Radius = x, offset * 0.7f, _delayTimer);


        DOTween.To(() => _cinemachine.m_Orbits[2].m_Radius, x => _cinemachine.m_Orbits[2].m_Radius = x, offset * 0.15f, _delayTimer);

    }

    //打开角色属性window时， 记录cinemachine的轨道数据
    private float _orbitesOffset;
    //打开角色属性window时，记录角色的旋转信息
    private float _roleRoateY;

    private float _cinemYAxis;

    //是否打开了角色属性window
    public bool _isOpenRoleAttrWindow;

    //是否打开了背包window
    private bool isOpenKnapsackWindow;

    private RectTransform _roleAttrWindowRectTrans;
    /// <summary>
    /// 设置角色属性window视角
    /// </summary>
    /// <param name="roleAttrWindow"></param>
    public void RoleAttrWindowAngle(WindowBase roleAttrWindow)
    {
        _roleAttrWindowRectTrans = roleAttrWindow.transform as RectTransform;

        if (roleAttrWindow.gameObject.activeSelf)//如果打开角色属性window， 切换视角
        {
            _orbitesOffset = _cinemachine.m_Orbits[0].m_Height;
            _roleRoateY = _mainRole.transform.localEulerAngles.y;
            _cinemYAxis = _cinemachine.m_YAxis.Value;

            _isOpenRoleAttrWindow = true;

            if (isOpenKnapsackWindow)
            {
                ToggleAngle(8, Camera.main.transform.localEulerAngles.y + 180, 0.2f, new Vector3(0, 0.85f), 460);
            }
            else
            {

                ToggleAngle(8, Camera.main.transform.localEulerAngles.y + 180, 0.2f, new Vector3(1.1f, 0.85f), 800);
            }
        }
        else
        { //如果关闭角色属性window，恢复视角
            RecoverAngle();
        }

    }
    /// <summary>
    /// 恢复视角
    /// </summary>
    public void RecoverAngle()
    {
        if (_isOpenRoleAttrWindow)
        {
            _isOpenRoleAttrWindow = false;
            ToggleAngle(_orbitesOffset, _roleRoateY, _cinemYAxis, new Vector3(0, 1.17f), 460);
        }
    }

    private void ToggleAngle(float cinemOffset, float roleRotateY, float cinemYAxis, Vector3 lookAtPos, float roleAttrWindowOffset)
    {
        //1.设置轨道数据
        SetOrbites(cinemOffset);
        //2.设置角色旋转，朝向摄像机
        _mainRole.transform.localEulerAngles = new Vector3(0, roleRotateY);
        //3.设置cinemachine的Y Axis
        _cinemachine.m_YAxis.Value = cinemYAxis;
        //4.设置LootAt对的位置信息
        _cinemachine.LookAt.localPosition = lookAtPos;
        //5.设置角色属性window的偏移量
        _roleAttrWindowRectTrans.DOAnchorPos(new Vector2(roleAttrWindowOffset, 0), _delayTimer);
    }

    /// <summary>
    /// 背包window视角
    /// </summary>
    /// <param name="knapsackWindow"></param>
    public void KnapsackWindowAngle(WindowBase knapsackWindow)
    {


        isOpenKnapsackWindow = knapsackWindow.gameObject.activeSelf;

        if (_isOpenRoleAttrWindow)
        {//如果角色属性window已经打开了

            if (isOpenKnapsackWindow)//再打开背包window
            {
                ToggleAngle(8, Camera.main.transform.localEulerAngles.y + 180, 0.2f, new Vector3(0, 0.85f), 460);
            }
            else
            {
                ToggleAngle(8, Camera.main.transform.localEulerAngles.y + 180, 0.2f, new Vector3(1.1f, 0.85f), 800);
            }

        }



    }



}
