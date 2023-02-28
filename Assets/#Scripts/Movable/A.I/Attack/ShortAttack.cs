using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAttack : MonoBehaviour
{
    AIBase owner;
    Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        if (owner == null)
        {
            // ������ ã�� �� ���ο��� �ݶ��̴� ������ �Ҵ�
            owner = GetComponentInParent<AIBase>();
            owner.atkCollider = collider;
            collider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� ������ ��� ���� �ϱ� ���� �Լ��� ������.
        if (other.gameObject == owner.focusTarget.gameObject)
        {
            Activate(owner.focusTarget);
        }
        else
        {
            Activate(other.GetComponent<MovableBase>());
        }
    }

    void Activate(MovableBase wantTarget)
    {
        if (wantTarget != null && wantTarget.isAlly != owner.isAlly)
        {
            wantTarget.GetDamage(owner.Stat.AttackPower, owner);
        }
    }
}
