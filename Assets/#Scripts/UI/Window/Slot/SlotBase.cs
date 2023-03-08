using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotBase : MonoBehaviour
{
    protected Image iconImg;
    protected GameObject selectImg;
    public PartnerInfo info;

    protected virtual void Start()
    {
        iconImg = transform.GetChild(1).gameObject.GetComponent<Image>();
        selectImg = transform.GetChild(0).gameObject;
        selectImg.SetActive(false);
    }

}
