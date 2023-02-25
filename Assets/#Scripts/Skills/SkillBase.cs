using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public MovableBase owner; // ��ų�� ����

    protected ParticleSystem ps; // ��ƼŬ�ý���

    protected float damageMultiflierPercentage; // ������ ����(�ۼ�Ƽ����)

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
