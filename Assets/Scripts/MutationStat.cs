using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MutationStat : ScriptableObject
{
    // public string title;

    // public int id;

    public float currentValue;

    [HideInInspector]
    public float minValue, maxValue;

    public float initialValue, valueCap;

    public float upgradeStep;

    public string unitSymbol;

    public bool showDecimal, hasRange;

    [TextArea(1, 10)]
    public string infoText;

    public float basePrice, costIncrease;

    public int upgradesBought;

    public bool isMaxed;

    public int unlockAtLevel;



    public void ResetValues()
    {
        isMaxed = false;

        currentValue = minValue = maxValue = initialValue;

        upgradesBought = 0;

        OneFreeUpgrade();

        //buy an upgrade so user has some sliders to play around with

    }


  



    public void OneFreeUpgrade()
    {
        if (hasRange)
        {
            if (upgradeStep > 0)
                maxValue += upgradeStep;
            else
                minValue += upgradeStep;
        }
    }





    public void Upgrade(bool onGameLoaded = false)
    {

        //using optional parameter to loop through when loading game
        if (!onGameLoaded)
        {
            GameManager.Instance.Money -= CurrentPrice();
            GameManager.Instance.SaveData.moneySpent += CurrentPrice();
            upgradesBought++;
        }

        if (!hasRange)
        {
            currentValue += upgradeStep;
            isMaxed = CheckMaxed(currentValue);
        }
        else if (upgradeStep > 0)
        {
            maxValue += upgradeStep;
            isMaxed = CheckMaxed(maxValue);
        }
        else
        {
            minValue += upgradeStep;
            isMaxed = CheckMaxed(minValue);
        }
    }




    public float CurrentPrice()
    {
        float currentPrice = Mathf.Pow(costIncrease, upgradesBought) * (costIncrease - 1);
        currentPrice = currentPrice / (costIncrease - 1);
        currentPrice *= basePrice;

        return currentPrice;
    }





    private bool CheckMaxed(float newValue)
    {
        return Mathf.Abs(newValue - valueCap) < 0.01f;
    }

}
