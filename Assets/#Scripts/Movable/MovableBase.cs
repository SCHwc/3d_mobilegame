using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBase))]
public abstract class MovableBase : MonoBehaviour
{
    protected StatBase stat;
    public StatBase Stat
    {
        set => stat = value;
        get
        {
            if (!stat)
            {
                stat = GetComponent<StatBase>();
            }
            return stat;
        }
    }
    protected Collider collider;        // �ݶ��̴�
    public Animator anim;               // �ִϸ�����
    public MovableBase focusTarget;     // �� ĳ������ ��ǥ��
    public bool isAlly;                 // ���Ϳ���
    public SkinnedMeshRenderer[] meshs;

    public Collider atkCollider; // 근접공격에 필요한 콜라이더

    // �߻� �޼����
    abstract public float GetDamage(float damage, MovableBase from);   // �ǰݰ��� �޼���
   
    protected virtual void Start()
    {
        stat = GetComponent<StatBase>();
        collider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        meshs=GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public virtual void AddWeapon(string wantName) { }
}
