

public enum PartnerState // AI���� ĳ������ ���µ�
{
    Idle,
    Walk,
    Attack,
    Skill,
    Die,

    Count
}
public enum MonsterState // ������ ���µ�
{
    Idle,
    Walk,
    Attack,
    Skill,
    Die,

    Count
}

public enum AttackType
{
    Short,
    Long
}

public enum SceneType // Scene ����
{
    Unknown,    // ����Ʈ

    Lobby,      // �κ�
    Inventory,  // �κ�
    Stage,      // ��������

    Count      // ����
}