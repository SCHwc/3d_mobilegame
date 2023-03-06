using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    public AIBase owner;
    public Image cooltime;
    void Start()
    {
        
    }

    void Update()
    {
        cooltime.fillAmount = owner.equipSkill.coolTimeRate;
    }
}
