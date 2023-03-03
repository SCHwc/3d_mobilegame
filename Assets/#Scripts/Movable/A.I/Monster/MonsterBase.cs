using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MState
{
    protected float distance;
    /// <summary> �ش� ���¸� ������ �� 1ȸ ȣ�� </summary>
    public abstract void OnEnter(MonsterBase partner);

    /// <summary> �ش� ������ ������Ʈ ȣ�� </summary>
    public abstract void OnUpdate(MonsterBase partner);

    /// <summary> �ش� ���¸� ������ �� 1ȸ ȣ�� </summary>
    public abstract void OnExit(MonsterBase partner);
}

public class MonsterBase : AIBase
{
    MState[] states;
    MState currentState;

    public GameObject findEffect;

    protected override void Start()
    {
        base.Start();

        Setup();
    }

    protected override void Update()
    {
        base.Update();
        if (currentState != null)
        {
            currentState.OnUpdate(this);
        }
    }

    public void Setup()
    {
        // ���� ĳ���Ͱ� ���� �� �ִ� ���� ������ŭ �޸� �Ҵ�, �� ���¿� Ŭ���� �޸� �Ҵ�
        states = new MState[5];
        states[(int)MonsterState.Idle] = new MonsterStates.MIdle();
        states[(int)MonsterState.Walk] = new MonsterStates.MMove();
        states[(int)MonsterState.Attack] = new MonsterStates.MAttack();
        states[(int)MonsterState.Die] = new MonsterStates.MDie();

        ChangeState(MonsterState.Idle);

        findEffect = Instantiate(GameManager.findEffect, this.transform);
        findEffect.SetActive(false);
    }


    public override float GetDamage(float damage, MovableBase from)
    {
        base.GetDamage(damage, from);

        if (focusTarget == null) { focusTarget = from; }

        if (Stat.CurrentHp <= 0)
        {
            ChangeState(MonsterState.Die);
        }

        return damage;
    }

    public void ChangeState(MonsterState newState)
    {
        /// <summary> ����ó�� </summary>
        if (states[(int)newState] == null) { return; } // �����Ϸ��� ���°� ����ִ� ���¸� ����
        /// <summary> ����ó�� </summary>
        if (currentState != null) { currentState.OnExit(this); } // ���� ���°� �ִٸ� ������

        currentState = states[(int)newState];
        currentState.OnEnter(this);
    }

    public void Anim_Attack()
    {
        switch (atkType)
        {
            case AttackType.Short:
                atkCollider.enabled = true;
                Invoke("AttackColliderOff", 0.3f);
                break;
            case AttackType.Long:
                break;
        };
    }
    void AttackColliderOff()
    {
        atkCollider.enabled = false;
    }

    public void Anim_AttackTimeCheck()
    {   // ���� �ִϸ��̼��� ���� �� ���� ���ð��� �ʱ�ȭ �ϱ� ���� �־��� �Լ�
        Stat.AttackSpeed = Stat.AttackDelay;
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
