using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase
{
    protected MovableBase owner;
    protected GameObject spawnPrefab;

    //public WeaponInfo info { get; protected set; }

    public WeaponBase(MovableBase wantOwner)
    {
        owner = wantOwner;
    }

    protected virtual ProjectileBase Shot(MovableBase wantTarget, Vector3 wantPosition, bool wantTracking)
    {
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab).GetComponent<ProjectileBase>();
        proj.Initialize(owner, wantTarget, wantTracking);
        wantPosition.y = 1f;
        proj.transform.position = wantPosition;


        return proj;
    }

    public virtual void OnAttack(MovableBase target, bool wantTracking) { }
}
