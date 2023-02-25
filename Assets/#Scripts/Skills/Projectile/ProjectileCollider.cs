using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    // 발사체의 정보
    ProjectileBase from = null;

    private void Start()
    {
        // 부모 오브젝트에 있는 정보 할당해주기
        from = GetComponentInParent<ProjectileBase>();
    }

    public void OnTriggerEnter(Collider collision) { from?.OnTriggerEnter(collision); }
}

