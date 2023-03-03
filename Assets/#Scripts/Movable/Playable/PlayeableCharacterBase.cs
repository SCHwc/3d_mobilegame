using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeableCharacterBase : MovableBase
{
    public bool isRun = false; // �޸��� ��������
    protected bool isAttacking = false; // ��Ÿ������

    float moveAttackSpeed = 0.5f;

    public Vector3 inputVector; // �Էº���

    #region ��ų �̸� & WeaponBase
    [SerializeField] protected string normalWeaponName; // �븻��ų �̸�
    WeaponBase normalSkill; // ��ų ��ũ��Ʈ
    [SerializeField] protected string ultimateWeaponName; // �ñر� �̸�
    WeaponBase ultimateSkill; // �ñر� ��ũ��Ʈ
    #endregion

    #region ��ų��Ÿ��
    [SerializeField] protected float normalSkillCooldown; //�븻��ų ��Ÿ��
    [SerializeField] protected float ultimateSkillCooldown; //�븻��ų ��Ÿ��
    protected float normalSkillCheckTime = 0;
    protected float ultimateSkillCheckTime = 0;

    public float normalCoolDownRate // ���� �Ϲݽ�ų ��Ÿ�� ���� 0 ~ 1
    {
        get
        {
            if (normalSkillCheckTime == 0) return 0;
            else if (normalSkillCheckTime < normalSkillCooldown) return normalSkillCheckTime / normalSkillCooldown;
            else return 1;
        }
    }
    public float ultimateCoolDownRate // ���� �ñر� ��Ÿ�� ���� 0 ~ 1
    {
        get
        {
            if (ultimateSkillCheckTime == 0) return 0;
            else if (ultimateSkillCheckTime < ultimateSkillCooldown) return ultimateSkillCheckTime / ultimateSkillCooldown;
            else return 1;
        }
    }

    public bool isCool_normal { get; protected set; } = false; // �Ϲݽ�ų ��Ÿ�� ������
    public bool isCool_ultimate { get; protected set; } = false; // �ñر� ��Ÿ�� ������
    #endregion

    #region �þ� ����
    [SerializeField] protected float viewAngle = 130f; // �þ� ����
    [SerializeField] protected float viewDistance = 10f; // �þ� �Ÿ�
    [SerializeField] protected LayerMask targetMask; // Ÿ�� ����ũ
    #endregion

    protected override void Start()
    {
        base.Start();

        normalSkill = AddWeapon(normalWeaponName);
        ultimateSkill = AddWeapon(ultimateWeaponName);
    }

    protected override void Update()
    {
        View(); // �þ߰��� ���� Ÿ�� üũ

        Move(inputVector); // �̵�

        #region ��Ÿ�� üũ
        // �븻 ��Ÿ�� üũ
        if (isCool_normal)
        {
            normalSkillCheckTime += Time.deltaTime;
            if (normalSkillCheckTime >= normalSkillCooldown)
            {
                normalSkillCheckTime = 0f;
                isCool_normal = false;
            }
        }
        // �ñر� ��Ÿ�� üũ
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

    // ���ݽ� �����̴� �޼���
    public virtual void AttackMove(Vector3 inputVector)
    {
        // ���ݽ� ������ ���� �̵�
        if (isAttacking)
        {
            if (inputVector.magnitude > 0.1f) // �Է¹��������� �׹�������
            {
                float dirX = inputVector.x;
                float dirZ = inputVector.y;
                Vector3 moveDir = new Vector3(dirX, 0, dirZ).normalized;

                transform.position += (moveDir * Time.deltaTime * moveAttackSpeed);

                transform.LookAt(transform.position + moveDir);
            }
            else if (focusTarget != null) // �Է� ���ϰ� Ÿ�� ����� Ÿ�������� �����̸� ����
            {
                Vector3 moveDir = focusTarget.transform.position - transform.position;
                moveDir.y = 0f;
                moveDir = moveDir.normalized;

                transform.position += moveDir * Time.deltaTime * moveAttackSpeed;

                transform.LookAt(transform.position + moveDir);
            }
            else // Ÿ�پ��� �Է¾����� �ڱ��� �չ�������
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

    public virtual void Skill(SkillType skillType) // ��ų�ߵ�
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
                if (isCool_ultimate != true)
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
        atkCollider.enabled = true;
    }

    public virtual void InActiveAttackCol()
    {
        atkCollider.enabled = false;
    }

    public override float GetDamage(float damage, MovableBase from)
    {
        stat.CurrentHp -= damage;

        anim.SetTrigger("Hit");

        return damage;
    }

    // �þ߰� + �þ� ���� ���� ������ ���� ����� �༮�� Ÿ������ �����������ϴ� �޼���
    protected void View()
    {
        float nearesttargetDist = viewDistance + 1;
        int nearesttargetIdx = -1;

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            Vector3 _direction = (_targetTf.position - transform.position).normalized;
            // �þ߰�
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
                return new Weapon_FloatingSword(this);
            case "BlackHole":
                return new Weapon_BlackHole(this);
        }

        return null;
    }
}
