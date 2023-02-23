using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Damage : ProjectileAction
{
    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        target?.GetDamage(proj.owner.Stat.AttackPower, proj.owner);
    }
}
