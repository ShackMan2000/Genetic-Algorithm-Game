using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GameSettings : ScriptableObject 
{
    public float moneyAtStart;

    public Color[] colors;

    public float burstFromGateTime;

    public float[] rewards;

    public float launchDelay;
}
