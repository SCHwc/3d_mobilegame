using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartnerListWindow : MonoBehaviour
{

    // 슬롯을 생성할 위치(부모)
    public Transform slotTransform;
    // 슬롯 프리팹
    public GameObject slotPrefab;
    // 선택한 캐릭터의 정보를 보여줄 텍스트
    public TextMeshProUGUI contextText;

    void Start()
    {
        foreach (var current in GameManager.partnerList)
        {
            Slot_Partner slot = GameObject.Instantiate(slotPrefab, slotTransform).GetComponent<Slot_Partner>();
            slot.SetInfo(current.Value);
        }
    }

    private void OnDisable()
    {
        // 해당 창을 끈다면 선택되어있는 칸을 해제한다.
        GameManager.Instance.currentInfo = null;
    }


}
