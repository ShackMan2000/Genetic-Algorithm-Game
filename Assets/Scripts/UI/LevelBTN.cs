using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBTN : MonoBehaviour
{



    [SerializeField]
    private GameObject lockImage, levelText;

    [SerializeField]
    private Level level;

    private Button thisBTN;


    private void Awake()
    {
        thisBTN = GetComponent<Button>();
        //  SaveManager.Instance.SaveGameLoaded += UpdateBTN;

    }

    private void Start()
    {
        UpdateBTN();
    }



    private void UpdateBTN()
    {
        if (SaveManager.Instance.saveData.levelsUnlocked >= level.levelID)
        {
            lockImage.SetActive(false);
            thisBTN.interactable = true;
            var lvlText = levelText.GetComponent<Text>();
            lvlText.text = (level.levelID + 1).ToString();
            levelText.SetActive(true);
        }
    }




    public void ButtonClicked()
    {
        if (GameManager.Instance.SaveData.levelsUnlocked >= level.levelID)
        {
            GameManager.Instance.LoadLevel(level);

        }
    }
}
