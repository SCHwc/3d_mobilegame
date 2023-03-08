using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Partner : SlotBase
{
    PartnerListWindow parent;
    

    protected override void Start()
    {
        base.Start();
        parent = GetComponentInParent<PartnerListWindow>();
        
    }

    void Update()
    {
        if (info != null)
        {
            iconImg.sprite = info.icon;
        }

        if (info == GameManager.Instance.currentInfo)
        {
            selectImg.SetActive(true);
        }
        else
        {
            selectImg.SetActive(false);
        }
    }

    public void SetInfo(PartnerInfo wantInfo)
    {
        info = wantInfo;
    }

    public void SelectPartner()
    {
        // ������ ������ ������ ������� ���� ��
        if (GameManager.Instance.currentInfo != null)
        {
            if (GameManager.Instance.currentInfo == info)
            {
                // ���� ���� ĭ�� ��ġ�ѰŶ�� null
                GameManager.Instance.currentInfo = null;
            }
            else
            {
                // �ƴ϶�� ���� ������ ĭ�� ������ �ѱ��
                GameManager.Instance.currentInfo = null;
                GameManager.Instance.currentInfo = info;
            }
        }
        else
        {
            GameManager.Instance.currentInfo = info;
        }

    }
}
