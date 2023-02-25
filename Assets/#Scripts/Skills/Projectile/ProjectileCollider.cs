using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    // �߻�ü�� ����
    ProjectileBase from = null;

    private void Start()
    {
        // �θ� ������Ʈ�� �ִ� ���� �Ҵ����ֱ�
        from = GetComponentInParent<ProjectileBase>();
    }

    public void OnTriggerEnter(Collider collision) { from?.OnTriggerEnter(collision); }
}

