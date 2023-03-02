using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public PlayeableCharacterBase player;


    // 인게임에서 사용될 이펙트들
    public static GameObject swordEffect;
    public static GameObject monsterAtkEffect;
    public static GameObject findEffect;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        swordEffect = Resources.Load<GameObject>("Prefabs/Effects/SwordImpact");
        monsterAtkEffect = Resources.Load<GameObject>("Prefabs/Effects/MonsterAtkEffect");
        findEffect = Resources.Load<GameObject>("Prefabs/Effects/FindEffect");
    }
 
    void Update()
    {

        
    }
}
