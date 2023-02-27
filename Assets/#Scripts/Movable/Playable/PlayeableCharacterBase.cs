using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeableCharacterBase : MovableBase
{
    private void Update()
    {
    }

    public override float GetDamage(float damage, MovableBase from)
    {
        throw new System.NotImplementedException();
    }

}
