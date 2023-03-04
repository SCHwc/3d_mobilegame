using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase
{
    protected MovableBase owner;
    protected GameObject spawnPrefab;

    // 해당 스킬의 쿨타임
    protected float _coolTime;
    public float CoolTime
    {
        get => _coolTime;
        set
        {
            value = _coolTime;
        }
    }    
    
    // 해당 스킬의 현재 쿨타임
    protected float _currentCoolTime;
    public float CurrentCoolTime
    {
        get => _currentCoolTime;
        set => Mathf.Clamp(value, 0, CoolTime);
    }
    public float coolTimeRate { get => CurrentCoolTime / CoolTime; }

    public WeaponBase(MovableBase wantOwner, float wantCoolTime)
    {
        owner = wantOwner;
        CoolTime = wantCoolTime;
        CurrentCoolTime = 0;
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
