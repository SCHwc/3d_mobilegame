using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterStates
{
    public class MIdle : MState
    {
        public override void OnEnter(MonsterBase monster)
        {
            monster.anim.SetBool("isBattle", false);
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.targetCharacter != null)
            {
                distance = (monster.targetCharacter.transform.position - monster.gameObject.transform.position).magnitude;

                if (distance > monster.AtkRange) // 공격범위보다 멀다면 이동
                {
                    monster.ChangeState(MonsterState.Walk);
                }
                else                             // 공격범위보다 가까우면 공격
                {
                    monster.ChangeState(MonsterState.Attack);
                }
            }
        }

        public override void OnExit(MonsterBase monster)
        {
        }
    }

    public class MMove : MState
    {
        public override void OnEnter(MonsterBase monster)
        {
            
            monster.agent.enabled = true;
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.targetCharacter == null)
            {
                distance = 0;
                monster.ChangeState(MonsterState.Idle);
            }
            else
            {
                distance = (monster.targetCharacter.transform.position - monster.gameObject.transform.position).magnitude;
            }

            monster.anim.SetFloat("isMove", monster.agent.velocity.magnitude);

            if (distance > monster.AtkRange)
            {             // 공격범위보다 크다면 이동
                monster.agent.SetDestination(monster.targetCharacter.transform.position);
            }
            else if (distance <= monster.AtkRange)
            {             // 공격범위보다 작거나 같다면 공격으로 변경
                monster.ChangeState(MonsterState.Attack);
            }
        }
        public override void OnExit(MonsterBase monster)
        {
            monster.agent.enabled = false;
        }
    }

    public class MAttack : MState
    {
        public override void OnEnter(MonsterBase monster)
        {
            monster.Stat.AttackSpeed = 0f;
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.targetCharacter == null)
            {   // 타겟이 없다면 할당된 타겟을 지우고 다시 대기상태로 돌아간다.
                distance = 0;
                monster.ChangeState(MonsterState.Idle);
            }
            else
            {   // 타겟이 있다면 타겟과의 거리 할당
                distance = (monster.targetCharacter.transform.position - monster.gameObject.transform.position).magnitude; ;
            }

            if (monster.targetCharacter != null && distance > monster.AtkRange)
            {
                monster.ChangeState(MonsterState.Walk);
            }
            else if (monster.targetCharacter != null && distance <= monster.AtkRange && monster.Stat.AttackSpeed <= 0)
            {
                monster.anim.SetFloat("isMove", monster.agent.velocity.magnitude);
                monster.anim.SetBool("isBattle", true);
            }

            if (monster.Stat.AttackSpeed > 0)
            {
                monster.anim.SetFloat("isMove", monster.agent.velocity.magnitude);
                monster.anim.SetBool("isBattle", false);
                monster.Stat.AttackSpeed -= Time.deltaTime;
            }
        }
        public override void OnExit(MonsterBase monster)
        {
            monster.anim.SetBool("isBattle", false);
        }
    }

    public class MDie : MState
    {
        public override void OnEnter(MonsterBase monster)
        {
            Debug.Log($"{monster.gameObject.name}이 죽었다!");
            monster.anim.SetTrigger("isDie");
        }
        public override void OnUpdate(MonsterBase monster)
        {
        }

        public override void OnExit(MonsterBase monster)
        {
        }

    }

    public class MSkill : MState
    {
        public override void OnEnter(MonsterBase monster)
        {
        }

        public override void OnExit(MonsterBase monster)
        {
        }

        public override void OnUpdate(MonsterBase monster)
        {
        }
    }
}