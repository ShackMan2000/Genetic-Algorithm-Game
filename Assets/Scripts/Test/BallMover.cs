using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    private Rigidbody2D rb;

    private double timeStamp;

    public Vector3 startPos, goal;
    public float speed;

    public List<double> moveTimes;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }


    private void FixedUpdate()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, goal, Time.fixedDeltaTime * speed);
        if(transform.position == goal)
        {
            moveTimes.Add(Time.time - timeStamp);
            timeStamp = Time.time;
            transform.position = startPos;
        }



    }

}
