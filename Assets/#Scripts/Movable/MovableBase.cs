using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBase))]
public abstract class MovableBase : MonoBehaviour
{
    protected StatBase stat;
    public StatBase Stat
    {
        set => stat=value;
        get
        {
            if (!stat)
            {
                stat = GetComponent<StatBase>();
            }
            return stat;
        }
    }
    protected Collider collider; // �ݶ��̴�
    public Animator anim;     // �ִϸ�����
    public Transform focusTarget;
    // �߻� �޼����
    abstract public float Attack();      // ���ݰ��� �޼���
    abstract public float GetDamage(float damage, MovableBase from);   // �ǰݰ��� �޼���
    abstract public float GetHeal();     // ü��ȸ�� �޼���
    abstract public void Move();        // �̵����� �޼���

    protected virtual void Start()
    {
        stat = GetComponent<StatBase>();
        collider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }
}
