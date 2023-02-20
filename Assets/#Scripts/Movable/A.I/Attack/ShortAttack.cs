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
            owner = GetComponentInParent<AIBase>();
            owner.atkCollider = collider;
            collider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<AIBase>().GetDamage(owner.Stat.AttackPower, owner);
    }
}
