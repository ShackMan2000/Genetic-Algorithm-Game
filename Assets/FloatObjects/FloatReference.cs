using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatReference : ScriptableObject 
{
    private float variable;

    private void Awake()
    {
        Debug.Log("awake");
    }

    public float Variable
    {
        get
        {
            return variable;
        }

        set
        {
            variable = value;
        }
    }


}
