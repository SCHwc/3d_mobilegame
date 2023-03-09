using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllySkillBtn : ButtonBase
{
    [Tooltip("몇번 동료의 스킬을 발동할 것인지")]
    [SerializeField] CharacterType type;

    // 쿨타임 표기할 이미지
    [SerializeField] Image coolDownImage;

    // 스킬 아이콘
    Image _icon;
    // 스킬을 발동시킬 동료
    PartnerBase _target;

    private void Start()
    {
        // 형변환하여 파트너 베이스 할당
        _target = (PartnerBase)GameManager.Instance.party[(int)type];
        GameManager.Instance.skillBtns[(int)type] = this.gameObject;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // 쿨타임 표시
        if (_target.equipSkill.CurrentCoolTime > 0)
            coolDownImage.fillAmount = _target.equipSkill.coolTimeRate;
        else
            coolDownImage.fillAmount = 0f;
    }

    public override void ButtonDown()
    {
        _target.SkillCasting();
        // 스킬 사용 1초 후 QTE발동
        Invoke("QTE", 1f);
        
    }

    private void QTE()
    {
        if(type < CharacterType.Partner3)
        {
            int nextIdx = (int)type + 1;
            GameManager.Instance.OnQTE(nextIdx);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SelfDisable());
    }

    // 스킬버튼이 4초후 꺼지도록
    IEnumerator SelfDisable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
