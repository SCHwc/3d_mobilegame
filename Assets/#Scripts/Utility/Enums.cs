using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    // AI�� ����
    public enum State
    {
        Default,
        Move,
        Attack,

    }

    // Scene ����
    public enum SceneType
    {
        Unknown, // ����Ʈ

        Lobby,      // �κ�
        Inventory,  // �κ�
        Stage,      // ��������

        Length  // ����
    }
}
