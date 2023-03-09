using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllySkillBtn : ButtonBase
{
    [Tooltip("��� ������ ��ų�� �ߵ��� ������")]
    [SerializeField] CharacterType type;

    // ��Ÿ�� ǥ���� �̹���
    [SerializeField] Image coolDownImage;

    // ��ų ������
    Image _icon;
    // ��ų�� �ߵ���ų ����
    PartnerBase _target;

    private void Start()
    {
        // ����ȯ�Ͽ� ��Ʈ�� ���̽� �Ҵ�
        _target = (PartnerBase)GameManager.Instance.party[(int)type];
        GameManager.Instance.skillBtns[(int)type] = this.gameObject;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // ��Ÿ�� ǥ��
        if (_target.equipSkill.CurrentCoolTime > 0)
            coolDownImage.fillAmount = _target.equipSkill.coolTimeRate;
        else
            coolDownImage.fillAmount = 0f;
    }

    public override void ButtonDown()
    {
        _target.SkillCasting();
        // ��ų ��� 1�� �� QTE�ߵ�
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

    // ��ų��ư�� 4���� ��������
    IEnumerator SelfDisable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
