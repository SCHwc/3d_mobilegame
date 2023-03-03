using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_IceSpike : WeaponBase
{
    public Weapon_IceSpike(MovableBase wantOwner) : base(wantOwner)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/IceSpike");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(target, target.transform.position, false);
    }
}
