using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public Camera mainCam;
    public Camera subCam;

    public void ShowSubCam()
    {
        mainCam.gameObject.SetActive(false);
        subCam.gameObject.SetActive(true);
    }

    public void ShowMainCam()
    {
        mainCam.gameObject.SetActive(true);
        subCam.gameObject.SetActive(false);
    }


}
