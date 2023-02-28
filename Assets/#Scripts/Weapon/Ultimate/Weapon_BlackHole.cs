using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_BlackHole : WeaponBase
{
    public Weapon_BlackHole(MovableBase wantOwner) : base(wantOwner)
    {
        spawnPrefab = Resources.Load<GameObject>($"Prefabs/Skills/BlackHole");
    }

    protected override ProjectileBase Shot(MovableBase wanttarget, Vector3 wantPosition, bool wantTracking)
    {
        ProjectileBase proj = null;

        if (wanttarget != null)
        {
            // 스킬 오브젝트 소환하고 위치 할당
            proj = GameObject.Instantiate(spawnPrefab).GetComponent<ProjectileBase>();
            proj.Initialize(owner, wanttarget, wantTracking);
            proj.transform.position = wanttarget.transform.position;
        }

        return proj;
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(target, owner.transform.position, wantTracking);
    }

}
