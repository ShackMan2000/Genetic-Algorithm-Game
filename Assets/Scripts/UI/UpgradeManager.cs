using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private UpgradeItem[] upgrades;

    [SerializeField]
    private BlinkingButton upgradeBTN;


    private void Awake()
    {
        foreach (var item in upgrades)
        {
            item.UpgradeBought += CheckOtherUpdates;
        }
    }


    private void Start()
    {
        CheckOtherUpdates();
    }



    public void ResetAllstats()
    {

        GameManager.Instance.Money += SaveManager.Instance.saveData.moneySpent;

        SaveManager.Instance.saveData.moneySpent = 0;
     

        foreach (var stat in upgrades)
        {
            stat.ResetStat();
        }
        SaveManager.Instance.SaveGame();

        CheckOtherUpdates();
    }



    public void CheckOtherUpdates()
    {
        var atLeastOneAvailable = false;

        foreach (var item in upgrades)
        {
            if (item.UpdateText() == true)
            {
                atLeastOneAvailable = true;
            }
        }

        upgradeBTN.isBlinking = atLeastOneAvailable;



    }

}
