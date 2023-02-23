using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_IceMissile : WeaponBase
{
    public Weapon_IceMissile(MovableBase wantOwner) : base(wantOwner, Managers.weaponInfos["IceMissile"])
    {    }

    public override void OnAttack(MovableBase target)
    {
       
    }
}
