using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeableCharacterBase : MovableBase
{
    public bool isRun = false; // �޸��� ��������

    public GameObject normalSkillPrefab; // �Ϲݽ�ų ������Ʈ
    public GameObject ultimateSkillPrefab; // �ñر�

    [SerializeField] protected Collider weaponCol;        // �����ݶ��̴�

    #region ��ų����
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
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            GetDamage(1, this);

        View();

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
    }

    public virtual void Move(Vector3 inputDir)
    {
        anim.SetFloat("MoveSpeed", inputDir.magnitude);

        if (inputDir.magnitude > 0.1f)
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
    }

    public virtual void Skill(SkillType skillType) // ��ų�ߵ�
    {

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

    // �þ߰� + �þ� ���� ���� ������ ���� ����� �༮�� Ÿ������ �����������ϴ� �޼���
    protected void View()
    {
        float nearestTargetDist = viewDistance + 1;
        int nearestTargetIdx = -1;

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for(int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            Vector3 _direction = (_targetTf.position - transform.position).normalized;
            float _angle = Vector3.Angle(_direction, transform.forward);
            if(_angle < viewAngle * 0.5f)
            {
                float dist = (_targetTf.position - transform.position).magnitude;
                if (dist < nearestTargetDist)
                {
                    nearestTargetDist = dist;
                    nearestTargetIdx = i;
                }
            }
        }

        if (nearestTargetIdx > -1)
            target = _target[nearestTargetIdx].GetComponent<MovableBase>();
        else
            target = null;

    }

    public override float GetHeal() { return 0; }
    public override float Attack() { return 0; }
    public override void Move() { }
}
