using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers _instance = null;
    public static Managers Instance { get => _instance; }

    #region ���� �Ŵ��� ���� �ʵ� �� ������Ƽ
    private static InputManager _input = new InputManager();

    // Managers�ʱ�ȭ �� ����� ��� ����Ͽ� get�� �ʱ�ȭ ���� �� ��ȯ
    public static InputManager Input { get { Init(); return _input; } }
    #endregion

    public static GameObject swordEffect;

    //public static Dictionary<string, WeaponInfo> weaponInfos = null;

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        // �̱���
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject("@Managers");

            // ���ӿ�����Ʈ�� Managers�� �ִٸ� �������� ���ٸ� �ٿ��� ��ȯ
            _instance = Utils.GetOrAddComponent<Managers>(go);

            // �� ��ȯ�ÿ��� �ı����� �ʵ��� ó��
            DontDestroyOnLoad(go);

            #region �����Ŵ����� �ʱ�ȭ
            _input.Init();
            #endregion

            // ���� ����� ���� ������ ����
            Application.targetFrameRate = 60;

        }

        swordEffect = Resources.Load<GameObject>("Prefabs/Effects/SwordImpact");

        //if (weaponInfos == null)
        //{
        //    weaponInfos = new Dictionary<string, WeaponInfo>();
        //    weaponInfos.Add
        //        (
        //        "IceMissile",
        //        new WeaponInfo
        //        (
        //            typeof(Weapon_IceMissile),
        //            Resources.Load<GameObject>("Prefabs/Projectiles/IceMissile"),
        //            null,
        //            "IceMissile"
        //            )
        //        );
        //}

    }
}

//public class WeaponInfo
//{
//    public System.Type weapon;
//    public GameObject spawnPrefab;
//    public Sprite icon;
//    public string name;

//    public WeaponInfo(System.Type wantWeapon, GameObject wantPrefab, Sprite wantIcon, string wantName)
//    {
//        weapon = wantWeapon;
//        spawnPrefab = wantPrefab;
//        icon = wantIcon;
//        name = wantName;
//    }
//}

