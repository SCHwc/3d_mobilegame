using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    // AI의 상태
    public enum State
    {
        Default,
        Move,
        Attack,

    }

    // Scene 종류
    public enum SceneType
    {
        Unknown, // 디폴트

        Lobby,      // 로비
        Inventory,  // 인벤
        Stage,      // 스테이지

        Length  // 개수
    }
}
