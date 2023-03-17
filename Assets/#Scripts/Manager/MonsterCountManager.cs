using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCountManager : MonoBehaviour
{
    public GameObject animDoorObj;

    [SerializeField] List<GameObject> monsters = new List<GameObject>();
    public int monsterCount
    {
        get => monsters.Count;
    }
    public bool monsterDisable
    {
        get
        {
            return monsterCount == 0;
        }
    }

    void Update()
    {
        foreach (var current in monsters)
        {
            if (current == null)
            {
                monsters.Remove(current);
            }

        }
        if (monsterDisable == true)
        {
            animDoorObj.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
