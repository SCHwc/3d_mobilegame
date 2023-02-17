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
    {// 등장할때마다 초기화 (이동시에 오브젝트를 끌 예정)
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
    public override float Attack() // 타겟이 있고 타겟이 나의 공격범위에 있을 때
    {
        partnerState = PartnerState.Attack;
        return 0;
    }

    public override float GetDamage() // 공격을 받았을 때
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
