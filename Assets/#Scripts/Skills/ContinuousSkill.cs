using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSkill : SkillBase
{
    [SerializeField] protected float holdTime; // 스킬 유지시간
    protected float holdTimeCheck = 0f;        // 스킬 유지시간 체크
    protected bool onSkill = false; // 스킬이 켜졌는지

    [SerializeField] protected float damageDelay; // 데미지 들어가는 딜레이
    protected float damageDelayCheck = 0f;        // 데미지 딜레이 체크
    protected float colliderActiveTime = 0.1f;    // 콜라이더 잠깐 켜지는 시간

    protected Collider col; // 스킬 콜라이더
    
    protected override void Awake()
    {
        base.Awake();
        col = Utils.GetOrAddComponent<Collider>(gameObject);
        col.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        // 파티클 자동 정지
        if(onSkill)
        {
            // 지속시간 체크
            holdTimeCheck += Time.deltaTime;
            if(holdTimeCheck >= holdTime)
            {
                onSkill = false;
                //holdTimeCheck = 0f;
                //damageDelayCheck = 0f;
                ps.Stop();
                Destroy(gameObject);
            }

            // 데미지 딜레이 체크
            damageDelayCheck += Time.deltaTime;
            if(damageDelayCheck >= damageDelay)
            {
                StartCoroutine(ActiveCol());
                damageDelayCheck = 0f;
            }
        }
    }

    public override void PlaySkill(MovableBase owner)
    {
        this.owner = owner;
        ps.Play();
        onSkill = true;
    }

    protected IEnumerator ActiveCol()
    {
        col.enabled = true;
        yield return new WaitForFixedUpdate();
        col.enabled = false;
    }

    // 데미지 입히기
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            monster.GetDamage(owner.Stat.AttackPower + owner.Stat.AttackPower * damageMultiflierPercentage * 0.01f, owner);
        }
    }
}
