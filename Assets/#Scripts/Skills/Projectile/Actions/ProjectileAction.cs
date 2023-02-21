using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAction : MonoBehaviour
{
    public virtual void Activate(ProjectileBase proj, MovableBase target, Vector3 position) { }
}
