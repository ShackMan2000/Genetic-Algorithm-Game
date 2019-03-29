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

    [SerializeField]
    private int TESTLEVELID;

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
        //FOR TESTING
        if (GameManager.Instance == null)
            levels[TESTLEVELID].SetActive(true);
        else
            levels[GameManager.Instance.currentLevel.levelID].SetActive(true);

        GetSawsList();
    }    

    public void GetSawsList()
    {
        saws = FindObjectsOfType<Saw>();
    }       
}
