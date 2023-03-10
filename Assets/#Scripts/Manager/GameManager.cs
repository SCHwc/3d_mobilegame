using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public PlayeableCharacterBase player;

    // 동료캐릭터 목록
    public static Dictionary<string, PartnerInfo> partnerList;
    // 부대 편성을 진행할 때 사용할 배열
    public PartnerInfo[] party = new PartnerInfo[3];
    public PartnerInfo currentInfo;

    // 인게임에 사용할 이펙트
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

        Initialize();

        // 임시 할당 삭제 필요!!!
        party[0] = partnerList["RockShield"];
        party[1] = partnerList["I-Sword"];
        party[2] = partnerList["I-Sword"];
    }

    void Initialize()
    {

        // 이펙트 초기할당
        swordEffect = Resources.Load<GameObject>("Prefabs/Effects/SwordImpact");
        monsterAtkEffect = Resources.Load<GameObject>("Prefabs/Effects/MonsterAtkEffect");
        findEffect = Resources.Load<GameObject>("Prefabs/Effects/FindEffect");

        cameraShaker = Camera.main.GetComponent<CameraShake>();

        if (partnerList == null)
        {
            partnerList = new Dictionary<string, PartnerInfo>();

            partnerList.Add
                (
                "RockShield",
                new PartnerInfo
                (
                Resources.Load<GameObject>("Prefabs/Partners/Partner_RockShield"),
                Resources.Load<Sprite>("Prefabs/Icon/RockShield"),
                "락쉴드",
                PartnerType.Defense,
                AttackType.Short,
                "도발 (적들의 표적을 자신으로 돌린다)"
                )
                );
            partnerList.Add
                (
                "I-Sword",
                new PartnerInfo
                (
                Resources.Load<GameObject>("Prefabs/Partners/Partner_I-Sword"),
                Resources.Load<Sprite>("Prefabs/Icon/I-Sword"),
                "아이소드",
                PartnerType.Attack,
                AttackType.Long,
                "빙하소환 (적에게 공속저하 디버프와 높은 공격력을 가짐)"
                )
                );
            partnerList.Add
                (
                "Firework",
                new PartnerInfo
                (
                Resources.Load<GameObject>("Prefabs/Partners/Partner_Firework"),
                Resources.Load<Sprite>("Prefabs/Icon/Firework"),
                "파이어워크",
                PartnerType.Attack,
                AttackType.Long,
                "불장막 (동료들에게 피해감소 버프를 부여)"
                )
                );
            partnerList.Add
                (
                "Healer",
                new PartnerInfo
                (
                Resources.Load<GameObject>("Prefabs/Partners/Partner_Healer"),
                Resources.Load<Sprite>("Prefabs/Icon/Healer"),
                "힐러",
                PartnerType.Support,
                AttackType.Long,
                "힐링 (동료들의 체력을 회복)"
                )
                );
        }
    }
 }

public class PartnerInfo
{
    public GameObject prefab;
    public Sprite icon;
    public string name;
    public PartnerType type;
    public AttackType attackType;
    public string context;

    public PartnerInfo(GameObject wantPrefab, Sprite wantIcon, string wantName, PartnerType wantType, AttackType wantAttackType, string wantContext)
    {
        prefab = wantPrefab;
        icon = wantIcon;
        name = wantName;
        type = wantType;
        attackType = wantAttackType;
        context = wantContext;
    }

    public string GetContext()
    {
        string result = $"이름 : {name.TextSetting(Color.white)}\n";
        result += $"역할 : {type.PartnerTypeToText()}\n";
        result += $"공격타입 : {attackType.AttackTypeToText()}\n";
        result += $"스킬 : {context}";

        return result;
    }
}