using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitCounter : MonoBehaviour
{
   public Text itCountText;

    public float hitCount;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitCount ++;
        itCountText.text = hitCount.ToString();
    }

}
