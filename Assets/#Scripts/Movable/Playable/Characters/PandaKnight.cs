using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaKnight : PlayeableCharacterBase
{
    public override void Skill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Normal: // ȸ���ϴ� Į �� ������ ����
                if(!isCool_normal)
                {
                    isCool_normal = true;
                    GameObject bladeWhirl = Instantiate(normalSkillPrefab, transform);
                    SkillBase normalSkill = bladeWhirl.GetComponent<SkillBase>();
                    normalSkill.PlaySkill(this);
                }
                break;

            case SkillType.Ultimate: // ��Ȧ Ÿ�ٿ� ������(Ÿ�� ������ ����ó��)
                if (!isCool_ultimate)
                {
                    if(target != null)
                    {
                        isCool_ultimate = true;
                        GameObject blackHole = Instantiate(ultimateSkillPrefab, target.transform.position, Quaternion.identity);
                        SkillBase ultimateSkill = blackHole.GetComponent<SkillBase>();
                        ultimateSkill.PlaySkill(this);
                    }
                }
                break;
        }
    }

    public void NormalSkill()
    {
        Skill(SkillType.Normal);
    }

    public void UltimateSkill()
    {
        Skill(SkillType.Ultimate);
    }
}
