using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMoveChecker : MonoBehaviour 
{
    public CheckPoint checkPoint;

    private Vector3 checkPointPosition;

    
    private void Start()
    {
        checkPoint = GetComponentInChildren<CheckPoint>();

        checkPointPosition = checkPoint.gameObject.transform.position;
        StartCoroutine(CheckIfCheckPointMoved());
    }

    private IEnumerator CheckIfCheckPointMoved()
    {
        yield return new WaitForSeconds(5f);
     
       // print("didnt move the checkpoint at all");
    }
}
