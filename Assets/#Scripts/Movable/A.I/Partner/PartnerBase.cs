using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PState
{
    protected float distance;
    /// <summary> 해당 상태를 시작할 때 1회 호출 </summary>
    public abstract void OnEnter(PartnerBase partner);

    /// <summary> 해당 상태의 업데이트 호출 </summary>
    public abstract void OnUpdate(PartnerBase partner);

    /// <summary> 해당 상태를 종료할 때 1회 호출 </summary>
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
        // 동료 캐릭터가 가질 수 있는 상태 개수만큼 메모리 할당, 각 상태에 클래스 메모리 할당
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
        /// <summary> 예외처리 </summary>
        if (states[(int)newState] == null) { return; } // 변경하려는 상태가 비어있는 상태면 리턴
        /// <summary> 예외처리 </summary>
        if (currentState != null) { currentState.OnExit(this); } // 현재 상태가 있다면 끝내기


        currentState = states[(int)newState];
        currentState.OnEnter(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AtkRange);     // 공격범위체크

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FindRange);    // 감지범위체크
    }



    public override float GetDamage(float damage, MovableBase from) // 공격을 받았을 때
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
    {   // 다음 연계공격이 있을 때 애니메이션이 끝날 때 넣어줄 함수
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
    {   // 공격 애니메이션이 끝날 때 공격 대기시간을 초기화 하기 위해 넣어줄 함수
        Stat.AttackSpeed = Stat.AttackDelay;

    }












    public override float Attack() // 타겟이 있고 타겟이 나의 공격범위에 있을 때
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
