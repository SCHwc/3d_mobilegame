using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PState
{
    protected float distance;
    /// <summary> �ش� ���¸� ������ �� 1ȸ ȣ�� </summary>
    public abstract void OnEnter(PartnerBase partner);

    /// <summary> �ش� ������ ������Ʈ ȣ�� </summary>
    public abstract void OnUpdate(PartnerBase partner);

    /// <summary> �ش� ���¸� ������ �� 1ȸ ȣ�� </summary>
    public abstract void OnExit(PartnerBase partner);
}


public class PartnerBase : AIBase
{
   protected PartnerState partnerState;

    protected PState[] states;
    protected PState currentState;

    public GameObject targetMonster;

    protected override void Start()
    {
        base.Start();

        Setup();
    }

    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate(this);
        }
    }

    public virtual void Setup()
    {
        // ���� ĳ���Ͱ� ���� �� �ִ� ���� ������ŭ �޸� �Ҵ�, �� ���¿� Ŭ���� �޸� �Ҵ�
        states = new PState[5];
        states[(int)PartnerState.Idle] = new PartnerStates.PIdle();
        states[(int)PartnerState.Walk] = new PartnerStates.PMove();
        states[(int)PartnerState.Attack] = new PartnerStates.PAttack();
        states[(int)PartnerState.Die] = new PartnerStates.PDie();
        states[(int)PartnerState.Skill] = new PartnerStates.PSkill();

        ChangeState(PartnerState.Idle);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AtkRange);     // ���ݹ���üũ

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FindRange);    // ��������üũ
    }



    public override float GetDamage(float damage, MovableBase from) // ������ �޾��� ��
    {
        base.GetDamage(damage, from);

        anim.SetTrigger("GetHit");
        if (Stat.CurrentHp <= 0)
        {
            ChangeState(PartnerState.Die);
        }

        return damage;
    }

    public virtual void Anim_Attack()
    {
        if (atkType == AttackType.Short)
        {
            atkCollider.enabled = true;
            if (targetMonster != null)
            {
                GameObject effect = Instantiate(Managers.swordEffect, atkCollider.transform);
            }
            Invoke("AttackColliderOff", 0.3f);
        }
    }
    void AttackColliderOff()
    {
        atkCollider.enabled = false;
    }

    public void Anim_ComboAttack()
    {   // ���� ��������� ���� �� �ִϸ��̼��� ���� �� �־��� �Լ�
        if (targetMonster.gameObject == null)
        {
            Stat.AttackSpeed = Stat.AttackDelay;
        }
        else
        {
            return;
        }
    }

    public void Anim_AttackTimeCheck()
    {   // ���� �ִϸ��̼��� ���� �� ���� ���ð��� �ʱ�ȭ �ϱ� ���� �־��� �Լ�
        Stat.AttackSpeed = Stat.AttackDelay;

    }












    public override float Attack() // Ÿ���� �ְ� Ÿ���� ���� ���ݹ����� ���� ��
    {
        return 0;
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
