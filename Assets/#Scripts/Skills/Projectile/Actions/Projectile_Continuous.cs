using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Continuous : ProjectileAction
{
    // 공격 딜레이
    [SerializeField] float attackDelay = 0.3f;
    float checkTime = 0f;
    // 끄고 킬 콜라이더
    Collider col;

    private void Start()
    {
        col = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        // 딜레이마다 콜라이더 키고 끄기
        checkTime += Time.deltaTime;
        if(checkTime >= attackDelay)
        {
            checkTime = 0f;
            StartCoroutine(ActiveCollier());
        }
    }

    // 콜라이더 키고 끄기
    IEnumerator ActiveCollier()
    {
        col.enabled = true;
        yield return new WaitForFixedUpdate();
        col.enabled = false;
    }
}
