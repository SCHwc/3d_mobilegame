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
            Vector3 targetPosition = focusTarget.transform.position;
            targetPosition.y = 1;
            transform.LookAt(targetPosition);
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Direction * currentSpeed * Time.deltaTime;
        }
    }

    public void Initialize(MovableBase wantOnwer, MovableBase wantTarget, bool wantTracking)
    {   // �߻�ü �ʱ�ȭ �Ҵ�
        owner = wantOnwer;
        focusTarget = wantTarget;
        isTracking = wantTracking;

        Direction = focusTarget.transform.position;
    }

    public virtual void Activate(GameObject other)
    {
        // �浹���� ������Ʈ��� return
        if (ignoreList.Contains(other)) { return; }

        if (owner.focusTarget.gameObject == other && other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            if (monster != null)
            {
                foreach (ProjectileAction current in actions) { current?.Activate(this, monster, transform.position); }
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        Activate(other.gameObject);
    }

    public virtual void SetIgnore(GameObject target)
    {
        ignoreList.Add(target); //����� �����ϵ��� �մϴ�!
    }
}
