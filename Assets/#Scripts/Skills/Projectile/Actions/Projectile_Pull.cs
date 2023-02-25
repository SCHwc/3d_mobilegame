using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Pull : ProjectileAction
{
    [SerializeField] float pullingPower = 200f;

    MovableBase _target;

    

    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        // ������ ���� ����((��Ȧ - Ÿ��).normalized)
        Vector3 dir = (proj.gameObject.transform.position - target.gameObject.transform.position).normalized;
        // Ÿ�� rigidBody ��������
        Rigidbody otherRigid = target.gameObject.GetComponent<Rigidbody>();

        Debug.Log(otherRigid.gameObject.name);

        // ���� * �� ��ŭ �������
        otherRigid.AddForce(dir * pullingPower);
    }
}
