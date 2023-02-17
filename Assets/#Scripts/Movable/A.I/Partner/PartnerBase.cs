using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerBase : AIBase
{
    PartnerState partnerState;

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {// �����Ҷ����� �ʱ�ȭ (�̵��ÿ� ������Ʈ�� �� ����)
        Idle();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
    public override void Idle()
    {
        partnerState = PartnerState.Idle;
    }
    public override float Attack() // Ÿ���� �ְ� Ÿ���� ���� ���ݹ����� ���� ��
    {
        partnerState = PartnerState.Attack;
        return 0;
    }

    public override float GetDamage() // ������ �޾��� ��
    {
        throw new System.NotImplementedException();
    }

    public override float GetHeal()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        partnerState = PartnerState.Walk;
    }
}
