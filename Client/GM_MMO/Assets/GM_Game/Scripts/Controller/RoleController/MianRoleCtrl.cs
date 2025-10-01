using UnityEngine;

/**
 * Title: ���ǵĿ�������
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

        //W A S D���Ƿ���
        if (_inputCtrl.Movement != Vector2.zero)
        {

            Vector3 target = new Vector3(_inputCtrl.Movement.x, 0, _inputCtrl.Movement.y);
            target = target * Time.deltaTime * _moveSpeed;

            _animator.SetFloat("Movement", 2);

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
