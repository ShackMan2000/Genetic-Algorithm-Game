using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{

    // 1 unit equals the length of 4 lights

    [SerializeField]
    private MutationStat speedStat, moveRange, mutationRange, incremental;

    private int step;

    [SerializeField]
    private float speed;

    private float diameter = 0.25f;

    public bool moving;

    [SerializeField]
    private LayerMask wallLayer;

    public List<Vector2> path;

    private Vector2 nextWayPoint;

    [SerializeField]
    private GameSettings settings;


    void Awake()
    {
        speed = speedStat.currentValue + 0.3f;
    }


    public void StartMoving()
    {
        step = -1;
        SetNextWaypoint();
        moving = true;
    }



    private void FixedUpdate()
    {
        if (moving)
            MoveToGoal();
    }


    private void MoveToGoal()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, nextWayPoint, Time.fixedDeltaTime * speed);

        if ((Vector2)transform.position == nextWayPoint)
        {
            SetNextWaypoint();
        }
    }


    private void SetNextWaypoint()
    {
        step++;

        if (step < path.Count)
        {
            nextWayPoint = path[step];
        }
        else
        {
            nextWayPoint = CreateReachableWayPoint(transform.position, 1.05f, transform.position);
            path.Add(nextWayPoint);
        }
    }



    private Vector2 CreateReachableWayPoint(Vector2 origin, float range, Vector2 startPoint)
    {
        int loopCount = 1;
        Vector2 nextReachableWaypoint = Vector2.zero;
        bool foundMarker = false;

        while (!foundMarker)
        {
            nextReachableWaypoint = Random.insideUnitCircle * range + origin;

            if (!Physics2D.Linecast(startPoint, nextReachableWaypoint, wallLayer))
                foundMarker = true;

            loopCount++;
            if (loopCount%10 == 0)
            {
                range += 0.05f;
                if(loopCount == 200)
                    print("hard to find a point from " + startPoint + " to " + origin);
            }
        }
        if(loopCount > 200)
            print(loopCount + " iterations to find " + nextReachableWaypoint);


        return nextReachableWaypoint;
    }
    




    public List<Vector2> GetPath()
    {
        return path;
    }

    public void OverridePath(List<Vector2> newPath)
    {
        path = new List<Vector2>(newPath);
    }



    public void MutatePath()
    {     
        float rangeInCircleUnits = mutationRange.currentValue * diameter;

        for (int i = 1; i < path.Count; i++)
        {         
            // var incrementalAdjuster = incremental.currentValue + (100 - incremental.currentValue) * ((float)i / path.Count);
            // incrementalAdjuster /= 100f;
            Vector2 mutatedWaypoint = CreateReachableWayPoint(path[i], rangeInCircleUnits, path[i - 1]);
            if (mutatedWaypoint != Vector2.zero)
            {
                path[i] = mutatedWaypoint;
            }
            //else
            //{
            //    string f = gameObject.name;
            //    print("replacing " + i + " aqs in "  + path[i-1] + " with " + lastGoodWaypoint + " " + f);
            //    path[i - 1] = lastGoodWaypoint;
            //    mutatedWaypoint = CreateReachableWayPoint(path[i], rangeInCircleUnits, path[i - 1]);
            //    if (mutatedWaypoint != Vector2.zero)
            //    {
            //        //speed = 0;
            //        //return;
            //        if (Physics2D.Linecast(path[i-1], path[i], wallLayer))
            //            print("bullshit");
            //        path[i] = mutatedWaypoint;
            //    }
              
            //}

        }
    }



    public void ChopOffRestPath()
    {
        path.RemoveRange(step, path.Count - step);
    }

}
