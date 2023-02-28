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
        // ������ ���� �� �ݶ��̴��� ������, �浹������ ���⼭
        other.GetComponent<MovableBase>().GetDamage(owner.Stat.AttackPower, owner);
    }
}
