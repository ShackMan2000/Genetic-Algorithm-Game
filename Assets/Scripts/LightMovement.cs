using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{

    // 1 unit equals the length of 5 lights

    [SerializeField]
    private MutationStat speedStat, minDistance, maxDistance, mutateSecond, mutateDirection, incremental;

    private int currentStep;

    private float speed;


    private Rigidbody2D rigidBody;

    public List<float> pathSeconds;
    public List<Vector3> pathRotations;

    public Coroutine moveRoutine;


    public int lockedSteps = 0;

    [SerializeField]
    private GameSettings settings;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        speed = speedStat.currentValue + 0.3f;
        
    }
    
 


    public void StartMoving()
    {
        moveRoutine = StartCoroutine(Move());
    }
    public void StopMoving()
    {
        rigidBody.velocity = Vector3.zero;
    }


    private IEnumerator Move()
    {
        float moveTime;
        currentStep = 0;

        //for initial burst
        rigidBody.velocity = transform.up * speed;
        // /5 so that 1 light moves one lenght of it's own per unit
        yield return new WaitForSecondsRealtime((settings.burstFromGateTime/5) / speed);


        while (true)
        {          
            if (currentStep < pathRotations.Count)
            {
                transform.eulerAngles = pathRotations[currentStep];
                moveTime = pathSeconds[currentStep];
            }
            else
            {
                Vector3 nextRotation = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
                transform.eulerAngles = nextRotation;
                pathRotations.Add(nextRotation);

                moveTime = Random.Range(minDistance.currentValue, maxDistance.currentValue);
                pathSeconds.Add(moveTime);
            }

            rigidBody.velocity = transform.up * speed;

            currentStep++;

            yield return new WaitForSecondsRealtime((moveTime/5f) / (speed));
        }
    }
          


    public void MutatePath()
    {

        float secondsChange = mutateSecond.currentValue /100f;
        float rotationChange = (mutateDirection.currentValue/100f) * 180f;

        for (int i = 0; i < pathRotations.Count; i++)
        {
            //incremental Modifier
            var incrementalAdjuster = incremental.currentValue + (100 - incremental.currentValue) * ((float)i / pathRotations.Count);
            //0.3 min to not make single dot in the beginning
           //incrementalAdjuster = Mathf.Clamp(incrementalAdjuster, 0.3f, 1.0f);
           incrementalAdjuster /= 100f;

            pathRotations[i] = new Vector3(0, 0, pathRotations[i].z + Random.Range(-rotationChange * incrementalAdjuster, rotationChange * incrementalAdjuster));

            pathSeconds[i] = pathSeconds[i] + Random.Range(-secondsChange * pathSeconds[i] * incrementalAdjuster, secondsChange * pathSeconds[i] * incrementalAdjuster);

            pathSeconds[i] = Mathf.Clamp(pathSeconds[i], minDistance.currentValue, maxDistance.currentValue);
        }
    }



    public void ChopOffRestPath()
    {
        if (pathSeconds.Count > currentStep + 1)
        {
            for (int i = currentStep; i < pathSeconds.Count; i++)
            {
                pathSeconds.RemoveAt(i);
                pathRotations.RemoveAt(i);
            }
        }
    }



    public void ClearPath()
    {
        if (pathRotations.Count > currentStep + 1)
        {
            pathRotations.RemoveRange(currentStep, pathRotations.Count - currentStep);
            pathSeconds.RemoveRange(currentStep, pathSeconds.Count - currentStep);
        }
    }
}
