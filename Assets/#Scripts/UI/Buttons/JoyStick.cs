using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능 지원

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector2 inputDirection;
    private bool isInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // 드래그 시작시
    public void OnBeginDrag(PointerEventData eventData)
    {
        JoyStickControll(eventData);
        isInput = true;
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        JoyStickControll(eventData);
    }

    // 드래그 끝낼때
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
    }

    private void JoyStickControll(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange; // 스틱의 움직인 양을 0 ~ 1사이로 정규화
    }

    private void Update()
    {
        if (isInput)
            Managers.Input.inputVector = inputDirection;
        else
            Managers.Input.inputVector = Vector2.zero;
    }
}

