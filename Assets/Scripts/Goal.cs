using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public bool needsMergedLights;
    private GateAnimator gateAnimator;

    [SerializeField]
    private GameSettings settings;

    [SerializeField]
    private GameObject lightDummy;

    // Use this for initialization
    void Start()
    {
        gateAnimator = GetComponent<GateAnimator>();

        if (GameManager.Instance.currentLevel.gateColors.Length > 1)
        { needsMergedLights = true;
            gateAnimator.SetColor(settings.colors[GameManager.Instance.currentLevel.mergedColor]);
        }
        else
            gateAnimator.SetColor(settings.colors[GameManager.Instance.currentLevel.gateColors[0]]);
    }


    

    public IEnumerator LevelWonLaunchLights()
    {
        int grenadeCount = 10;

        while (grenadeCount >= 0)
        {
            grenadeCount --;
            for (int i = 0; i < 200; i++)
            {
                GameObject launchingLight = Instantiate(lightDummy);
            }
            yield return new WaitForSeconds(2f);
        }
    
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Light enteredLight = collision.collider.GetComponent<Light>();

        if (!needsMergedLights)
            LightEnterdGoal(enteredLight);

        else if (enteredLight.isMerged)
            LightEnterdGoal(enteredLight);
    }


    private void LightEnterdGoal(Light enteredLight)
    {
        enteredLight.Die();
        LevelManager.Instance.IncreaseScoreByOne();
    }




}

