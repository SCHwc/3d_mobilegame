using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIBase : MovableBase
{
    public AttackType atkType; // 이 캐릭터의 전투방식 (근접, 원거리)

    [SerializeField, Tooltip("캐릭터의 공격범위"), InspectorName("공격범위")]
    protected float _atkRange = 0f;
    public float AtkRange
    {
        get => _atkRange;
        set
        {
            _atkRange = value;
        }
    }

    [SerializeField, Tooltip("캐릭터의 감지범위"), InspectorName("감지범위")]
    protected float _findRange = 0f;
    public float FindRange
    {
        get => _findRange;
        set
        {
            _findRange = value;
        }
    }
    public NavMeshAgent agent;

    protected WeaponBase equipSkill;
    public string skillName;
    public bool skillTracking;
    public float skillCoolTime;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.stat.MoveSpeed; // 네브메쉬의 속도를 캐릭터의 이동속도로 설정
        equipSkill = AddWeapon(skillName, skillCoolTime);

    }

    public override float GetDamage(float damage, MovableBase from)
    {
        if (buffs.Count < 1) { StartCoroutine(GetDamageMeshs()); }


        damage -= Stat.DefensePower;                               // 방어력만큼 데미지 감소
        damage = Mathf.Max(0, damage);                             // 0보다 작아지지 않게
        if (Stat.CurrentHp < damage) { damage = Stat.CurrentHp; }  // 데미지가 체력보다 높다면 데미지를 체력과 같게
        if (damage == 0) { return 0; }

        Stat.CurrentHp -= damage;                                  // 예외처리들을 끝내고나서 데미지 받기

        return damage;
    }
    public IEnumerator GetDamageMeshs()
    {
        foreach (SkinnedMeshRenderer mesh in meshs) { mesh.material.color = Color.red; }
        yield return new WaitForSeconds(0.25f);

        foreach (SkinnedMeshRenderer mesh in meshs) { mesh.material.color = Color.white; }
        yield break;
    }

    public virtual void SkillCasting()
    {
        if (focusTarget != null && equipSkill.CurrentCoolTime <= 0)
        {
            anim.SetTrigger("isSkill");
            StartCoroutine(CoolTimeCycle(equipSkill));
        }
        else { return; }
    }

    public virtual void OffCollider()
    {
        collider.enabled = false;
    }

    IEnumerator CoolTimeCycle(WeaponBase currentWeapon)
    {
        currentWeapon.CurrentCoolTime = currentWeapon.CoolTime;
        while (currentWeapon.CurrentCoolTime > 0)
        {
            currentWeapon.CurrentCoolTime -= Time.deltaTime;
        }

        yield return null;
    }

    abstract public void Die();
}
