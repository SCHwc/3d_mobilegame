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
        states = new PState[4];
        states[(int)PartnerState.Idle] = new PartnerStates.PIdle();
        states[(int)PartnerState.Walk] = new PartnerStates.PMove();
        states[(int)PartnerState.Attack] = new PartnerStates.PAttack();
        states[(int)PartnerState.Die] = new PartnerStates.PDie();

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
            Invoke("AttackColliderOff", 0.3f);
        }
    }
    void AttackColliderOff()
    {
        atkCollider.enabled = false;
    }

    public void Anim_ComboAttack()
    {   // ���� ��������� ���� �� �ִϸ��̼��� ���� �� �־��� �Լ�
        if (focusTarget.gameObject == null)
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

    public void Anim_Skill()
    {
        equipSkill.OnAttack(focusTarget.GetComponent<MonsterBase>(), skillTracking);
    }

    public override void AddWeapon(string wantName)
    {
        switch (wantName)
        {
            case "Provoke":
                equipSkill = new Weapon_Provoke(this);
                break;
            case "FireShield":
                break;
            case "IceSpear":
                break;
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
