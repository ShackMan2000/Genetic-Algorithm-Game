using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatSlider : MonoBehaviour
{

    [SerializeField]
    private MutationStat stat, chanceStat;

    [SerializeField]
    private Text titleText, currentValueText, chanceText;

    public Slider slider;

    private LevelUI levelUI;


    private void OnValidate()
    {
        if (stat != null)
        {
            gameObject.name = stat.name + " slider";
            titleText.text = stat.name;
        }
    }


    private void Awake()
    {
        if (SaveManager.Instance.saveData.levelsUnlocked < stat.unlockAtLevel)
            Destroy(this.gameObject);

        GetComponentInChildren<Button>().onClick.AddListener(InfoBTNClicked);

        levelUI = FindObjectOfType<LevelUI>();
    }

    void Start()
    {
        if (chanceStat != null)
        {
            chanceText.gameObject.SetActive(true);
            chanceText.text = "Chance: " + Mathf.RoundToInt((chanceStat.currentValue)).ToString() + "%";
        }


        slider.minValue = stat.minValue;
        slider.maxValue = stat.maxValue;

        slider.onValueChanged.AddListener(delegate { ValueChanged(); });
        slider.value = stat.minValue;
        stat.currentValue = stat.minValue;

     

        UpdateValueText();
    }



    public void ValueChanged()
    {
        stat.currentValue = slider.value;
        UpdateValueText();
    }



    private void UpdateValueText()
    {
        if (stat.showDecimal)
            currentValueText.text = ((float)(Mathf.RoundToInt(slider.value * 10)) / 10).ToString() + stat.unitSymbol;
        else
            currentValueText.text = Mathf.RoundToInt(slider.value).ToString() + stat.unitSymbol;
    }



    private void InfoBTNClicked()
    {
        levelUI.OpenInfoPanel(stat);
    }


}
