using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchEffect : MonoBehaviour
{
    Image _image;
    RectTransform _rectTransform;

    private void Start()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _image.enabled = false;
    }

    private void OnMouseDown()
    {
        _rectTransform.position = Input.touches[0].position;
        _image.enabled = true;
    }

    private void OnMouseDrag()
    {
        _rectTransform.position = Input.touches[0].position;
    }

    private void OnMouseUp()
    {
        _image.enabled = false;
    }

}
