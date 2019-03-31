using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public Level currentLevel;
    

    public GameSettings settings;
       

    [SerializeField]
    private SaveData saveData;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {

            }
            return instance;
        }
    }







    public float Money
    {
        get
        {
            return SaveData.money;
        }
        set
        {
            SaveData.money = value;
            OnMoneyChanged(value);        
        }
    }

    public SaveData SaveData
    {
        get
        {
            return SaveManager.Instance.saveData;
        }

        set
        {
            saveData = SaveManager.Instance.saveData; 
        }
    }
    public Action<float> OnMoneyChanged = delegate { };




    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

  


    public void LoadLevel(Level newLevel)
    {
        currentLevel = newLevel;

        SceneManager.LoadScene("GameScene");
    }



     [ContextMenu("Wipe Save")]
    public void WipeSaveData()
    {
        FindObjectOfType<UpgradeManager>().ResetAllstats();

        SaveData.showTutorial1 = true;
        SaveData.showTutorial2 = true;
        SaveData.moneySpent = 0;
        SaveData.money = settings.moneyAtStart;
        SaveData.levelsUnlocked = 0;
        SaveData.bestTimes = new float[20];

        SceneManager.LoadScene("MenuScene");


        SaveManager.Instance.SaveGame();
    }



  
}






