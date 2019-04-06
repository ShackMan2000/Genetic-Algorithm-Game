using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRender, whiteCenter;

    [SerializeField]
    private GameSettings settings;

    private Color mergedColor;

    private Vector2 goalPosition;

    public bool dead = true;

    public bool reachedCheckpoint;

    private Vector2 gatePosition;

    public LightMovement movement;
    
    public float distanceToGoal;

    private Vector2 startPosition = Vector2.zero;
    private Vector3 startRotation = Vector3.zero;

    private bool collided;

    public Gate myGate;

    private int myGateID;

    private CheckPoint myCheckPoint;

    public bool isMerged;
    private Coroutine lerproutine;

    private Color centerColor;

    public bool hasBeenCloned;

    public static int TESTINT;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();

        mergedColor = settings.colors[GameManager.Instance.currentLevel.mergedColor];
        
        goalPosition = GameManager.Instance.currentLevel.goalPosition;

        gameObject.name = "light " + TESTINT.ToString();
        TESTINT++;
    }


    public void ConnectToGate(Gate gate, CheckPoint checkpoint)
    {
        myGate = gate;
        myGateID = myGate.gateId;
        transform.parent = myGate.transform;
        myCheckPoint = checkpoint;
        SetStartPositionAndRotation();
    }


    private void SetStartPositionAndRotation()
    {
        gatePosition = new Vector2(myGate.transform.position.x, myGate.transform.position.y);
        startPosition = Random.insideUnitCircle * 0.3f + gatePosition;
        startRotation = new Vector3(0, 0, Random.Range(0, 360));
    }


    public void PrepareToLaunch()
    {
        hasBeenCloned = false;
        isMerged = false;
        reachedCheckpoint = false;
        collided = false;
        dead = false;

        whiteCenter.color = centerColor = new Color(1,1,1,0.5f);

        gameObject.layer = myGate.gateId + 10;

        transform.position = startPosition;
        transform.eulerAngles = startRotation;

        gameObject.SetActive(true);
    }

    public void ClearRemainingPath()
    {
        movement.ChopOffRestPath();
    }

    public void FreeBird()
    {
        ClearRemainingPath();
        if(lerproutine != null)
            StopCoroutine(lerproutine);

        lerproutine = StartCoroutine(Blink());
    }



    public void SetColor(Color newColor)
    {
        spriteRender.color = newColor;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {    
        if (collision.CompareTag("knife"))
        {
            Die();

        }
        else if ((collision.CompareTag("0") && myGateID == 0) 
            || (collision.CompareTag("1") && myGateID == 1))
        {
           

            if (!reachedCheckpoint)
            {
                whiteCenter.color = centerColor = Color.white;
                reachedCheckpoint = true;
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //only one of the colors needs to check 

        if (myGate.gateId == 1 && !isMerged)
        {
            if (collision.collider.CompareTag("light"))
            {
                var otherLight = collision.collider.GetComponent<Light>();

                if (!otherLight.isMerged)
                {
                    MergeLights();
                    otherLight.MergeLights();
                }
            }
        }   
    }


    public void MergeLights()
    {

        gameObject.layer = 8;
        isMerged = true;
        SetColor(mergedColor);
        LevelManager.Instance.ChangeLightCount(myGate.gateId, -1);
        LevelManager.Instance.ChangeLightCount(2, 1);

    }
    


    public void Die()
    {
        if(dead)
            return;

        dead = true;
        //only on violent death
       // movement.ChopOffRestPath();
        movement.moving = false;

        SetDistanceToGoal();


        if (isMerged)
            LevelManager.Instance.ChangeLightCount(2, -1);
        else
            LevelManager.Instance.ChangeLightCount(myGate.gateId, -1);

        gameObject.SetActive(false);
        myGate.LightDied(this);
    }


          

    public void SetDistanceToGoal()
    {
        if (!reachedCheckpoint)
        {
            distanceToGoal = Vector2.Distance(transform.position, myCheckPoint.transform.position);
            distanceToGoal += 10000;
        }
        else
        {
            distanceToGoal = Vector2.Distance(transform.position, goalPosition);
        }

    }

    //Ignoring the fact that merging and gaining a new color stops the blinking
    private IEnumerator Blink()
    {
        int blinkRounds = 0;

        while(blinkRounds < 10)
        {
            spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 0);
            whiteCenter.color = new Color(1,1,1,0);
            yield return new WaitForSeconds(0.05f);
            spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 1);
            whiteCenter.color = centerColor;
            yield return new WaitForSeconds(0.05f);
            blinkRounds ++;
        }
        yield return null;

    }

}





