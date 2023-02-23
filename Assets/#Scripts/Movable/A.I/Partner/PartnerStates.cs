using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PartnerStates
{

    public class PIdle : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            Debug.Log("Idle 상태 진입");
            partner.anim.SetBool("isBattle", false);
        }


        public override void OnUpdate(PartnerBase partner)
        {
            if (partner.focusTarget == null)
            {
                // 캐릭터의 감지범위만큼 적을 감지한다.
                Collider[] col = Physics.OverlapSphere(partner.gameObject.transform.position, partner.FindRange);

                foreach (Collider collider in col)
                {
                    if (collider.tag == "Monster") // 만약 몬스터가 있다면 타겟 설정
                    {
                        Debug.Log("몬스터 발견 !");
                        partner.focusTarget = collider.gameObject.transform;
                        break;
                    }
                }
            }

            if (partner.focusTarget != null) // 타겟이 있을 때
            {                                  // 타겟과의 거리
                distance = (partner.focusTarget.position - partner.gameObject.transform.position).magnitude;
                if (distance > partner.AtkRange) // 공격범위보다 멀다면 이동
                {
                    partner.ChangeState(PartnerState.Walk);
                }
                else                             // 공격범위보다 가까우면 공격
                {
                    partner.ChangeState(PartnerState.Attack);
                }
            }
        }

        public override void OnExit(PartnerBase partner)
        {
            Debug.Log("Idle 상태 종료");
        }
    }

    public class PMove : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            Debug.Log("Move 상태 진입");
            partner.agent.enabled = true;
        }

        public override void OnUpdate(PartnerBase partner)
        {
            if (partner.focusTarget == null)
            {   // 타겟이 없다면 다시 대기상태로 돌아간다.
                distance = 0;
                partner.ChangeState(PartnerState.Idle);
            }
            else
            {    // 타겟이 있으면 거리 할당
                distance = (partner.focusTarget.position - partner.gameObject.transform.position).magnitude;
            }

            partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);

            if (distance > partner.AtkRange)
            {             // 공격범위보다 크다면 이동
                partner.agent.SetDestination(partner.focusTarget.position);
            }
            else if (distance <= partner.AtkRange)
            {             // 공격범위보다 작거나 같다면 공격으로 변경
                partner.ChangeState(PartnerState.Attack);
            }
        }

        public override void OnExit(PartnerBase partner)
        {
            Debug.Log("Move 상태 종료");
            partner.agent.enabled = false;
        }
    }

    public class PAttack : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            Debug.Log("Attack 상태 진입");

            partner.Stat.AttackSpeed = 0f;

            if (partner.focusTarget != null)
            {
                partner.transform.LookAt(partner.focusTarget);
            }
        }

        public override void OnUpdate(PartnerBase partner)
        {
            if (partner.focusTarget == null)
            {   // 타겟이 없다면 할당된 타겟을 지우고 다시 대기상태로 돌아간다.
                distance = 0;
                partner.ChangeState(PartnerState.Idle);
            }
            else
            {   // 타겟이 있다면 타겟과의 거리 할당
                distance = (partner.focusTarget.position - partner.gameObject.transform.position).magnitude; ;
            }

            if (partner.focusTarget != null && distance > partner.AtkRange)
            {
                partner.ChangeState(PartnerState.Walk);
            }
            else if (partner.focusTarget != null && distance <= partner.AtkRange && partner.Stat.AttackSpeed <= 0)
            {
                partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
                partner.anim.SetBool("isBattle", true);
                Debug.Log("공격!");
            }

            if (partner.Stat.AttackSpeed > 0)
            {
                Debug.Log($"공격 대기 남은시간 - {partner.Stat.AttackSpeed}");
                partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
                partner.anim.SetBool("isBattle", false);
                partner.Stat.AttackSpeed -= Time.deltaTime;
            }
        }

        public override void OnExit(PartnerBase partner)
        {
            Debug.Log("Attack 상태 종료");
            partner.anim.SetBool("isBattle", false);
        }
    }

    public class PDie : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            partner.anim.SetTrigger("isDie");
        }


        public override void OnUpdate(PartnerBase partner)
        {
        }
        public override void OnExit(PartnerBase partner)
        {
        }
    }

    public class PSkill : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit(PartnerBase partner)
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate(PartnerBase partner)
        {
            throw new System.NotImplementedException();
        }
    }
}

