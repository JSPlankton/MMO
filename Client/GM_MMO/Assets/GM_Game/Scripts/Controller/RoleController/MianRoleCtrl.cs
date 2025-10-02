using System;
using UnityEngine;

/**
 * Title: ���ǵĿ�������
 * Description:
 */


public class MianRoleCtrl : RoleCtrlBase
{


    private PlayerInputCtrl _inputCtrl;
    //��ɫ�ƶ��ٶ�
    private float _moveSpeed = 10;
    private float _rotationSpeed = 1000;

    protected override void OnAwake()
    {
        _inputCtrl = GetComponent<PlayerInputCtrl>();

        _inputCtrl.ShiftKeyIsPressEvent += ShiftKeyIsPress;
    }

    /// <summary>
    /// Shift�����Ƿ��£� ������£���ɫ��Ҫ���Կ�����
    /// </summary>
    /// <param name="obj"></param>
    private void ShiftKeyIsPress(bool ispress)
    {
        if (ispress)
        {
            _moveSpeed = 18;
        }
        else
        {
            _moveSpeed = 10;
        }
    }

    private void Update()
    {

        PlayerMovement();



    }

    /// <summary>
    /// ��ɫ�ƶ�
    /// </summary>
    private void PlayerMovement()
    {
        //W A S D���Ƿ���
        if (_inputCtrl.Movement != Vector2.zero)
        {
            //���ö����ƶ���Ŀ��ƫ����
            Vector3 target = new Vector3(_inputCtrl.Movement.x, 0, _inputCtrl.Movement.y);
            target = target * Time.deltaTime * _moveSpeed;

            if (_moveSpeed == 10)
            {
                _animator.SetFloat("Movement", 2);
            }
            else if (_moveSpeed == 18)
            {
                _animator.SetFloat("Movement", 3);
            }



            //�ӱ��ؿռ䣬ת��������ռ䣬
            target = Camera.main.transform.TransformDirection(target);
            target.y = 0;

            //�������ת
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target),
              _rotationSpeed * Time.deltaTime);

            //��ɫ�ƶ�
            _characterController.Move(target);

        }
        else
        {
            _animator.SetFloat("Movement", 0);
        }
    }
}
