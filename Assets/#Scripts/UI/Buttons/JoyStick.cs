using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<<< HEAD:Assets/#Scripts/UI/Buttons/JoyStick.cs
public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
========
public class PlayerController : MonoBehaviour
>>>>>>>> re-paul:Assets/#Scripts/Controllers/PlayerController.cs
{
    PlayeableCharacterBase player { get => GameManager.Instance.player; } // �÷��̾�

    private void Update()
    {
        player.inputVector = Managers.Input.inputVector; // �Է°� �Ѱ��ֱ�
        player.isRun = Managers.Input.isRun;    // �޸��� ���� ������Ʈ
        player.Attack(Managers.Input.onAttack); // �����Է� ������ �ߵ�
    }

    public void Skill(SkillType type)
    {
        player.Skill(type);
    }
}
