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

    private CinemachineFreeLook _cinemachine;

    private Mouse _mouse;

    private void Awake()
    {
        _cinemachine = GetComponent<CinemachineFreeLook>();
        _mouse = Mouse.current;
        SetOrbites(12);
    }


    private void Update()
    {


        //摄像机缩放功能
        //1.获取到鼠标滚轮事件
        if (_mouse.scroll.y.ReadValue() != 0)
        {
            SetOrbites(_cinemachine.m_Orbits[0].m_Height - _mouse.scroll.y.ReadValue() * Time.deltaTime);
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


}
