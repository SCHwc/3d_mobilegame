using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeableCharacterBase : MovableBase
{
    float moveAttackSpeed = 0.2f;

    public Vector3 inputVector; // 이동입력값

    #region 상태Bool값
    public bool isRun = false; // 달리기 상태
    public bool isDead = false; // 죽었는가
    protected bool isAttacking = false; // 공격중인가
    #endregion

    #region 스킬이름 & WeaponBase
    [SerializeField] protected string normalWeaponName; // 일반스킬 이름
    WeaponBase normalSkill; // 일반스킬
    [SerializeField] protected string ultimateWeaponName; // 궁극기 이름
    WeaponBase ultimateSkill; // 궁극기
    #endregion

    #region 스킬 쿨타임
    [SerializeField] protected float normalSkillCooldown; // 일반스킬 쿨타임
    [SerializeField] protected float ultimateSkillCooldown; // 궁극기 쿨타임
    protected float normalSkillCheckTime = 0;
    protected float ultimateSkillCheckTime = 0;

    public float normalCoolDownRate // 일반스킬 쿨타임 비율 0 ~ 1
    {
        get
        {
            if (normalSkillCheckTime == 0) return 0;
            else if (normalSkillCheckTime < normalSkillCooldown) return normalSkillCheckTime / normalSkillCooldown;
            else return 1;
        }
    }
    public float ultimateCoolDownRate // 궁극기 쿨타임 비율 0 ~ 1
    {
        get
        {
            if (ultimateSkillCheckTime == 0) return 0;
            else if (ultimateSkillCheckTime < ultimateSkillCooldown) return ultimateSkillCheckTime / ultimateSkillCooldown;
            else return 1;
        }
    }

    public bool isCool_normal { get; protected set; } = false; // 일반스킬이 쿨타임중인가
    public bool isCool_ultimate { get; protected set; } = false; // 궁극기가 쿨타임중인가
    #endregion

    #region 시야 관련
    [SerializeField] protected float viewAngle = 130f; // 시야각
    [SerializeField] protected float viewDistance = 10f; // 시야거리
    [SerializeField] protected LayerMask targetMask; // 몬스터 레이어
    #endregion

    protected override void Start()
    {
        base.Start();

        normalSkill = AddWeapon(normalWeaponName);
        ultimateSkill = AddWeapon(ultimateWeaponName);
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    protected override void Update()
    {
        if (stat.CurrentHp <= 0 && !isDead) Die(); // 사망

        View(); // 시야각 내의 가장 가까운적을 타겟으로 설정

        Move(inputVector); // 입력받은 값에 따라 움직임

        #region 쿨타임 체크
        // 노말 쿨타임 체크
        if (isCool_normal)
        {
            normalSkillCheckTime += Time.deltaTime;
            if (normalSkillCheckTime >= normalSkillCooldown)
            {
                normalSkillCheckTime = 0f;
                isCool_normal = false;
            }
        }
        // 궁극기 쿨타임 체크
        if (isCool_ultimate)
        {
            ultimateSkillCheckTime += Time.deltaTime;
            if (ultimateSkillCheckTime >= ultimateSkillCooldown)
            {
                ultimateSkillCheckTime = 0f;
                isCool_ultimate = false;
            }
        }
        #endregion
    }

    public virtual void Move(Vector3 inputDir)
    {
        anim.SetFloat("MoveSpeed", inputDir.magnitude);

        // �Է� ������ ������ �������� �ƴҶ�
        if (inputDir.magnitude > 0.1f && !isAttacking)
        {
            float dirX = inputDir.x;
            float dirZ = inputDir.y;
            Vector3 moveDir = new Vector3(dirX, 0, dirZ);

            transform.position += (moveDir * stat.MoveSpeed * Time.deltaTime) * (isRun ? stat.moveSpeedMultiflier : 1f);

            anim.SetBool("IsRun", isRun);

            transform.LookAt(transform.position + moveDir);
        }
    }

    public virtual void Attack(bool onAttack)
    {
        anim.SetBool("OnAttack", onAttack);
        isAttacking = onAttack;
    }

    public virtual void Skill(SkillType skillType) // 타입에 따른 스킬발동
    {

        switch (skillType)
        {
            case SkillType.Normal:
                if (isCool_normal != true)
                {
                    isCool_normal = true;
                    normalSkill.OnAttack(focusTarget, false);
                    anim.SetTrigger("OnSkill");
                }
                break;
            case SkillType.Ultimate:
                if(isCool_ultimate != true && focusTarget != null)
                {
                    isCool_ultimate = true;
                    ultimateSkill.OnAttack(focusTarget, false);
                    anim.SetTrigger("OnSkill");
                }
                break;
        }
    }
    
    public override float GetDamage(float damage, MovableBase from) 
    {
        stat.CurrentHp -= damage;

        StartCoroutine(GetDamageMeshs());

        return damage;
    }

    protected void Die() // 체력이 0이 되면 사망
    {
        isDead = true;
        anim.SetTrigger("OnDead");
    }

    // 가장 가까운 적 타겟으로
    protected void View()
    {
        float nearesttargetDist = viewDistance + 1;
        int nearesttargetIdx = -1;

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            Vector3 _direction = (_targetTf.position - transform.position).normalized;
            // 시야각을 통해 검사
            float _angle = Vector3.Angle(_direction, transform.forward);
            if (_angle < viewAngle * 0.5f)
            {

                float dist = (_targetTf.position - transform.position).magnitude;
                if (dist < nearesttargetDist)
                {
                    nearesttargetDist = dist;
                    nearesttargetIdx = i;
                }
            }
        }

        if (nearesttargetIdx > -1)
            focusTarget = _target[nearesttargetIdx].GetComponent<MovableBase>();
        else
            focusTarget = null;

    }

    public WeaponBase AddWeapon(string wantName)
    {
        switch (wantName)
        {
            case "FloatingSword":
                return new Weapon_FloatingSword(this, normalSkillCooldown);
            case "SwordStorm":
                return new Weapon_SwordStorm(this, ultimateSkillCooldown);            
        }

        return null;
    }

    public virtual void ActiveAttackCol()
    {
        atkCollider.enabled = true;
    }

    public virtual void InActiveAttackCol()
    {
        atkCollider.enabled = false;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public IEnumerator GetDamageMeshs()
    {
        foreach (SkinnedMeshRenderer mesh in meshs) { mesh.material.color = Color.red; }
        yield return new WaitForSeconds(0.25f);

        foreach (SkinnedMeshRenderer mesh in meshs) { mesh.material.color = Color.white; }
        yield break;
    }
}
