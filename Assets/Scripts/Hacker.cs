using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hacker : MonoBehaviour
{


    public MutationStat[] allStats;


    private static Hacker instance = null;


    public static Hacker Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Hacker>();
            }

            return instance;
        }

    }





    void Awake()

    {

        if (instance == null)

        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

        }

        else
        {
            Destroy(gameObject);

        }
    }



    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MenuScene");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveManager.Instance.SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.Instance.LoadGame();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {

        }
     
      
        if (Input.GetKeyDown(KeyCode.N))
        {
            FindObjectOfType<GatesManager>().NewRound();
        }

    }


    [ContextMenu("ClearPlayerPrefs")]
    void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }



    [ContextMenu("Unlock All Levels")]
    public void UnlockAllLevels()
    {
        GameManager.Instance.SaveData.levelsUnlocked = 20;
        SaveManager.Instance.SaveGame();
    }
    [ContextMenu("BestValues")]
    public void BestValues()
    {
        foreach (var stat in allStats)
        {
            stat.currentValue = stat.valueCap;
        }
    }





    //  [ContextMenu("Reset Stats")]
    public void WipeSaveData()
    {
        foreach (var stat in allStats)
        {
            stat.ResetValues();
         //   SaveManager.Instance.saveData.money = gameSettings.mo
        }
    }




}
