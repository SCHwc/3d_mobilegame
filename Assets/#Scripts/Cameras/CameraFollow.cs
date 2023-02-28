using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 normalOffset;  
    public Transform target;

    void Update()
    {
        if (!target || target.gameObject.activeInHierarchy == false)
        {
            transform.position = new Vector3(0, 15.73f, -10.7f);
            transform.localEulerAngles = new Vector3(60, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(60, 0, 0);
            //transform.position = player.transform.position + normalOffset;
            transform.position = target.transform.position + normalOffset;
        }
    }
}
