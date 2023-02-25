using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSkill : SkillBase
{
    [SerializeField] protected float holdTime; // ��ų �����ð�
    protected float holdTimeCheck = 0f;        // ��ų �����ð� üũ
    protected bool onSkill = false; // ��ų�� ��������

    [SerializeField] protected float damageDelay; // ������ ���� ������
    protected float damageDelayCheck = 0f;        // ������ ������ üũ
    protected float colliderActiveTime = 0.1f;    // �ݶ��̴� ��� ������ �ð�

    protected Collider col; // ��ų �ݶ��̴�
    
    protected override void Awake()
    {
        base.Awake();
        col = Utils.GetOrAddComponent<Collider>(gameObject);
        col.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        // ��ƼŬ �ڵ� ����
        if(onSkill)
        {
            // ���ӽð� üũ
            holdTimeCheck += Time.deltaTime;
            if(holdTimeCheck >= holdTime)
            {
                onSkill = false;
                //holdTimeCheck = 0f;
                //damageDelayCheck = 0f;
                ps.Stop();
                Destroy(gameObject);
            }

            // ������ ������ üũ
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

    // ������ ������
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            monster.GetDamage(owner.Stat.AttackPower + owner.Stat.AttackPower * damageMultiflierPercentage * 0.01f, owner);
        }
    }
}
