using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnimator : MonoBehaviour
{


    [SerializeField]
    private List<GameObject> swushes;

    private float[] extraRounds;

    public float intervallTime = 0.3f;
    private float intervallTimer;

    [SerializeField]
    private float scaleSpeed, extraRoundTime;

    public float rotationSpeed;

    [SerializeField]
    private float speed;
    private float originalSpeed;

    public bool startGate;
    private float launchAnimationTime;
    private float launchAnimationCounter;

    private bool isAccelaerating, isDecelerating;

    private void Start()
    {
        launchAnimationTime = GameManager.Instance.launchAnimationTime;

        if (!startGate)
        {
            scaleSpeed = -scaleSpeed;
            speed = speed / 3;
            gameObject.layer = 9;
        }
        else
        {
            originalSpeed = speed;
        }
     
    }
    

    public void SetColor(Color newColor)
    {
        for (int i = 0; i < swushes.Count; i++)
        {
            swushes[i].GetComponentInChildren<SpriteRenderer>().color = newColor;
        }
    }


    public void LaunchAnimation()
    {
        launchAnimationCounter = 0;
        isAccelaerating = true;
    }


    void Update()
    {
        if (isAccelaerating)
        {
            launchAnimationCounter += Time.deltaTime;
            speed += 5 * Time.deltaTime;

            if (launchAnimationCounter > launchAnimationTime)
            {
                launchAnimationCounter = 0;
                isAccelaerating = false;
                isDecelerating = true;
            }
        }
        else if (isDecelerating)
        {
            launchAnimationCounter += Time.deltaTime * 5;
            speed -= 5 * Time.deltaTime * 5;

            if (launchAnimationCounter > launchAnimationTime)
            {
                isDecelerating = false;
            }
        

    }
        AnimateSwushes();          

    }

    public void ResetSpeed()
    {
        speed = originalSpeed;
    }


    private void AnimateSwushes()
    {
        intervallTimer += Time.deltaTime * speed;


        for (int i = 0; i < swushes.Count; i++)
        {
            var swush = swushes[i];
            swush.transform.Rotate(new Vector3(0, 0, rotationSpeed * speed));

            var newx = swush.transform.localScale.x + scaleSpeed * speed * Time.deltaTime;
            var newY = swush.transform.localScale.y + scaleSpeed * speed * Time.deltaTime;

            if ((startGate && newx <= 1) || (!startGate && newx > 0 && i > 3))
            {
                swush.transform.localScale = new Vector3(newx, newY, 1);
            }


        }


        if (intervallTimer >= intervallTime)
        {
            intervallTimer = 0;

            var swushToRecycle = swushes[swushes.Count - 1];
            swushes.Remove(swushToRecycle);
            swushes.Insert(0, swushToRecycle);

            if (startGate)
                swushToRecycle.transform.localScale = new Vector3(0, 0, 1);
            else
                swushToRecycle.transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
