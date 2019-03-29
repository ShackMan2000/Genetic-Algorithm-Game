using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOffireCollection : MonoBehaviour 
{
    public float delayBeforeExplode;

    RingOfFire[] rings;  



    public void ClaimChildRings()
    {
        rings = GetComponentsInChildren<RingOfFire>();
        foreach (var ring in rings)
        {
            ring.isPartOfCollection = true;
        }
    }

    public void  AdjustStartTimeOfchilds()
    {
        foreach (var ring in rings)
        {
            ring.delayBeforeExplode += delayBeforeExplode;
        }
    }



}
