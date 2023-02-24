using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Destroy : ProjectileAction
{
    // 
    public int pierce;

    public GameObject prefab;

    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        pierce--;
        if (pierce < 0 || proj.leftTime <= 0)
        {
            ProjectileBase obj = Instantiate(prefab.GetComponent<ProjectileBase>());
            obj.transform.position = position;
            Destroy(proj.gameObject);
        }
    }
}
