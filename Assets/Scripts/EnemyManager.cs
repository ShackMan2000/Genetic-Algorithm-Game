using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private Saw[] saws;

    [SerializeField]
    private List<GameObject> levels;
  

    private static EnemyManager instance;

    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyManager>();
            }

            return instance;
        }
    }

    private void Start()
    {
        Instantiate(levels[GameManager.Instance.currentLevel.levelID]);
        GetSawsList();
    }

    public void GetSawsList()
    {
        saws = FindObjectsOfType<Saw>();
    }
}
