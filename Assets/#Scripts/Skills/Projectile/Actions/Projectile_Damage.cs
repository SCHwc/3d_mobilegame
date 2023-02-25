using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Damage : ProjectileAction
{
    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        // ������ ���ݷ����� Ÿ�ٿ��� �������� �ش�. (�Ӽ��������� �� �������� ���� �߰�)
        target?.GetDamage(proj.owner.Stat.AttackPower, proj.owner);
    }
}
