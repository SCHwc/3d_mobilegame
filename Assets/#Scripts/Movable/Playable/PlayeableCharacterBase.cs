using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeableCharacterBase : MovableBase
{
    public bool isRun = false; // 달리기 상태인지
    protected bool isAttacking = false; // 평타중인지

    float moveAttackSpeed = 0.5f;

    public Vector3 inputVector; // 입력벡터

    [SerializeField] protected Collider weaponCol;        // 무기콜라이더
    
    #region 스킬 이름 & WeaponBase
    [SerializeField] protected string normalWeaponName; // 노말스킬 이름
    WeaponBase normalSkill; // 스킬 스크립트
    [SerializeField] protected string ultimateWeaponName; // 궁극기 이름
    WeaponBase ultimateSkill; // 궁극기 스크립트
    #endregion

    #region 스킬쿨타임
    [SerializeField] protected float normalSkillCooldown; //노말스킬 쿨타임
    [SerializeField] protected float ultimateSkillCooldown; //노말스킬 쿨타임
    protected float normalSkillCheckTime = 0;
    protected float ultimateSkillCheckTime = 0;

    public float normalCoolDownRate // 남은 일반스킬 쿨타임 비율 0 ~ 1
    {
        get
        {
            if (normalSkillCheckTime == 0) return 0;
            else if (normalSkillCheckTime < normalSkillCooldown) return normalSkillCheckTime / normalSkillCooldown;
            else return 1;
        }
    }
    public float ultimateCoolDownRate // 남은 궁극기 쿨타임 비율 0 ~ 1
    {
        get
        {
            if (ultimateSkillCheckTime == 0) return 0;
            else if (ultimateSkillCheckTime < ultimateSkillCooldown) return ultimateSkillCheckTime / ultimateSkillCooldown;
            else return 1;
        }
    }

    public bool isCool_normal { get; protected set; } = false; // 일반스킬 쿨타임 중인지
    public bool isCool_ultimate { get; protected set; } = false; // 궁극기 쿨타임 중인지
    #endregion

    #region 시야 관련
    [SerializeField] protected float viewAngle = 130f; // 시야 각도
    [SerializeField] protected float viewDistance = 10f; // 시야 거리
    [SerializeField] protected LayerMask targetMask; // 타겟 마스크
    #endregion

    protected override void Start()
    {
        base.Start();

        normalSkill = AddWeapon(normalWeaponName);
        ultimateSkill = AddWeapon(ultimateWeaponName);
    }

    protected void Update()
    {
        View(); // 시야각을 통한 타겟 체크

        Move(inputVector); // 이동

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

        AttackMove(inputVector); // 공격시 앞으로 조금씩 이동
    }

    public virtual void Move(Vector3 inputDir)
    {
        anim.SetFloat("MoveSpeed", inputDir.magnitude);

        // 입력 방향이 있으며 공격중이 아닐때
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

    // 공격시 움직이는 메서드
    public virtual void AttackMove(Vector3 inputVector)
    {
        // 공격시 앞으로 조금 이동
        if (isAttacking)
        {
            if (inputVector.magnitude > 0.1f) // 입력방향있으면 그방향으로
            {
                float dirX = inputVector.x;
                float dirZ = inputVector.y;
                Vector3 moveDir = new Vector3(dirX, 0, dirZ).normalized;

                transform.position += (moveDir * Time.deltaTime * moveAttackSpeed);

                transform.LookAt(transform.position + moveDir);
            }
            else if (focusTarget != null) // 입력 안하고 타겟 존재시 타겟쪽으로 움직이며 공격
            {
                Vector3 moveDir = focusTarget.transform.position - transform.position;
                moveDir.y = 0f;
                moveDir = moveDir.normalized;

                transform.position += moveDir * Time.deltaTime * moveAttackSpeed;

                transform.LookAt(transform.position + moveDir);
            }
            else // 타겟없고 입력없으면 자기의 앞방향으로
            {
                transform.position += transform.forward.normalized * Time.deltaTime * moveAttackSpeed;
            }
        }
    }

    public virtual void Attack(bool onAttack)
    {
        anim.SetBool("OnAttack", onAttack);
        isAttacking = onAttack;
    }

    public virtual void Skill(SkillType skillType) // 스킬발동
    {

        switch (skillType)
        {
            case SkillType.Normal:
                if(isCool_normal != true)
                {
                    isCool_normal = true;
                    normalSkill.OnAttack(this, false);
                    anim.SetTrigger("OnSkill");
                }
                break;
            case SkillType.Ultimate:
                if(isCool_ultimate != true)
                {
                    isCool_ultimate = true;
                    ultimateSkill.OnAttack(focusTarget, false);
                    anim.SetTrigger("OnSkill");
                }
                break;
        }
    }

    public virtual void ActiveAttackCol()
    {
        weaponCol.enabled = true;
    }

    public virtual void InActiveAttackCol()
    {
        weaponCol.enabled = false;
    }
    
    public override float GetDamage(float damage, MovableBase from) 
    {
        stat.CurrentHp -= damage;

        anim.SetTrigger("Hit");

        return damage;
    }

    // 시야각 + 시야 범위 내의 몬스터중 가장 가까운 녀석을 타겟으로 가져가도록하는 메서드
    protected void View()
    {
        float nearesttargetDist = viewDistance + 1;
        int nearesttargetIdx = -1;

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for(int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            Vector3 _direction = (_targetTf.position - transform.position).normalized;
            // 시야각
            float _angle = Vector3.Angle(_direction, transform.forward);
            if(_angle < viewAngle * 0.5f)
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
                return new Weapon_FloatingSword(this);
            case "BlackHole":
                return new Weapon_BlackHole(this);
        }

        return null;
    }
}
