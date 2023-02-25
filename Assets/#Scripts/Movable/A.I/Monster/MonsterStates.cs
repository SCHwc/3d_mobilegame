using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterStates
{
    public class MIdle : MState
    {
        float findRange = 0;
        public override void OnEnter(MonsterBase monster)
        {
            monster.anim.SetBool("isBattle", false);
            monster.getHitStack = 0;
            findRange = 0;
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.focusTarget == null)
            {
                if (findRange < monster.FindRange) { findRange += Time.deltaTime; }
                else { findRange = 0; }

                // ĳ������ ����������ŭ ���� �����Ѵ�.
                Collider[] col = Physics.OverlapSphere(monster.gameObject.transform.position, findRange);

                foreach (Collider collider in col)
                {
                    if (collider.tag == "Partner"||collider.tag=="Player") 
                    {
                        monster.focusTarget = collider.gameObject.transform;
                        break;
                    }
                }
            }

            if (monster.focusTarget != null)
            {
                distance = (monster.focusTarget.position - monster.gameObject.transform.position).magnitude;

                if (distance > monster.AtkRange) // ���ݹ������� �ִٸ� �̵�
                {
                    monster.ChangeState(MonsterState.Walk);
                }
                else                             // ���ݹ������� ������ ����
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
            if (monster.focusTarget == null)
            {
                distance = 0;
                monster.ChangeState(MonsterState.Idle);
            }
            else
            {
                distance = (monster.focusTarget.position - monster.gameObject.transform.position).magnitude;
            }

            monster.anim.SetFloat("isMove", monster.agent.velocity.magnitude);

            if (distance > monster.AtkRange)
            {             // ���ݹ������� ũ�ٸ� �̵�
                monster.agent.SetDestination(monster.focusTarget.position);
            }
            else if (distance <= monster.AtkRange)
            {             // ���ݹ������� �۰ų� ���ٸ� �������� ����
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
            if (monster.focusTarget != null)
            {
                monster.transform.LookAt(monster.focusTarget);
            }
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.focusTarget == null)
            {   // Ÿ���� ���ٸ� �Ҵ�� Ÿ���� ����� �ٽ� �����·� ���ư���.
                distance = 0;
                monster.ChangeState(MonsterState.Idle);
            }
            else
            {   // Ÿ���� �ִٸ� Ÿ�ٰ��� �Ÿ� �Ҵ�
                distance = (monster.focusTarget.position - monster.gameObject.transform.position).magnitude;
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
            Debug.Log($"{monster.gameObject.name}�� �׾���!");
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