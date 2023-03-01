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
            // 주인을 찾고 그 주인에게 콜라이더 정보를 할당
            owner = GetComponentInParent<MovableBase>();
            owner.atkCollider = collider;
            collider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 예외처리.캐릭터가 아니라면 return
        if (other.GetComponent<MovableBase>() == null) { return; }
        // 캐릭터 일때만 충돌 이벤트 함수 실행하게 설정
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
