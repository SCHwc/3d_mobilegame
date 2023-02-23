using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public PlayeableCharacterBase player; 

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
            Destroy(this);
    }

    void Update()
    {
        
    }
}
