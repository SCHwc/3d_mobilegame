using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaKnight : PlayeableCharacterBase
{
    public override void Skill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Normal: // 회전하는 칼 내 몸에서 고정
                if(!isCool_normal)
                {
                    isCool_normal = true;
                    GameObject bladeWhirl = Instantiate(normalSkillPrefab, transform);
                    SkillBase normalSkill = bladeWhirl.GetComponent<SkillBase>();
                    normalSkill.PlaySkill(this);
                }
                break;

            case SkillType.Ultimate: // 블랙홀 타겟에 날리기(타겟 없으면 예외처리)
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
