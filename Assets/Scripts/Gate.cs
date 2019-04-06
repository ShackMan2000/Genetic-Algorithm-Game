
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField]
    private MutationStat maxLights, cloneRange, cloneChance, freebirdChance;

    public int gateId;

    [SerializeField]
    private GameObject LightPrefab;

    public CheckPoint checkPoint;

    public Vector3 goalPosition;

    public List<Light> activeLights, deadLights;

    private GateAnimator gateAnimator;


    public bool allLightsDead;

    private Color color;

    public bool TESTPrintMe;

    public float stamp1,stamp2, stamp3;


    private void Awake()
    {
        gateAnimator = GetComponent<GateAnimator>();
    }


    public void SetColor(Color newColor)
    {
        color = newColor;

        gateAnimator.SetColor(newColor);
        checkPoint.gateID = gateId;
        checkPoint.FullColor = newColor;

        checkPoint.gameObject.transform.tag = (gateId == 0) ? "0" : "1";
    }



    public void PrepareFirstRound()
    {
        for (int i = 0; i < (int)maxLights.currentValue; i++)
        {
            Light newLight = newLight = Instantiate(LightPrefab.GetComponent<Light>(), Vector2.zero, Quaternion.identity);
            newLight.ConnectToGate(this, checkPoint);
            activeLights.Add(newLight);
        }

        StartCoroutine(PrepareToLaunch());
    }



    public void PrepareNewRound()
    {
     //  print("Preparing Gate " + gateId +" at " + Time.time);
        stamp1 = Time.time;

        List<Light> sortedLights = deadLights.OrderBy(o => o.distanceToGoal).ToList();
        deadLights = new List<Light>(sortedLights);

        ChooseLightsToClone();

        foreach (var light in deadLights)
        {
            light.SetColor(color);

        }

        activeLights.AddRange(deadLights);
        deadLights.Clear();


        MutateLights();

        StopCoroutine("PrepareToLaunch");

        StartCoroutine(PrepareToLaunch());
    }




    private void ChooseLightsToClone()
    {
        int lightsToCloneRange = Mathf.RoundToInt(deadLights.Count * cloneRange.currentValue / 100f);

        for (int i = 0; i < lightsToCloneRange; i++)
        {
            if (deadLights.Count == 0)
                return;

            Light originalLight = deadLights[0];
            activeLights.Add(originalLight);

            deadLights.RemoveAt(0);

            CloneLight(originalLight);
        }
    }



    private void CloneLight(Light originalLight)
    {
        originalLight.hasBeenCloned = true;

        float thisCloneChance = cloneChance.currentValue;

        while (thisCloneChance > 0f && deadLights.Count > 0)
        {
            if (thisCloneChance >= Random.Range(0f, 1f))
            {
                Light clone = deadLights[deadLights.Count - 1];
                clone.movement.OverridePath(originalLight.movement.GetPath());
                activeLights.Add(clone);
                deadLights.Remove(clone);
            }
            thisCloneChance -= 100f;
        }
    }



    private void MutateLights()
    {
        foreach (var light in activeLights)
        {
            if (!light.hasBeenCloned)
                light.movement.MutatePath();
        }
    }



    private IEnumerator PrepareToLaunch()
    {
        for (int i = 0; i < activeLights.Count; i++)
        {
            Light launchingLight = activeLights[i];

            launchingLight.PrepareToLaunch();
        }



        gateAnimator.LaunchAnimation();
        yield return new WaitForSecondsRealtime(GameManager.Instance.settings.launchDelay);
      

        GatesManager.Instance.GateReadyToLaunch();
    }



    public void LaunchLights()
    {

        allLightsDead = false;
        LevelManager.Instance.ChangeLightCount(gateId, activeLights.Count);

        for (int i = 0; i < activeLights.Count; i++)
        {

            activeLights[i].movement.StartMoving();
        }


      
    }



    public void KillRemainingLights()
    {
        List<Light> remainingLights = new List<Light>(activeLights);
        for (int i = 0; i < remainingLights.Count; i++)
        {
            remainingLights[i].Die();
        }
    }





    public void LightDied(Light deadLight)
    {

        activeLights.Remove(deadLight);
        deadLights.Add(deadLight);

        if (activeLights.Count == 0)
        {
            allLightsDead = true;
            GatesManager.Instance.AllLightsFromThisGateDead();
        }
    }




    public void FreeBirdBTNClicked()
    {
        for (int i = 0; i < activeLights.Count; i++)
        {
            if (Random.Range(0f, 100f) > 100 - freebirdChance.currentValue)
            {
                activeLights[i].FreeBird();
            }
        }
    }


}
