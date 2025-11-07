using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Title:
 * Description:
 */

//震屏信息
[Serializable]
public class ShakeScreenInfo
{
    //延迟时间
    public float _delay;

    //持续时间
    public float _duration;
    //力度
    public float _force;

}

//特效信息
[Serializable]
public class EffectInfo
{

    //特效资源
    public ParticleSystem _fx;

    //特效的父组件
    public string _parentName;

    //特效的位置
    public Vector3 _pos;

    //特效的旋转信息
    public Vector3 _eulerAngle;

}

[Serializable]
public class AtkConfigEntity
{

    //特效信息
    public EffectInfo _effect;
    //音效信息
    public AudioClip[] _clips;
    //震屏信息
    public ShakeScreenInfo _shakeScreenInfo;
}

[CreateAssetMenu(fileName = "AtkConfig", menuName = "RoleAtkConfig")]
public class RoleAtkConfig : ScriptableObject
{

    public List<AtkConfigEntity> atkConfigEntities;

}
