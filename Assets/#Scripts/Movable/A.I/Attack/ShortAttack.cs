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
        // 충돌한 물체의 정보를 얻기 쉽게 하기 위해 함수로 돌린다.
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
