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

    [SerializeField, Tooltip("ĳ������ ���ݹ���"), InspectorName("���ݹ���")]
    protected float _atkRange = 0f;
    public float AtkRange
    {
        get => _atkRange;
        set
        {
            _atkRange= value;
        }
    }

    [SerializeField, Tooltip("ĳ������ ��������"), InspectorName("��������")]
    protected float _findRange = 0f;
    public float FindRange
    {
        get => _findRange;
        set
        {
            _findRange = value;
        }
    }
    public NavMeshAgent agent;

    protected bool isBattle;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.stat.MoveSpeed; // �׺�޽��� �ӵ��� ĳ������ �̵��ӵ��� ����
    }


    abstract public void Idle();
}
