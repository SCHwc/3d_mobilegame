using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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