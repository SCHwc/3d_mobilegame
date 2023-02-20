public enum PartnerState // AI동료 캐릭터의 상태들
{
    Idle,
    Walk,
    Attack,
    Skill,
    Die,

    Count
}
public enum MonsterState // 몬스터의 상태들
{
    Idle,
    Walk,
    Attack,
    Skill,
    Die,

    Count
}

public enum SceneType // Scene 종류
{
    Unknown,    // 디폴트

    Lobby,      // 로비
    Inventory,  // 인벤
    Stage,      // 스테이지

    Count      // 개수
}