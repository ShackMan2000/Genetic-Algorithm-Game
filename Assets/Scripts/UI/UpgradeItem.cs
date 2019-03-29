using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{

    [SerializeField]
    private MutationStat stat;

    [SerializeField]
    private Text titleText;


    private bool canBuy;


    [SerializeField]
    private Sprite btnEnabled, btnDisabled;

    [SerializeField]
    private Image BTNImage;

    private float buyAmount = 1f;


    [SerializeField]
    private Text currentValue, priceText, gainText;

    public Action UpgradeBought = delegate { };



    //Editor only, not important for build
    private void OnValidate()
    {
        if (stat != null)
        {
            gameObject.name = stat.name + " Upgrade";
            titleText.text = stat.name;
        }
    }

       

    private void Start()
    {
        GetComponent<UpgradeLocker>().Unlock(stat.unlockAtLevel);
        UpdateText();

    }




    public bool UpdateText()
    {
        BTNImage.sprite = btnDisabled;
        canBuy = false;

        string plusOrMinus = stat.upgradeStep > 0 ? "+" : "";

        gainText.text = plusOrMinus + NumberConverter.NumberToString(stat.upgradeStep, stat.showDecimal) + stat.unitSymbol;

        if (stat.hasRange)
        {
            currentValue.text = NumberConverter.NumberToString(stat.minValue, stat.showDecimal) + "- " +
               NumberConverter.NumberToString(stat.maxValue, stat.showDecimal) + stat.unitSymbol;
        }
        else
        {
            currentValue.text = NumberConverter.NumberToString(stat.currentValue, stat.showDecimal) + stat.unitSymbol;
        }


        priceText.text = NumberConverter.NumberToString(stat.CurrentPrice(), stat.showDecimal) + "$";


        if (stat.isMaxed)
        {
            priceText.text = "maxed";
        }
        else if (GameManager.Instance.Money >= stat.CurrentPrice())
        {
            BTNImage.sprite = btnEnabled;
            canBuy = true;
        }

        return canBuy;

    }

    public void ResetStat()
    {
        stat.ResetValues();
        UpdateText();
    }


    public void UpgradeBTNClicked()
    {
        if (canBuy)
        {
            stat.Upgrade();
            UpdateText();
            SaveManager.Instance.SaveGame();
            UpgradeBought();
        }
    }


}
























