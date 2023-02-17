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
        }


        public override void OnUpdate(PartnerBase partner)
        {
            // 캐릭터의 감지범위만큼 적을 감지한다.
            Collider[] col = Physics.OverlapSphere(partner.gameObject.transform.position, partner.FindRange);

            foreach (Collider collider in col)
            {
                if (collider.tag == "Monster") // 만약 몬스터가 있다면 타겟 설정
                {
                    partner.targetMonster = collider.gameObject;
                    break;
                }
                else // 없다면 ?
                {

                }
            }

            if (partner.targetMonster != null) // 타겟이 있을 때
            {                                  // 타겟과의 거리
                float distance = (partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude;
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

            if (partner.targetMonster == null) // 타겟이 없을때
            {      // 플레이어와의 거리 측정
                //if() 
                //partner.ChangeState(PartnerState.Idle); 
            }
            else if ((partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude > partner.AtkRange)
            {             // 공격범위보다 크다면 이동
                partner.agent.SetDestination(partner.targetMonster.transform.position);
            }
            else if ((partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude <= partner.AtkRange)
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
            throw new System.NotImplementedException();
        }

        public override void OnUpdate(PartnerBase partner)
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit(PartnerBase partner)
        {
            throw new System.NotImplementedException();
        }
    }
}

