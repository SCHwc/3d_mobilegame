using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : ButtonBase
{
    [SerializeField] SkillType type;      // ���� ��ų�� ��ư����
    [SerializeField] Image coolDownImage; // ��Ÿ�� �׸���

    private void Update()
    {
        if(GameManager.Instance.player != null)
        {
            switch(type)
            {
                case SkillType.Normal:
                    if (GameManager.Instance.player.equipSkill.coolTimeRate > 0)
                    {
                        coolDownImage.fillAmount = GameManager.Instance.player.equipSkill.coolTimeRate;
                    }
                    else
                        coolDownImage.fillAmount = 0;
                    break;

                case SkillType.Ultimate:
                    if (GameManager.Instance.player.ultimateSkill.coolTimeRate > 0)
                    {
                        coolDownImage.fillAmount = GameManager.Instance.player.ultimateSkill.coolTimeRate;
                    }
                    else
                        coolDownImage.fillAmount = 0;
                    break;
            }

        }
    }

    public void Click()
    {
        GameManager.Instance.player.Skill(type);
    }

}
