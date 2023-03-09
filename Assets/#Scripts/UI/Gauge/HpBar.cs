using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    // �θ� Ʈ������
    Transform parent; 
    // �޾ƿ� ����
    StatBase _stat;
    // ü���� ũ�� ������ �����̴�
    Slider _slider;

    void Start()
    {
        parent = transform.parent;
        // �θ𿡼� ���Ȱ�������
        _stat = parent.GetComponent<StatBase>();
        _slider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        // ������ �Ӹ� ���� �ߵ���
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);

        // UI�� ȸ�������� ī�޶��� ȸ������� ������ 
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.HPRate;
        _slider.value = ratio;
    }
}
