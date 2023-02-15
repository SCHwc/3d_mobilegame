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
    abstract public int Attack();      // ���ݰ��� �޼���
    abstract public int GetDamage();   // �ǰݰ��� �޼���
    abstract public int GetHeal();     // ü��ȸ�� �޼���
    abstract public void Move();        // �̵����� �޼���

    protected virtual void Start()
    {
        stat = GetComponent<StatBase>();
        collider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }
}
