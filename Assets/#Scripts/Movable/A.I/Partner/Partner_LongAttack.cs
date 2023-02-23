using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Partner_LongAttack : PartnerBase
{
    WeaponBase currentWeapon;
    public string weaponName;

    protected override void Start()
    {
        base.Start();

        

    }
    protected override void Update()
    {
        base.Update();
    }


    public override void Anim_Attack()
    {
        if (atkType == AttackType.Long)
        {
            currentWeapon.OnAttack(focusTarget.GetComponent<MonsterBase>());
        }
    }

    
}
