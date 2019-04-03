using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RingOffireManager : MonoBehaviour
{

    private RingOfFire[] allChildRings;
    private RingOfFire[] soloRings;
    private RingOffireCollection[] ringCollections;


    [SerializeField]
    private float repeatLooptime = 1f, loopDelay;

    private Coroutine ringLoop;

    private void OnEnable()
    {
        GatesManager.newRoundStarts += Reset;
    }

    private void OnDisable()
    {
        GatesManager.newRoundStarts -= Reset;
    }


    private void Start()
    {
        loopDelay += GameManager.Instance.settings.launchDelay;

        ringCollections = GetComponentsInChildren<RingOffireCollection>();

        foreach (var collection in ringCollections)
        {
            collection.delayBeforeExplode += loopDelay;
            collection.ClaimChildRings();
            collection.AdjustStartTimeOfchilds();
        }


        allChildRings = GetComponentsInChildren<RingOfFire>();

        soloRings = allChildRings.Where(ring => !ring.isPartOfCollection).ToArray();

        foreach (var ring in soloRings)
        {
            ring.delayBeforeExplode += loopDelay;
        }
    }



    public void Reset()
    {
        StopRingLoop();
        StartRingLoop();
    }

    

    public void StartRingLoop()
    {
        ringLoop = StartCoroutine(StartAllRings());
    }


    public IEnumerator StartAllRings()
    {
        while (true)
        {
            print("start Them All");
            for (int i = 0; i < allChildRings.Length; i++)
            {
                allChildRings[i].StartAnimation();
            }
            yield return new WaitForSeconds(repeatLooptime);
        }
    }

    public void StopRingLoop()
    {

        if (ringLoop != null)
        {
            StopCoroutine(ringLoop);
        print("stop routine");

        }

        for (int i = 0; i < allChildRings.Length; i++)
        {
            allChildRings[i].StopAnimation();
        }
    }

}
