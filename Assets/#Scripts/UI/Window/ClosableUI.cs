using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosableUI : MonoBehaviour
{
    // �ش� â�� �̸�
    public string windowName;
    // �ش� â�� �����ִ°�?
    public bool isOpen;

    public static Dictionary<string, ClosableUI> uiDic = new Dictionary<string, ClosableUI>();

    Animator anim;

    public static void OpenUI(string wantName)
    {
        if (uiDic.ContainsKey(wantName))
        {
            uiDic[wantName].PlayOpen();
        }
    }

    public static void CloseUI(string wantName)
    {
        if (uiDic.ContainsKey(wantName))
        {
            uiDic[wantName].PlayClose();
        }
    }

    public void PlayOpen()
    {

    }

    public void PlayClose()
    {

    }

    public void Disable()
    {
        
    }

    void Start()
    {
        if (!isOpen)
        {
            Invoke("Disable", 0.3f);
        }
    }
}
