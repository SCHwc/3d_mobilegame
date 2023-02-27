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

    protected Collider collider;    // �ݶ��̴�
    public Animator anim;           // �ִϸ�����
    public MovableBase focusTarget; // Ÿ��
    public bool isAlly;             // �� ĳ������ ��ǥ��
    // �߻� �޼����
    abstract public float GetDamage(float damage, MovableBase from);   // �ǰݰ��� �޼���

    protected virtual void Start()
    {
        stat = GetComponent<StatBase>();
        collider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    public virtual void AddWeapon(string wantName) { }
}
