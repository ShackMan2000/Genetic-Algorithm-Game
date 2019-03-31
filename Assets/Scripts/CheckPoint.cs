using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private Camera cam;
    public int gateID;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private Color halfColor, fullColor;

    [SerializeField]
    private float minX, maxX, minY, maxY;

    private Coroutine blinkroutine;

    [SerializeField]
    private float blinktime = 0.1f;

    private bool blinking;



    public Color FullColor
    {
        get
        {
            return fullColor;
        }

        set
        {
            fullColor = value;
            halfColor = new Color(FullColor.r, FullColor.g, FullColor.b, 0.5f);
            spriteRenderer.color = halfColor;
        }
    }




    private void Awake()
    {
        cam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnMouseDrag()
    {
        if (blinkroutine != null)
            StopCoroutine(blinkroutine);
        boxCollider.enabled = false;

        spriteRenderer.color = FullColor;


        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        float newX = cam.ScreenToWorldPoint(Input.mousePosition).x;
        float newY = cam.ScreenToWorldPoint(Input.mousePosition).y;

        newX = Mathf.Clamp(newX, -cam.orthographicSize * cam.aspect * minX, cam.orthographicSize * cam.aspect * maxX);
        newY = Mathf.Clamp(newY, -cam.orthographicSize * minY, cam.orthographicSize * maxY);

        transform.position = new Vector3(newX, newY, 0);

    }


    private void OnMouseUp()
    {
        spriteRenderer.color = halfColor;
        boxCollider.enabled = true;
    }


    public void Blink()
    {
        if(blinkroutine != null)
        {
            StopCoroutine(blinkroutine);            
        }
        blinkroutine = StartCoroutine(BlinkRoutine());
    }


    private IEnumerator BlinkRoutine()
    {
        Color blinkColor = (halfColor + Color.white) / 2f;
        Vector3 expandedScale = new Vector3(1.1f, 1.1f, 1);

        for (float i = 0; i < blinktime; i+= Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(halfColor, blinkColor,i/blinktime);
            transform.localScale = Vector3.Lerp(Vector3.one, expandedScale, i/blinktime);
            yield return null;
        }
        for (float i = 0; i < blinktime; i += Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(blinkColor, halfColor, i/blinktime);
            transform.localScale = Vector3.Lerp(expandedScale, Vector3.one, i / blinktime);
            yield return null;
        }





    }


}
