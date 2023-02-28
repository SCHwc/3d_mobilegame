using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<<< HEAD:Assets/#Scripts/UI/Buttons/JoyStick.cs
public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
========
public class PlayerController : MonoBehaviour
>>>>>>>> re-paul:Assets/#Scripts/Controllers/PlayerController.cs
{
    PlayeableCharacterBase player { get => GameManager.Instance.player; } // 플레이어

    private void Update()
    {
        player.inputVector = Managers.Input.inputVector; // 입력값 넘겨주기
        player.isRun = Managers.Input.isRun;    // 달리기 상태 업데이트
        player.Attack(Managers.Input.onAttack); // 공격입력 받으면 발동
    }

    public void Skill(SkillType type)
    {
        player.Skill(type);
    }
}
