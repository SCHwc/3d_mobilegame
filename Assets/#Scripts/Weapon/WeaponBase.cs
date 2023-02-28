using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase
{
    protected MovableBase owner;       // ����
    protected GameObject spawnPrefab;  // ������Ʈ

    public WeaponBase(MovableBase wantOwner)
    {
        owner = wantOwner;
    }

    protected virtual ProjectileBase Shot(MovableBase wanttarget, Vector3 wantPosition, bool wantTracking)
    {
        // ��ų ������Ʈ ��ȯ�ϰ� ��ġ �Ҵ�
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab).GetComponent<ProjectileBase>();
        proj.Initialize(owner, wanttarget, wantTracking);
        wantPosition.y = 1f;
        proj.transform.position = wantPosition;

        return proj;
    }

    public virtual void OnAttack(MovableBase target, bool wantTracking) { }
}
