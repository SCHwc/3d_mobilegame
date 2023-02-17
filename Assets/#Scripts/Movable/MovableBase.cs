using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBase))]
public abstract class MovableBase : MonoBehaviour
{
    protected StatBase stat;
    protected Collider collider; // �ݶ��̴�
    protected Animator anim;     // �ִϸ�����
    protected Enums.State state = Enums.State.Default; // ����(FSM�� ����)

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
