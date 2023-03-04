using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // �߻�ü�� ����
    public MovableBase owner { get; protected set; }

    // �߻�ü�� ��ǥ��
    public MovableBase focusTarget;

    // �߻�ü�� �� �ϵ�
    protected ProjectileAction[] actions;

    // �浹�� ������ ����Ʈ
    protected List<GameObject> ignoreList = new List<GameObject>();

    //������ ����
    protected Vector3 _direction;
    public Vector3 Direction
    {
        get => _direction;
        set
        {
            _direction = value.normalized;
        }
    }

    // �߻�ü�� Ư�� ��ġ
    #region
    [Tooltip("�߻�ü�� �ӵ�")]
    public float currentSpeed;
    //[Tooltip("�߻�ü�� ���ӵ� - �ð��� ���� ���������� ��ȭ�ϴ� �ӵ���")]
    //public float acceleration;
    //[Tooltip("�߻�ü�� ���ӵ� - �ð��� ������ ȸ��")]
    //public float angularSpeed;
    [Tooltip("�� ��ü�� ��Ƴ��� �ð�")]
    public float leftTime;
    [Tooltip("�߻�ü�� �� �������׵� �´°�?")]
    public bool contactSelf;
    [Tooltip("�߻�ü�� ��ǥ���� ��� ���󰡴°�?")]
    public bool isTracking;
    [Tooltip("�������ΰ�?")]
    public bool isRangeAttack;
    #endregion

    void Start()
    {
        foreach (Collider current in GetComponentsInChildren<Collider>())
        {
            // �ݶ��̴��� �ִ� ������Ʈ���� �浹���� ����� �Ҵ��Ų��.
            current.gameObject.AddComponent<ProjectileCollider>();
        }

        // �߻�ü�� �ൿ���� ��Ƽ� �Ҵ���ѵα�
        actions = GetComponents<ProjectileAction>();
    }

    void Update()
    {
        if (leftTime <= 0 || focusTarget == null) { Destroy(gameObject); }
        leftTime -= Time.deltaTime; // �߻�ü�� �����ð�

        // �ð��� �������� ����
        // currentSpeed += acceleration * Time.deltaTime;

        if (isTracking)
        {  // ��ǥ���� �����ϵ��� �������ִٸ� ��ǥ���� �ٶ󺸰� ����� ��� ����
            if (focusTarget)
            {
                Vector3 targetPosition = focusTarget.transform.position;
                targetPosition.y = 1;
                transform.LookAt(targetPosition);
                transform.position += transform.forward * currentSpeed * Time.deltaTime;
            }
        }
        else
        {
            // �ٶ󺸴� ����
            if (currentSpeed > 0)
            {
                Vector3 lookPosition = new Vector3(Direction.x, 0, Direction.z).normalized;
                transform.LookAt(transform.position + lookPosition);
            }
            else
            {
            }

            // �������� �ʴ´ٸ� �ʱ�ȭ �޼��忡�� �Ҵ���� �������� ����
            Vector3 movePosition = Direction * currentSpeed * Time.deltaTime;
            transform.position += movePosition;
        }
    }

    public void Initialize(MovableBase wantOnwer, MovableBase wantTarget, bool wantTracking)
    {
        // 소환되면서 초기화
        owner = wantOnwer;
        isTracking = wantTracking;
        if (!contactSelf && owner != null) { SetIgnore(owner.gameObject); }

        focusTarget = wantTarget;
        Direction = focusTarget.transform.position - owner.transform.position;
    }

    public virtual void Activate(MovableBase other)
    {
        // 예외처리1.충돌을 무시하기로한 물체라면 return
        if (ignoreList.Contains(other.gameObject)) { return; }
        // 예외처리2.같은 진영이라면 return
        if (owner.isAlly == other.isAlly) { return; }

        if (isTracking) // 현재 스킬이 추적 스킬이라면
        {
            // 타겟이 아니면 return
            if (owner.focusTarget != other) { return; }
            // 타겟이면 타겟 대상으로 액션 실행
            else { ActionActivate(this, focusTarget, transform.position); }
        }
        else
        {
            if (owner.isAlly != other.isAlly)
            {
                ActionActivate(this, other, transform.position);
            }
        }
    }

    void ActionActivate(ProjectileBase wantProj, MovableBase wantTarget, Vector3 wantPosition)
    {
        foreach (ProjectileAction current in actions) { current?.Activate(wantProj, wantTarget, wantPosition); }
    }

    public void OnTriggerEnter(Collider other)
    {
        // 예외처리.캐릭터가 아니라면 return
        if (other.GetComponent<MovableBase>() == null) { return; }
        // 캐릭터 일때만 충돌 이벤트 함수 실행하게 설정
        else
        {
            Activate(other.GetComponent<MovableBase>());
        }
    }

    public virtual void SetIgnore(GameObject target)
    {
        ignoreList.Add(target); //����� �浹�����Ѵ�.(Layer�� Ȱ���ؼ� �浹�� ������ �� �Լ� ���ʿ�)
    }
}
