using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // �߻�ü�� ����
    public MovableBase owner { get; protected set; }

    // �߻�ü�� ��ǥ��
    public MovableBase focustarget;

    // �߻�ü�� �� �ϵ�
    [SerializeField]protected ProjectileAction[] actions;

    // �浹�� ������ ����Ʈ
    protected List<GameObject> ignoreList = new List<GameObject>();

    // ������ ����
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
        if (leftTime <= 0) { Destroy(gameObject); }
        leftTime -= Time.deltaTime; // �߻�ü�� �����ð�

        // �ð��� �������� ����
        // currentSpeed += acceleration * Time.deltaTime;

        if (isTracking)
        {  // ��ǥ���� �����ϵ��� �������ִٸ� ��ǥ���� �ٶ󺸰� ����� ��� ����
            Vector3 targetPosition = focustarget.transform.position;
            targetPosition.y = 1;
            transform.LookAt(targetPosition);
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }
        else
        {   // �ƴ϶�� �ʱ�ȭ �޼��忡�� �Ҵ���� �������� ����
            transform.position += Direction * currentSpeed * Time.deltaTime;
        }
    }

    public void Initialize(MovableBase wantOnwer, MovableBase wanttarget, bool wantTracking)
    {   // �߻�ü �ʱ�ȭ �Ҵ�
        owner = wantOnwer;
        focustarget = wanttarget;
        isTracking = wantTracking;

        Direction = focustarget.transform.position;
    }

    public virtual void Activate(GameObject other)
    {
        // �浹���� ������Ʈ��� return
        if (ignoreList.Contains(other)) { return; }


        // ���͸� �����ϱ� ���� ����
        if (owner.focusTarget.gameObject == other && other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            if (monster != null)
            {    // �� ��ų�� �޷��ִ� �̺�Ʈ���� ��� �����Ų��.
                foreach (ProjectileAction current in actions) { current?.Activate(this, monster, transform.position); }
            }
        }

        // �÷��̾� ������ �����ϱ� ���� ����
        if (owner.focusTarget.gameObject == other && (other.gameObject.layer == LayerMask.NameToLayer("Player")
            || other.gameObject.layer == LayerMask.NameToLayer("Partner")))
        {
            MovableBase target = other.GetComponent<MovableBase>();
            if (target != null)
            {   // �� ��ų�� �޷��ִ� �̺�Ʈ���� ��� �����Ų��.
                foreach (ProjectileAction current in actions) { current?.Activate(this, target, transform.position); }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // �ݶ��̴����� �浹�� �����ϸ� �浹�� ��ü�� ������ ���⼭ �޾ƿ� > Activate �Լ��� ���� �����Ŀ� �浹 �̺�Ʈ ����
        Activate(other.gameObject);
    }

    public virtual void SetIgnore(GameObject target)
    {
        ignoreList.Add(target); //����� �浹�����Ѵ�.(Layer�� Ȱ���ؼ� �浹�� ������ �� �Լ� ���ʿ�)
    }

}
