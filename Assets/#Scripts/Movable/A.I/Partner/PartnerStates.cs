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
            partner.anim.SetBool("isBattle", false);
        }


        public override void OnUpdate(PartnerBase partner)
        {
            if (partner.targetMonster == null)
            {
                // ĳ������ ����������ŭ ���� �����Ѵ�.
                Collider[] col = Physics.OverlapSphere(partner.gameObject.transform.position, partner.FindRange);

                foreach (Collider collider in col)
                {
                    if (collider.tag == "Monster") // ���� ���Ͱ� �ִٸ� Ÿ�� ����
                    {
                        Debug.Log("���� �߰� !");
                        partner.targetMonster = collider.gameObject;
                        break;
                    }
                }
            }

            if (partner.targetMonster != null) // Ÿ���� ���� ��
            {                                  // Ÿ�ٰ��� �Ÿ�
                distance = (partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude;
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
            if (partner.targetMonster == null)
            {   // Ÿ���� ���ٸ� �ٽ� �����·� ���ư���.
                distance = 0;
                partner.ChangeState(PartnerState.Idle);
            }
            else
            {    // Ÿ���� ������ �Ÿ� �Ҵ�
                distance = (partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude;
            }

            partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);

            if (distance > partner.AtkRange)
            {             // ���ݹ������� ũ�ٸ� �̵�
                partner.agent.SetDestination(partner.targetMonster.transform.position);
            }
            else if (distance <= partner.AtkRange)
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
            Debug.Log("Attack ���� ����");

            partner.Stat.AttackSpeed = 0f;
        }

        public override void OnUpdate(PartnerBase partner)
        {
            if (partner.targetMonster == null)
            {   // Ÿ���� ���ٸ� �Ҵ�� Ÿ���� ����� �ٽ� �����·� ���ư���.
                distance = 0;
                partner.ChangeState(PartnerState.Idle);
            }
            else
            {   // Ÿ���� �ִٸ� Ÿ�ٰ��� �Ÿ� �Ҵ�
                distance = (partner.targetMonster.transform.position - partner.gameObject.transform.position).magnitude; ;
            }

            if (partner.targetMonster != null && distance > partner.AtkRange)
            {
                partner.ChangeState(PartnerState.Walk);
            }
            else if (partner.targetMonster != null && distance <= partner.AtkRange && partner.Stat.AttackSpeed <= 0)
            {
                partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
                partner.anim.SetBool("isBattle", true);
                Debug.Log("����!");
            }

            if (partner.Stat.AttackSpeed > 0)
            {
                Debug.Log($"���� ��� �����ð� - {partner.Stat.AttackSpeed}");
                partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
                partner.anim.SetBool("isBattle", false);
                partner.Stat.AttackSpeed -= Time.deltaTime;
            }
        }

        public override void OnExit(PartnerBase partner)
        {
            Debug.Log("Attack ���� ����");
            partner.anim.SetBool("isBattle", false);
        }
    }

    public class PDie : PState
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

