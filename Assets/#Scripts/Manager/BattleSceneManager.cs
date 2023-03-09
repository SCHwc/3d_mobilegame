using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private static BattleSceneManager _instance;
    public static BattleSceneManager Instance { get => _instance; }

    // ���� ���� â���� 
    public PartnerBase[] partners = new PartnerBase[3];

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        Initialize();
    }

    private void Initialize()
    {
        
    }
}
