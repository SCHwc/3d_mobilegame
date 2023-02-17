using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIBase : MovableBase
{
    [SerializeField, Tooltip("ĳ������ ���� ������"), InspectorName("���� ������")]
    protected float _atkDelay = 0f;
    public float AtkDelay
    {
        get => _atkDelay;
        set
        {
            _atkDelay = value;
        }
    }

    protected NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stat.MoveSpeed; // �׺�޽��� �ӵ��� ĳ������ �̵��ӵ��� ����
    }


    abstract public void Idle();
}
