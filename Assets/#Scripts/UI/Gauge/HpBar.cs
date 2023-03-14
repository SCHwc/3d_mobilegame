using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    // 자신의 rectTransform
    RectTransform _rectTransform;
    // 부모 트랜스폼
    Transform parent; 
    // 받아올 스탯
    StatBase _stat;
    // 체력의 크기 보여줄 슬라이더
    Slider _slider;
    // 부모 콜라이더 (높이를 받기 위해)
    Collider _col;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        parent = transform.parent;
        // 부모에서 스탯가져오기
        _stat = parent.GetComponent<StatBase>();
        _col = parent.GetComponent<Collider>();
        _slider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        // 주인의 머리 위에 뜨도록
        _rectTransform.anchoredPosition = Vector3.up * (_col.bounds.size.y + parent.position.y);

        // UI의 회전방향을 카메라의 회전방향과 같도록 
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.HPRate;
        _slider.value = ratio;
    }
}
