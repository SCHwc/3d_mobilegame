using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PState
{
    /// <summary> 해당 상태를 시작할 때 1회 호출 </summary>
    public abstract void OnEnter(PartnerBase partner);

    /// <summary> 해당 상태의 업데이트 호출 </summary>
    public abstract void OnUpdate(PartnerBase partner);

    /// <summary> 해당 상태를 종료할 때 1회 호출 </summary>
    public abstract void OnExit(PartnerBase partner);
}


public class PartnerBase : AIBase
{
    PartnerState partnerState;

    PState[] states;
    PState currentState;

    public GameObject targetMonster;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate(this);
        }
    }

    public void Setup()
    {
        // 동료 캐릭터가 가질 수 있는 상태 개수만큼 메모리 할당, 각 상태에 클래스 메모리 할당
        states = new PState[4];
        states[(int)PartnerState.Idle] = new PartnerStates.PIdle();
        states[(int)PartnerState.Walk] = new PartnerStates.PMove();

        ChangeState(PartnerState.Idle);
    }
    void UpdateState()
    {
        switch (partnerState)
        {
            case PartnerState.Idle:
                isBattle = false;
                break;
            case PartnerState.Walk:
                isBattle = true;
                agent.enabled = true;
                break;
            case PartnerState.Attack:
                isBattle = true;
                agent.enabled = false;
                break;
            case PartnerState.Die:
                isBattle = false;
                agent.enabled = false;
                break;
        }
    }
    public void ChangeState(PartnerState newState)
    {
        /// <summary> 예외처리 </summary>
        if (states[(int)newState] == null) { return; } // 변경하려는 상태가 비어있는 상태면 리턴
        /// <summary> 예외처리 </summary>
        if (currentState != null) { currentState.OnExit(this); } // 현재 상태가 있다면 끝내기


        currentState = states[(int)newState];
        currentState.OnEnter(this);
    }
    private void OnEnable()
    {// 등장할때마다 초기화 (이동시에 오브젝트를 끌 예정)
        Idle();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
    public override void Idle()
    {
        partnerState = PartnerState.Idle;
    }
    public override float Attack() // 타겟이 있고 타겟이 나의 공격범위에 있을 때
    {
        partnerState = PartnerState.Attack;
        return 0;
    }

    public override float GetDamage(float damage, MovableBase from) // 공격을 받았을 때
    {
        throw new System.NotImplementedException();
    }

    public override float GetHeal()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        partnerState = PartnerState.Walk;
    }
}
