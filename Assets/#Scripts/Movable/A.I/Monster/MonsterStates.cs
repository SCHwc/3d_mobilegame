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
            monster.focusTarget = null;
        }

        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.focusTarget == null)
            {
                float nearestDistance = 10f;
                int nearestIndex = -1;

                // 캐릭터의 감지범위만큼 적을 감지한다.
                Collider[] col = Physics.OverlapSphere(monster.gameObject.transform.position, monster.FindRange, 1 << 9 | 1 << 10);
                for (int i = 0; i < col.Length; i++)
                {
                    float tempDistance = (col[i].gameObject.transform.position - monster.gameObject.transform.position).magnitude;
                    if (tempDistance < nearestDistance)
                    {
                        nearestDistance = tempDistance;
                        nearestIndex = i;
                    }
                }

                if (nearestIndex > -1)
                {
                    monster.focusTarget = col[nearestIndex].GetComponent<MovableBase>();
                }
                else { monster.focusTarget = null; }

            }

            if (monster.focusTarget != null)
            {
                distance = (monster.focusTarget.transform.position - monster.gameObject.transform.position).magnitude;
                monster.findEffect.SetActive(true);

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
        float moveTime = 0f;
        public override void OnEnter(MonsterBase monster)
        {
            monster.agent.enabled = true;
            moveTime = 0f;
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.focusTarget == null)
            {
                distance = 0;
                monster.ChangeState(MonsterState.Idle);
            }
            else
            {
                distance = (monster.focusTarget.transform.position - monster.gameObject.transform.position).magnitude;
            }

            monster.anim.SetFloat("isMove", monster.agent.velocity.magnitude);

            if (distance > monster.AtkRange)
            {             // 공격범위보다 크다면 이동
                monster.agent.SetDestination(monster.focusTarget.transform.position);
                moveTime += Time.deltaTime;
                if (moveTime >= 3.5f)
                {
                    monster.focusTarget = null;
                    monster.ChangeState(MonsterState.Idle);
                }
            }
            else if (distance <= monster.AtkRange)
            {             // 공격범위보다 작거나 같다면 공격으로 변경
                monster.ChangeState(MonsterState.Attack);
                if (moveTime > 0) { moveTime = 0f; }
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
            if (monster.focusTarget != null)
            {
                monster.transform.LookAt(monster.focusTarget.transform);
            }
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.focusTarget == null)
            {   // 타겟이 없다면 할당된 타겟을 지우고 다시 대기상태로 돌아간다.
                distance = 0;
                monster.ChangeState(MonsterState.Idle);
            }
            else
            {   // 타겟이 있다면 타겟과의 거리 할당
                distance = (monster.focusTarget.transform.position - monster.gameObject.transform.position).magnitude;
                monster.transform.LookAt(monster.focusTarget.transform);
            }

            if (monster.focusTarget != null && distance > monster.AtkRange)
            {
                monster.ChangeState(MonsterState.Walk);
            }
            else if (monster.focusTarget != null && distance <= monster.AtkRange && monster.Stat.AttackSpeed <= 0)
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
            monster.anim.SetTrigger("isDie");
        }
        public override void OnUpdate(MonsterBase monster)
        {
        }

        public override void OnExit(MonsterBase monster)
        {
        }

    }
}