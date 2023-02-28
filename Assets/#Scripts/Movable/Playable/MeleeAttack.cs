using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    MovableBase owner; // ����
    [SerializeField] protected ParticleSystem attackEffect; // ���� ����Ʈ

    void Start()
    {
        owner = GetComponentInParent<MovableBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            MonsterBase target = other.GetComponent<MonsterBase>();
            target.GetDamage(owner.Stat.AttackPower, owner);
            attackEffect.Play();
        }
    }
}
