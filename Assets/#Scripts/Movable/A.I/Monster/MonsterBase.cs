using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MState
{
    protected float distance;
    /// <summary> 해당 상태를 시작할 때 1회 호출 </summary>
    public abstract void OnEnter(MonsterBase partner);

    /// <summary> 해당 상태의 업데이트 호출 </summary>
    public abstract void OnUpdate(MonsterBase partner);

    /// <summary> 해당 상태를 종료할 때 1회 호출 </summary>
    public abstract void OnExit(MonsterBase partner);
}

public class MonsterBase : AIBase
{
    MonsterState monsterState;

    MState[] states;
    MState currentState;

    public MovableBase targetCharacter;

    int getHitStack = 0; // 공격한 적을 타겟으로 할당하기 위한 변수

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AtkRange);     // 공격범위체크

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FindRange);    // 감지범위체크
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
        // 동료 캐릭터가 가질 수 있는 상태 개수만큼 메모리 할당, 각 상태에 클래스 메모리 할당
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
        {   // 전투 중 몬스터를 공격하는 캐릭터가 타겟이 아니라면 그 공격한 캐릭터를 타겟으로 설정 (1회 한정)
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
        /// <summary> 예외처리 </summary>
        if (states[(int)newState] == null) { return; } // 변경하려는 상태가 비어있는 상태면 리턴
        /// <summary> 예외처리 </summary>
        if (currentState != null) { currentState.OnExit(this); } // 현재 상태가 있다면 끝내기

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
    {   // 공격 애니메이션이 끝날 때 공격 대기시간을 초기화 하기 위해 넣어줄 함수
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
