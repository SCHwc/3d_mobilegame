using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PartnerStates
{
    public class PIdle : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            Debug.Log("Idle ���� ����");
        }


        public override void OnUpdate(PartnerBase partner)
        {
            // ĳ������ ����������ŭ ���� �����Ѵ�.
            Collider[] col = Physics.OverlapSphere(partner.gameObject.transform.position, partner.FindRange);

            foreach (Collider collider in col)
            {
                if (collider.tag == "Monster") // ���� ���Ͱ� �ִٸ� Ÿ�� ����
                {
                    partner.targetMonster = collider.gameObject;
                    break;
                }
                else // ���ٸ� ?
                {

                }
            }

            if (partner.targetMonster != null) // Ÿ���� ���� ��
            {                                  // Ÿ�ٰ��� �Ÿ�
                float distance = (partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude;
                if (distance > partner.AtkRange) // ���ݹ������� �ִٸ� �̵�
                {
                    partner.ChangeState(PartnerState.Walk);
                }
                else                             // ���ݹ������� ������ ����
                {
                    partner.ChangeState(PartnerState.Attack);
                }
            }
        }

        public override void OnExit(PartnerBase partner)
        {
            Debug.Log("Idle ���� ����");
        }
    }

    public class PMove : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            Debug.Log("Move ���� ����");
            partner.agent.enabled = true;
        }

        public override void OnUpdate(PartnerBase partner)
        {

            if (partner.targetMonster == null) // Ÿ���� ������
            {      // �÷��̾���� �Ÿ� ����
                //if() 
                //partner.ChangeState(PartnerState.Idle); 
            }
            else if ((partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude > partner.AtkRange)
            {             // ���ݹ������� ũ�ٸ� �̵�
                partner.agent.SetDestination(partner.targetMonster.transform.position);
            }
            else if ((partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude <= partner.AtkRange)
            {             // ���ݹ������� �۰ų� ���ٸ� �������� ����
                partner.ChangeState(PartnerState.Attack);
            }
        }

        public override void OnExit(PartnerBase partner)
        {
            Debug.Log("Move ���� ����");
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

