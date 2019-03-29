using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingButton : MonoBehaviour
{



    public bool isBlinking;

    [SerializeField]
    private float maxExpand = 1, speed = 1;
    private float scaleIncrease = 0;



    private void Update()
    {
        if (isBlinking)
        {            
            scaleIncrease = (Mathf.PingPong(Time.time * speed, maxExpand));
            transform.localScale = new Vector3((1 + scaleIncrease), (1 + scaleIncrease), 1);
        }

    }






}
