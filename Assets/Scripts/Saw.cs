using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{

    [SerializeField]
    private List<Vector3> checkpointsPath;

    [SerializeField]
    private List<float> checkpointsFreezeTime;

    public int step = 0;

    [SerializeField]
    private float moveSpeed, rotationSpeed;

    private float startDelay;

    public bool TESTprint;
    public float  TESTtimeStamp;

    public bool moving;

   // private GatesManager gatesManager;


    [SerializeField]
    private float freezeTimer;
    public Vector3 startPosition, startRotation;

    public bool hasOwnPath, isBigWheelAxis;

    public bool gameHasStarted = false;


    private void OnEnable()
    {
        GatesManager.newRoundStarts += Restart;
    }

    private void OnDisable()
    {
        GatesManager.newRoundStarts -= Restart;
    }

    public void PrintTime()
    {
        if(TESTprint)
            print(TESTtimeStamp - Time.time);
    }

    private void Awake()
    {
        CheckforController();

        
        startDelay = GatesManager.Instance.burstAtStartTime;

        startPosition = transform.localPosition;

        if (isBigWheelAxis)        {

            startRotation = transform.eulerAngles;
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
        }
        else
        {
            startRotation = new Vector3(0, 0, Random.Range(0, 360));
            transform.eulerAngles = startRotation;
        }


        if (checkpointsPath.Count > 0)
        {
            var newStartPosition = new Vector3(transform.localPosition.x, transform.localPosition.y);
            checkpointsPath[0] = newStartPosition;

            hasOwnPath = true;
            freezeTimer += checkpointsFreezeTime[0];
        }
    }



    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1) * Time.fixedDeltaTime * rotationSpeed);

        if (hasOwnPath)
        {
            MoveAlongPath();
        }    

    }


    private void MoveAlongPath()
    {
        if (moving)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, checkpointsPath[step], moveSpeed * Time.fixedDeltaTime);
            if (transform.localPosition == checkpointsPath[step])
            {
                moving = false;
                freezeTimer = checkpointsFreezeTime[step];
            }
        }
        else
        {
            freezeTimer -= Time.fixedDeltaTime;
            if (freezeTimer <= 0)
            {
                freezeTimer = 0;
                NextStep();
            }

        }
    }

    public void NextStep()
    {
        step++;
        if (step == checkpointsPath.Count)
            step = 0;

        moving = true;
    }



    public void Restart()
    {
        if(TESTprint)
            TESTtimeStamp = Time.time;

        StopAllCoroutines();
        step = 0;

        if (hasOwnPath)
        {
            transform.localPosition = startPosition;
            freezeTimer = startDelay + checkpointsFreezeTime[0];
        }
        transform.eulerAngles = startRotation;
    }





    private void CheckforController()
    {
        var controller = FindObjectOfType<SawController>();
        if (controller != null)
        {
            moveSpeed = controller.masterSpeed;
        }
    }

}

