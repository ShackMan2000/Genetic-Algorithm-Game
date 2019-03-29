using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundNumber : MonoBehaviour {

    public string RoundIt(float inputNumber)
    {
        float deducedNumber = inputNumber;
        string apendix ="";



        if (inputNumber >= 1000000000000000000f)
        {
            apendix = "qu";
            deducedNumber = inputNumber / 1000000000000000000f;
        }
        else if (inputNumber >= 1000000000000000f)
        {
            apendix = "qa";
            deducedNumber = inputNumber / 1000000000000000f;
        }
        else if (inputNumber >= 1000000000000f)
        {
            apendix = "t";
            deducedNumber = inputNumber / 1000000000000f;
        }
        else if (inputNumber >= 1000000000f)
        {
            apendix = "b";
            deducedNumber = inputNumber / 1000000000f;
        }
        else if (inputNumber >= 1000000f)
        {
            apendix = "m";
            deducedNumber = inputNumber / 1000000f;
        }
        else if (inputNumber >= 1000f)
        {
            apendix = "k";
            deducedNumber = inputNumber/1000f;
        }



        return deducedNumber.ToString("F2") + apendix;
    }


}
