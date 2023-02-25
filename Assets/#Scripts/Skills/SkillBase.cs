using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public MovableBase owner; // 스킬의 주인

    protected ParticleSystem ps; // 파티클시스템

    protected float damageMultiflierPercentage; // 데미지 배율(퍼센티지로)

    protected virtual void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual void PlaySkill(MovableBase owner)
    { 
    }

}
