using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Damage : ProjectileAction
{
    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        // 주인의 공격력으로 타겟에게 데미지를 준다. (속성데미지를 더 넣으려면 변수 추가)
        target?.GetDamage(proj.owner.Stat.AttackPower, proj.owner);
    }
}
