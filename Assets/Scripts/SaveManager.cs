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

        saveData = new SaveData();
        LoadGame();

    }




    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("save"))
            return;

        string dataAsJson;

        //dataAsJson = File.ReadAllText(filePath);


        dataAsJson = PlayerPrefs.GetString("save");
        dataAsJson = Base64Decode(dataAsJson);



        saveData = JsonUtility.FromJson<SaveData>(dataAsJson);

        for (int i = 0; i < stats.Length; i++)
        {
            stats[i].ResetValues();
            stats[i].upgradesBought = saveData.upgradesBought[i];

            for (int u = 0; u < stats[i].upgradesBought; u++)
            {
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

        PlayerPrefs.SetString("save", Base64Encode(dataAsJson));


        //using (StreamWriter writer = new StreamWriter(filePath))
        //{
        //    writer.Write(dataAsJson);

        //}
    }




    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }


    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }





    [ContextMenu("CreateSaveFile")]
    public void CreateSaveFileThroughEditor()
    {
        //must be done manually so save game exists when loading
        //can be deleted once all variables in saveData are written and json file is synced 
        saveData = new SaveData();
        SaveGame();
    }


}
