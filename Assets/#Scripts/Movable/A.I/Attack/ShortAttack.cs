using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAttack : MonoBehaviour
{
    MovableBase owner;
    Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        if (owner == null)
        {
            // ������ ã�� �� ���ο��� �ݶ��̴� ������ �Ҵ�
            owner = GetComponentInParent<MovableBase>();
            owner.atkCollider = collider;
            collider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ����ó��.ĳ���Ͱ� �ƴ϶�� return
        if (other.GetComponent<MovableBase>() == null) { return; }
        // ĳ���� �϶��� �浹 �̺�Ʈ �Լ� �����ϰ� ����
        else
        {
            Activate(other.GetComponent<MovableBase>());
        }
    }

    void Activate(MovableBase wantTarget)
    {
        if (wantTarget == null) { return; }

        if (wantTarget.isAlly != owner.isAlly)
        {
            GameObject effect = Instantiate(Managers.swordEffect, owner.atkCollider.transform);
            wantTarget.GetDamage(owner.Stat.AttackPower, owner);
        }
    }
}
