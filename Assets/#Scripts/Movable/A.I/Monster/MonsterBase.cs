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
    MonsterState monsterState;

    MState[] states;
    MState currentState;

    public MovableBase targetCharacter;

    int getHitStack = 0; // ������ ���� Ÿ������ �Ҵ��ϱ� ���� ����

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AtkRange);     // ���ݹ���üũ

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FindRange);    // ��������üũ
    }
    protected override void Start()
    {
        base.Start();

        Setup();
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
        states = new MState[5];
        states[(int)MonsterState.Idle] = new MonsterStates.MIdle();
        states[(int)MonsterState.Walk] = new MonsterStates.MMove();
        states[(int)MonsterState.Attack] = new MonsterStates.MAttack();
        states[(int)MonsterState.Die] = new MonsterStates.MDie();
        states[(int)MonsterState.Skill] = new MonsterStates.MSkill();

        ChangeState(MonsterState.Idle);
    }


    public override float GetDamage(float damage, MovableBase from)
    {
        base.GetDamage(damage, from);

        if (getHitStack == 0 && targetCharacter != from)
        {   // ���� �� ���͸� �����ϴ� ĳ���Ͱ� Ÿ���� �ƴ϶�� �� ������ ĳ���͸� Ÿ������ ���� (1ȸ ����)
            targetCharacter = from;
            getHitStack++;
        }

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
                if (targetCharacter != null)
                {
                    GameObject effect = Instantiate(Managers.swordEffect, atkCollider.transform);
                }
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

   



    public override float Attack()
    {
        throw new System.NotImplementedException();
    }
    public override float GetHeal()
    {
        throw new System.NotImplementedException();
    }
    public override void Die()
    {
        Destroy(gameObject);
    }
}
