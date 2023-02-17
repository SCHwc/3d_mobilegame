using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PState
{
    /// <summary> �ش� ���¸� ������ �� 1ȸ ȣ�� </summary>
    public abstract void OnEnter(PartnerBase partner);

    /// <summary> �ش� ������ ������Ʈ ȣ�� </summary>
    public abstract void OnUpdate(PartnerBase partner);

    /// <summary> �ش� ���¸� ������ �� 1ȸ ȣ�� </summary>
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
        // ���� ĳ���Ͱ� ���� �� �ִ� ���� ������ŭ �޸� �Ҵ�, �� ���¿� Ŭ���� �޸� �Ҵ�
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
        /// <summary> ����ó�� </summary>
        if (states[(int)newState] == null) { return; } // �����Ϸ��� ���°� ����ִ� ���¸� ����
        /// <summary> ����ó�� </summary>
        if (currentState != null) { currentState.OnExit(this); } // ���� ���°� �ִٸ� ������


        currentState = states[(int)newState];
        currentState.OnEnter(this);
    }
    private void OnEnable()
    {// �����Ҷ����� �ʱ�ȭ (�̵��ÿ� ������Ʈ�� �� ����)
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
    public override float Attack() // Ÿ���� �ְ� Ÿ���� ���� ���ݹ����� ���� ��
    {
        partnerState = PartnerState.Attack;
        return 0;
    }

    public override float GetDamage(float damage, MovableBase from) // ������ �޾��� ��
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
