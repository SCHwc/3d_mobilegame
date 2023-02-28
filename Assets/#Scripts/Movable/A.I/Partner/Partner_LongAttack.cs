using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Partner_LongAttack : PartnerBase
{
    // ĳ���Ͱ� ������ �ִ� ��ų
    WeaponBase currentWeapon; 

    // ���ϴ� ��ų�� �̸�
    public string weaponName;

    // Ÿ���� ����, true ��� �߻�ü�� ���� ��� �����Ѵ�.
    public bool isTracking;

    protected override void Start()
    {
        base.Start();

        // ��ų �Ҵ�
        AddWeapon(weaponName);

    }
    protected override void Update()
    {
        base.Update();
    }


    public override void Anim_Attack()
    {
        if (focusTarget == null) { return; }
        if (atkType == AttackType.Long)
        {
            // �ش� ��ų�� ������ �ִ� ���� ����
            currentWeapon.OnAttack(focusTarget.GetComponent<MonsterBase>(), isTracking);
        }
    }

    public override void AddWeapon(string wantName)
    {
        switch (wantName)
        {
            case "IceMissile":
                currentWeapon = new Weapon_IceMissile(this);
                break;
            case "Fireball":
                currentWeapon = new Weapon_Fireball(this);
                break;
        }
    }
}
