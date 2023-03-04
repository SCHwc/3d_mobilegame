using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_SwordStorm : WeaponBase
{
    public Weapon_SwordStorm(MovableBase wantOwner) : base(wantOwner)
    {
        spawnPrefab = Resources.Load<GameObject>($"Prefabs/Skills/SwordStorm");
    }

    protected override ProjectileBase Shot(MovableBase wanttarget, Vector3 wantPosition, bool wantTracking)
    {
        // ��ų ������Ʈ ��ȯ�ϰ� ��ġ �Ҵ�
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab, owner.transform).GetComponent<ProjectileBase>();
        proj.Initialize(owner, wanttarget, wantTracking);

        return proj;
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(target, owner.transform.position, wantTracking);
    }


}
