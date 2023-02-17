using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeableCharacterBase : MovableBase
{
    private void Update()
    {
        Move();
        
    }

    public override float Attack()
    {
        throw new System.NotImplementedException();
    }

    public override float GetDamage(float damage, MovableBase from)
    {
        throw new System.NotImplementedException();
    }

    public override float GetHeal()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        Vector3 moveDir;

        anim.SetFloat("MoveSpeed", Managers.Input.inputVector.magnitude);

        if (Managers.Input.inputVector.magnitude > 0.1f)
        {
            float dirX = Managers.Input.inputVector.x;
            float dirZ = Managers.Input.inputVector.y;
            moveDir = new Vector3(dirX, 0, dirZ);

            transform.position += (moveDir * stat.MoveSpeed * Time.deltaTime);
        }        
    }
}
