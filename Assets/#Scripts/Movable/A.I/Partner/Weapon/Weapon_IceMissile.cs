using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_IceMissile : WeaponBase
{
    public Weapon_IceMissile(MovableBase wantOwner) : base(wantOwner)
    {
       spawnPrefab= Resources.Load<GameObject>("Prefabs/Projectiles/IceMissile");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(target, owner.transform.position, wantTracking);
    }
}
