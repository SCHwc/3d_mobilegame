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
            // 주인을 찾고 그 주인에게 콜라이더 정보를 할당
            owner = GetComponentInParent<AIBase>();
            owner.atkCollider = collider;
            collider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 주인이 공격 시 콜라이더가 켜지고, 충돌감지는 여기서
        other.GetComponent<MovableBase>().GetDamage(owner.Stat.AttackPower, owner);
    }
}
