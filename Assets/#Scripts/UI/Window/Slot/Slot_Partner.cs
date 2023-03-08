using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Partner : MonoBehaviour
{
    PartnerListWindow parent;
    public PartnerInfo info;

    Image iconImg;
    GameObject selectImg;

    void Start()
    {
        parent = GetComponentInParent<PartnerListWindow>();
        iconImg = transform.GetChild(1).gameObject.GetComponent<Image>();
        selectImg = transform.GetChild(0).gameObject;
        selectImg.SetActive(false);
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
        if (GameManager.Instance.currentInfo != null) { GameManager.Instance.currentInfo = null; }

        GameManager.Instance.currentInfo = info;
        parent.contextText.text = GameManager.Instance.currentInfo.GetContext();
    }
}
