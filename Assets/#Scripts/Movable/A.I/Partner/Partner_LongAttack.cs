using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Partner_LongAttack : PartnerBase
{
    WeaponBase currentWeapon;
    public string weaponName;
    public bool isTracking;

    protected override void Start()
    {
        base.Start();

        AddWeapon(weaponName);

    }
    protected override void Update()
    {
        base.Update();
    }


    public override void Anim_Attack()
    {
        if (atkType == AttackType.Long)
        {
            currentWeapon.OnAttack(focusTarget.GetComponent<MonsterBase>(), isTracking);
        }
    }

    public void AddWeapon(string wantName)
    {
        switch (wantName)
        {
            case "IceMissile":
                currentWeapon = new Weapon_IceMissile(this);
                break;
            case "Fireball":
                currentWeapon = new Weapon_Fireball(this);
                break;
        }
    }
}
