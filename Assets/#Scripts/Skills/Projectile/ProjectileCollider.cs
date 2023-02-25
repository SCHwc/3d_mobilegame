using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    // 발사체의 정보
    ProjectileBase from = null; 

    void Start()
    {
        if (from == null)
        {       // 부모 오브젝트에 있는 정보 할당해주기
                from = GetComponentInParent<ProjectileBase>();
        }
        
    }

    public void OnTriggerEnter(Collider collision) { from?.OnTriggerEnter(collision); }

    //public void OnCollisionEnter(Collision collision) { from?.OnCollisionEnter(collision); }
}
