using UnityEngine;

/**
 * Title: 主角的控制器类
 * Description:
 */


public class MianRoleCtrl : RoleCtrlBase
{


    private PlayerInputCtrl _inputCtrl;
    private float _moveSpeed = 10;
    private float _rotationSpeed = 1000;

    protected override void OnAwake()
    {
        _inputCtrl = GetComponent<PlayerInputCtrl>();
    }

    private void Update()
    {

        //W A S D键是否按下
        if (_inputCtrl.Movement != Vector2.zero)
        {
            //设置对象移动的目标偏移量
            Vector3 target = new Vector3(_inputCtrl.Movement.x, 0, _inputCtrl.Movement.y);
            target = target * Time.deltaTime * _moveSpeed;

            _animator.SetFloat("Movement", 2);

            //从本地空间，转换成世界空间，
            target = Camera.main.transform.TransformDirection(target);
            target.y = 0;

            //对象的旋转
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target),
              _rotationSpeed * Time.deltaTime);

            //角色移动
            _characterController.Move(target);

        }
        else
        {
            _animator.SetFloat("Movement", 0);
        }

    }


}
