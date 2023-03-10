using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_FireShield : WeaponBase
{
    public Weapon_FireShield(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/FireShield");
    }

    public override void OnBuff()
    {
        if (GameManager.Instance.player.gameObject.activeSelf == true)
        {
            BuffShot(GameManager.Instance.player, true);
        }

        for (int i = 0; i < BattleSceneManager.Instance.partners.Length; i++)
        {
            if (BattleSceneManager.Instance.partners[i].gameObject.activeSelf == true)
            {
                BuffShot((BattleSceneManager.Instance.partners[i]), true);
            }
        }
    }
}
