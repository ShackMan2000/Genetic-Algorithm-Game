using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : MonoBehaviour
{


    [SerializeField]
    private float secondsForFullExpand;

    public bool isPartOfCollection;
    private float maxExpand;
    private SpriteRenderer spriteRenderer;
    private Color myColor;
    private CircleCollider2D col;

    private Coroutine explodeAnimation;

    public float delayBeforeExplode;

    private bool animationIsRunning;




    // Use this for initialization
    void Awake()
    {
        maxExpand = transform.localScale.x;
        transform.localScale = new Vector3(0.0f, 0.0f, 0f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        myColor = spriteRenderer.color;
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;

        if (secondsForFullExpand == 0)
            secondsForFullExpand = 1;
    }


    public void StartAnimation()
    {
        if(!animationIsRunning)
            explodeAnimation = StartCoroutine(Explode());
    }

    public void StopAnimation()
    {
        if (explodeAnimation != null)
        {            
            StopCoroutine(explodeAnimation);
            animationIsRunning = false;
        }

        transform.localScale = new Vector3(0.0f, 0.0f, 0f);
        col.enabled = false;

    }


    private IEnumerator Explode()
    {
        animationIsRunning = true;
        yield return new WaitForSecondsRealtime(delayBeforeExplode);

        col.enabled = true;
        spriteRenderer.color = myColor;
        float timeTillDeath = secondsForFullExpand;
        float expandPerSecond = maxExpand / secondsForFullExpand;
        float spriteAlpha = 1;

        while (transform.localScale.x < maxExpand)
        {

            Vector3 newScale = new Vector3(transform.localScale.x + Time.deltaTime * expandPerSecond,
                                     transform.localScale.y + Time.deltaTime * expandPerSecond,
                                     1);
            transform.localScale = newScale;

            timeTillDeath -= Time.deltaTime;

            if (timeTillDeath <= 0.2f)
            {
                spriteAlpha -= Time.deltaTime * 10f;
                spriteRenderer.color = new Color(myColor.r, myColor.g, myColor.b, spriteAlpha);
            }
            yield return null;
        }
        transform.localScale = new Vector3(0.0f, 0.0f, 1f);
        col.enabled = false;
        animationIsRunning = false;
    }
}
