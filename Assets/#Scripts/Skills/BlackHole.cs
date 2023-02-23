using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : ContinuousSkill
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            Vector3 dir = (transform.position - other.transform.position).normalized;
            Rigidbody otherRigid = other.GetComponent<Rigidbody>();
            otherRigid.AddForce(dir * 300f);
            MonsterBase monster = other.GetComponent<MonsterBase>();
            monster.GetDamage(owner.Stat.AttackPower + owner.Stat.AttackPower * damageMultiflierPercentage * 0.01f, owner);
        }
    }
}
