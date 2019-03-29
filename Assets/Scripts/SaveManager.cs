using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    [SerializeField]
    private MutationStat[] stats;

    public SaveData saveData;

    private static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveManager>();
            }

            return instance;
        }
    }

    private string gameDataFileName = "saveData.json";

    public string filePath;

    // public static Action SaveGameLoaded = delegate { };
    //upgradeItem, levelBTN





    private void Awake()
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
        DontDestroyOnLoad(this.gameObject);

        filePath = Application.dataPath + Path.DirectorySeparatorChar + gameDataFileName;

        //  SceneManager.sceneLoaded += OnSceneLoaded;

        saveData = new SaveData();
        LoadGame();
    }



    ////loading and event call from awake finishes before other objects are instantiated, so call again here
    //private void Start()
    //{
    //    SaveGameLoaded();
    //}

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{

    //}


    public void LoadGame()
    {

        string dataAsJson;
        dataAsJson = File.ReadAllText(filePath);
        saveData = JsonUtility.FromJson<SaveData>(dataAsJson);

        for (int i = 0; i < stats.Length; i++)
        {
            stats[i].upgradesBought = saveData.upgradesBought[i];
            for (int u = 0; u < stats[i].upgradesBought; u++)
            {
                stats[i].ResetValues();
                stats[i].Upgrade(onGameLoaded: true);

            }
        }

        //  SaveGameLoaded();
    }


    public void SaveGame()
    {

        for (int i = 0; i < stats.Length; i++)
        {
            saveData.upgradesBought[i] = stats[i].upgradesBought;
        }

        string dataAsJson = JsonUtility.ToJson(saveData);


        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(dataAsJson);

        }
    }


    //public void ResetAllUpgrades()
    //{
    //    saveData.money += saveData.moneySpent;
    //    saveData.moneySpent = 0;
    //    foreach (MutationStat stat in stats)
    //    {

    //    }
    //}


    [ContextMenu("CreateSaveFile")]
    public void CreateSaveFileThroughEditor()
    {
        //must be done manually so save game exists when loading
        //can be deleted once all variables in saveData are written and json file is synced 
        saveData = new SaveData();
        SaveGame();
    }


}
