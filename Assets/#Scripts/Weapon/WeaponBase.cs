using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase
{
    protected MovableBase owner;       // 주인
    protected GameObject spawnPrefab;  // 오브젝트 

    //public WeaponInfo info { get; protected set; }

    public WeaponBase(MovableBase wantOwner)
    {
        owner = wantOwner;
    }

    protected virtual ProjectileBase Shot(MovableBase wantTarget, Vector3 wantPosition, bool wantTracking)
    {
        // 스킬 오브젝트 소환하고 위치 할당
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab).GetComponent<ProjectileBase>();
        proj.Initialize(owner, wantTarget, wantTracking);
        wantPosition.y = 1f;
        proj.transform.position = wantPosition;


        return proj;
    }

    // 스킬의 역할 메서드
    public virtual void OnAttack(MovableBase target, bool wantTracking) { }
}
