using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSwitcher : MonoBehaviour 
{

    [SerializeField]
    private GameObject startPointPlaceHolder;

    private Vector3 startPoint;



    private void Awake()
    {
        startPoint = startPointPlaceHolder.transform.position;
        startPointPlaceHolder.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("knife"))
        {
          //  collision.GetComponent<ElectricSaw>().NextStep();
            collision.gameObject.transform.position = startPoint;


        }
    }
}
