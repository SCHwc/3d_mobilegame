using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIBase : MovableBase
{
    [SerializeField, Tooltip("캐릭터의 공격 딜레이"), InspectorName("공격 딜레이")]
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
        agent.speed = stat.MoveSpeed; // 네브메쉬의 속도를 캐릭터의 이동속도로 설정
    }


    abstract public void Idle();
}
