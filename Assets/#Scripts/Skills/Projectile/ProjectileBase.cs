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

    //������ ���� ������ ����ȭ�� ����
    Vector3 _direction;
    //������ ���� ����� ����ȭ�� ����
    float _horizontalAngle;
    float _verticalAngle;

    public Vector3 Direction
    {
        get => _direction;
        set
        {
            //�����̱� ������ ũ��� 1�� ����Կ�!
            _direction = value.normalized;
            //_angle = _direction.ToAngle();
            _horizontalAngle = _direction.ToHorizontalAngle();
            _verticalAngle = _direction.ToVerticalAngle();
        }
    }
    public float HorizontalAngle
    {
        get => _horizontalAngle;
        set
        {
            _horizontalAngle = value;
            //_direction = value.ToDirection();
            _direction = new Vector2(_horizontalAngle, _verticalAngle).ToDirection();
        }
    }
    public float VerticalAngle
    {
        get => _verticalAngle;
        set
        {
            _verticalAngle = value;
            //_direction = value.ToDirection();
            _direction = new Vector2(_horizontalAngle, _verticalAngle).ToDirection();
        }
    }

    // �߻�ü�� Ư�� ��ġ
    #region
    [Tooltip("�߻�ü�� �ӵ�")]
    public float currentSpeed;
    [Tooltip("�߻�ü�� ���ӵ� - �ð��� ���� ���������� ��ȭ�ϴ� �ӵ���")]
    public float acceleration;
    [Tooltip("�߻�ü�� ���ӵ� - �ð��� ������ ȸ��")]
    public float angularSpeed;
    [Tooltip("�� ��ü�� ��Ƴ��� �ð�")]
    public float leftTime;
    [Tooltip("�߻�ü�� �� �������׵� �´°�?")]
    public bool contactSelf;
    [Tooltip("�߻�ü�� ��ǥ���� ��� ���󰡴°�?")]
    public bool isTracking;
    #endregion
    void Start()
    {
        // �߻�ü�� �ൿ���� ��Ƽ� �Ҵ���ѵα�
        actions = GetComponents<ProjectileAction>();
    }

    void Update()
    {
        if (leftTime <= 0) { Destroy(gameObject); }
        leftTime -= Time.deltaTime;

        // �ð��� �������� ����
        currentSpeed += acceleration * Time.deltaTime;
    }

    public void Initialize(MovableBase wantOnwer, MovableBase wantTarget, bool wantTracking)
    {
        owner = wantOnwer;
        focusTarget = wantTarget;
        isTracking = wantTracking;
    }

    public virtual void Activate(GameObject other)
    {
        // �浹���� ������Ʈ��� return
        if (ignoreList.Contains(other)) { return; }

        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            if (monster != null)
            {
                foreach (ProjectileAction current in actions) { current?.Activate(this, monster, transform.position); }
            }
        }

    }

    public virtual void SetIgnore(GameObject target)
    {
        ignoreList.Add(target); //����� �����ϵ��� �մϴ�!
    }
}
