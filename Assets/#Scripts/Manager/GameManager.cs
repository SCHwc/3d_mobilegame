using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public PlayeableCharacterBase player;

    public static Stack<ClosableUI> windowList = new Stack<ClosableUI>();

    // �ΰ��ӿ��� ���� ����Ʈ��
    public static GameObject swordEffect;
    public static GameObject monsterAtkEffect;
    public static GameObject findEffect;
    public CameraShake cameraShaker;

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
        cameraShaker = Camera.main.GetComponent<CameraShake>();
    }
 
    public static void AddWindow()
    {

    }
    public static void RemoveWindow()
    {

    }
}
