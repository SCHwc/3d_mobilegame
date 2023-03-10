using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllySkillBtn : ButtonBase
{
    // 쿨타임 표기할 이미지
    [SerializeField] Image coolDownImage;

    // 스킬 아이콘
    public Image _icon;
    // 스킬을 발동시킬 동료
    PartnerBase _target;

    private void Start()
    {
        BattleSceneManager.Instance.skillBtn = this;
        _icon = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_target == null) return;

        // 쿨타임 표시
        if (_target.equipSkill.coolTimeRate > 0)
            coolDownImage.fillAmount = _target.equipSkill.coolTimeRate;
        else
            coolDownImage.fillAmount = 0f;
    }

    public override void ButtonDown()
    {
        if(_target != null)
        {
            _target.SkillCasting();
            BattleSceneManager.Instance.isQTESuccess = true;
        }
    }

    public void SetTarget(PartnerBase wantTarget)
    {
        _target = wantTarget;
    }

}
