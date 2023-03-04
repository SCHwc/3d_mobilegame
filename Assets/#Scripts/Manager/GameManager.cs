using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public PlayeableCharacterBase player;
    public CameraShake cameraShaker;

    public static GameObject swordEffect;

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

        swordEffect = Resources.Load<GameObject>("Prefabs/Effects/SwordImpact");
        cameraShaker = Camera.main.GetComponent<CameraShake>();
    }

}
