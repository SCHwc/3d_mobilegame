using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // ������ ������
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            Debug.Log("MeleeAttack Script");
            //MonsterBase monster = other.GetComponent<MonsterBase>();
            //monster.GetDamage(stat.AttackPower, this);
        }
    }

}
