using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected MovableBase owner;
    protected GameObject spawnPrefab;

    public WeaponInfo info { get; protected set; }


    public WeaponBase(MovableBase wantOwner, WeaponInfo wantInfo)
    {
        owner = wantOwner;
        info = wantInfo;
        spawnPrefab = info.spawnPrefab;

    }

    protected virtual ProjectileBase Shot(MovableBase wantTarget, Vector3 wantPosition, bool wantTracking)
    {
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab).GetComponent<ProjectileBase>();
        proj.Initialize(owner, wantTarget, wantTracking);

        proj.transform.position = wantPosition;




        return proj;
    }

    public virtual void OnAttack(MovableBase target) { }
}
