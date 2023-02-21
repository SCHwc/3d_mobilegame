using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // �߻�ü�� ����
    public MovableBase owner { get; protected set; }

    // �߻�ü�� �� �ϵ�
    protected ProjectileAction[] actions;

    // �浹�� ������ ����Ʈ
    protected List<GameObject> ignoreList = new List<GameObject>();

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

    public virtual void Activate(GameObject other)
    {
        // �浹���� ������Ʈ��� return
        if (ignoreList.Contains(other)) { return; }

        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterBase monster= other.GetComponent<MonsterBase>();
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
