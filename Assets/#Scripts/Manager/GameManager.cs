using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public PlayeableCharacterBase player;

    // 부대 편성을 진행할 때 사용할 배열
    public MovableBase[] party = new MovableBase[3];

    // QTE 를 위한 스킬버튼 배열
    public GameObject[] skillBtns = new GameObject[3];

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
 
    // QTE발동 메서드 -> 스킬버튼 누르면Invoke
    public void OnQTE(int currentType)
    {
        skillBtns[currentType].SetActive(true);
    }
}
