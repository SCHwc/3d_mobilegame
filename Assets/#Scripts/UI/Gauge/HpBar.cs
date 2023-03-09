using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    // 부모 트랜스폼
    Transform parent; 
    // 받아올 스탯
    StatBase _stat;
    // 체력의 크기 보여줄 슬라이더
    Slider _slider;

    void Start()
    {
        parent = transform.parent;
        // 부모에서 스탯가져오기
        _stat = parent.GetComponent<StatBase>();
        _slider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        // 주인의 머리 위에 뜨도록
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);

        // UI의 회전방향을 카메라의 회전방향과 같도록 
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.HPRate;
        _slider.value = ratio;
    }
}
