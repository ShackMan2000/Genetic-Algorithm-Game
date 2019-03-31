using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatesManager : MonoBehaviour
{

    private static GatesManager instance;
    public static GatesManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GatesManager>();
            }
            return instance;
        }
    }




    public float burstAtStartTime;

    [SerializeField]
    private Text remainingTimeText;

    [SerializeField]
    private GameSettings settings;

    [SerializeField]
    private GameObject gatePF;
    public Gate[] gates;


    [SerializeField]
    private MutationStat lights, lifeSpan;

    public float maxLightsTotal;
    public float activeLightsTotal;


    public bool lightsActive = false;
    public float currentLifeSpan;
    public float lifeSpanCounter;

    private int gatesReady = 0;


    public delegate void NewRoundDelegate();
    public static event NewRoundDelegate newRoundStarts;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Start()
    {
        InstantiateGates();

        maxLightsTotal = lights.currentValue * gates.Length;
    }


    private void InstantiateGates()
    {
        Level level = GameManager.Instance.currentLevel;

        gates = new Gate[level.GatePositions.Length];

        for (int i = 0; i < level.GatePositions.Length; i++)
        {
            var newGate = Instantiate(gatePF, level.GatePositions[i], Quaternion.identity).GetComponent<Gate>();
            gates[i] = newGate;

            newGate.gateId = i;
            newGate.SetColor(settings.colors[level.gateColors[i]]);

            if (level.gateRotations.Length == level.GatePositions.Length)
                newGate.transform.eulerAngles = level.gateRotations[i];
        }
    }


    private void Update()
    {
        if (lightsActive)
        {
            lifeSpanCounter -= Time.deltaTime;
            if (lifeSpanCounter < 0)
            {
                KillRemainingLights();
            }
        }
        remainingTimeText.text = lifeSpanCounter.ToString("F1") + "s";

    }

    public void UnleashFirstRound()
    {
        for (int i = 0; i < gates.Length; i++)
        {
            gates[i].PrepareFirstRound();
        }

        if (newRoundStarts != null)
            newRoundStarts();
    }




    [ContextMenu("NewRound")]
    public void NewRound()
    {
        LevelManager.Instance.LockKillemAllBTN();
        StopCoroutine(LevelManager.Instance.UnlockKillEmAllButton(0));
        LevelManager.Instance.ChangeLightCount(-1, 0);


        if (newRoundStarts != null)
            newRoundStarts();
        // EnemyManager.Instance.ActivateEnemies();

        for (int i = 0; i < gates.Length; i++)
        {
            gates[i].PrepareNewRound();
        }
    }


    public void GateReadyToLaunch()
    {
        gatesReady++;
        if (gatesReady == gates.Length)
        {
            gatesReady = 0;
            LaunchAllGates();
        }
    }


    private void LaunchAllGates()
    {

        currentLifeSpan += lifeSpan.currentValue;
        lifeSpanCounter = currentLifeSpan;
        StartCoroutine(LevelManager.Instance.UnlockKillEmAllButton(currentLifeSpan));

        for (int i = 0; i < gates.Length; i++)
        {
            gates[i].LaunchLights();
        }
        lightsActive = true;
    }


    public void KillRemainingLights()
    {
        lightsActive = false;
        //otherwise might start new Round before finishing the Kill loop
        List<Gate> gatesWithLightsLeft = new List<Gate>();

        for (int i = 0; i < gates.Length; i++)
        {
            if (gates[i].activeLights.Count > 0)
            {
                gatesWithLightsLeft.Add(gates[i]);
            }
        }

        for (int i = 0; i < gatesWithLightsLeft.Count; i++)
        {
            gatesWithLightsLeft[i].KillRemainingLights();
        }
    }




    public void KillEarly()
    {
        currentLifeSpan -= lifeSpanCounter;
        KillRemainingLights();
    }


    public void AllLightsFromThisGateDead()
    {
        bool allGatesReady = true;

        for (int i = 0; i < gates.Length; i++)
        {
            if (!gates[i].allLightsDead)
                allGatesReady = false;
        }

        if (allGatesReady && !LevelManager.Instance.levelWon)
        {
            NewRound();
        }

    }


    public void FreeBirdBTNClicked()
    {
        for (int i = 0; i < gates.Length; i++)
        {
            gates[i].FreeBirdBTNClicked();
        }
    }
}
