using System;
using UnityEngine;

/**
 * Title:
 * Description:
 */


public class RoleAnimaBehaviour : MonoBehaviour
{

    private RoleCtrlBase _roleCtrl;

    [SerializeField] private Transform _effectPos;

    private void Awake()
    {
        _roleCtrl = GetComponent<RoleCtrlBase>();
    }

    /// <summary>
    /// 动画结束
    /// </summary>
    private void AnimaEnd()
    {

        //
        _roleCtrl.ChangeState(RoleState.Idle);

    }


    private void DoAttackConfig(AtkConfigEntity entity)
    {
        if (entity._effect != null && entity._effect._fx)
        {

            //实例化特效
            ParticleSystem fx = Instantiate(entity._effect._fx);
            if (fx != null)
            {
                if (!string.IsNullOrEmpty(entity._effect._parentName))
                {   //设置父组件
                    Transform parent = _roleCtrl.transform.Find(entity._effect._parentName);
                    fx.transform.SetParent(parent);
                }
                fx.transform.localPosition = entity._effect._pos;
                fx.transform.localEulerAngles = entity._effect._eulerAngle;
                fx.transform.localScale = Vector3.one;
            }

        }

    }

    private void PlayEffect(AnimationEvent animEvent)
    {

        RoleAtkConfig config = animEvent.objectReferenceParameter as RoleAtkConfig;

        int index = animEvent.intParameter;

        if (config != null)
        {
            AtkConfigEntity entity = config.atkConfigEntities[index];

            if (!string.IsNullOrEmpty(animEvent.stringParameter))
            {

                switch (animEvent.stringParameter)
                {
                    case "skill03_2":
                        entity._effect._pos = _roleCtrl.transform.localPosition + _roleCtrl.transform.forward * 6;
                        entity._effect._eulerAngle = new Vector3(-90, _roleCtrl.transform.localEulerAngles.y,
                                                            _roleCtrl.transform.localEulerAngles.z);
                        break;
                    case "skill04_2":
                        entity._effect._pos = _roleCtrl.transform.localPosition + _roleCtrl.transform.forward * 10;
                        break;
                }
            }

            DoAttackConfig(entity);

        }

        if (_roleCtrl._targetRole != null)
        {
            _roleCtrl._targetRole.ChangeState(RoleState.Hit);
            _roleCtrl.HitFx(_roleCtrl._targetRole.transform);
        }


    }


}
