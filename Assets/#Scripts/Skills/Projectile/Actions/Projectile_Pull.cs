using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projcetile_Pull : ProjectileAction
{
    [SerializeField] float pullingPower = 200f;

    MovableBase _target;

    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        // ²ø¾î´ç±æ ¹æÇâ ¼³Á¤((ºí·¢È¦ - Å¸°Ù).normalized)
        Vector3 dir = (proj.gameObject.transform.position - target.gameObject.transform.position).normalized;
        // Å¸°Ù rigidBody °¡Á®¿À±â
        Rigidbody otherRigid = target.gameObject.GetComponent<Rigidbody>();

        // ¹æÇâ * Èû ¸¸Å­ ²ø¾î´ç±â±â
        otherRigid.AddForce(dir * pullingPower);
    }
}
