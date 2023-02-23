using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // 데미지 입히기
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
