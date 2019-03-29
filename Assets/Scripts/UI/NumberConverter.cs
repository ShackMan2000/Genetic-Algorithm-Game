using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NumberConverter : MonoBehaviour
{
    public static string FormatTimeToString(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(100 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:0}", minutes, seconds, milliseconds) + "s";
    }



    public static string NumberToString(float number, bool showDecimal)
    {
        string outString;

        if (number >= 1000000000)
        {
            outString = (number / 1000000000).ToString("F2") + "b";

        }
        else if (number >= 1000000)
        {
            outString = (number/1000000).ToString("F2") + "m";

        }
        else if (number >= 1000)
        {
            outString = (number/1000).ToString("F2") + "k";
        }
        else
        {
            if (showDecimal)
                outString = number.ToString("F1");
            else
                outString = number.ToString("F0");
        }

        return outString;
    }
}
