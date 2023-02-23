using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayeableCharacterBase player { get => GameManager.Instance.player; } // 플레이어

    private void Update()
    {
        player.isRun = Managers.Input.isRun;
        player.Move(Managers.Input.inputVector);
        player.Attack(Managers.Input.onAttack);
    }

    public void Skill(SkillType type)
    {
        player.Skill(type);
    }
}
