using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{

    public float money, moneySpent;
    
    public bool showTutorial1, showTutorial2;

    public int[] upgradesBought = new int[11];

    public int levelsUnlocked = 1;

    public float[] bestTimes = new float[20];

}
