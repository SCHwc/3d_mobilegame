using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Party : SlotBase
{
    // �迭�� �� ��ȣ
    public int index;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (GameManager.Instance.currentInfo != null)
        {
            selectImg.SetActive(true);
        }
        else { selectImg.SetActive(false); }
    }

    public void AddParty()
    {
        if (info == null)
        {
            if (GameManager.Instance.currentInfo == null) { return; }
            // ����ִ� ������ ���ٸ� ����ֱ�
            info = GameManager.Instance.currentInfo;
            GameManager.Instance.currentInfo = null;
            iconImg.sprite = info.icon;
            GameManager.Instance.party[index] = info;
        }
        else
        {
            info = null;
            iconImg.sprite = null;
            GameManager.Instance.party[index] = null;
        }
    }
}
