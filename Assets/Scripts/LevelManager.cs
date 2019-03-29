using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{


    [SerializeField]
    private Camera cam;

    [SerializeField]
    private MutationStat lightsAtStart;

    [SerializeField]
    private GameObject[] canvi;
    [SerializeField]
    private LevelWonOverlay levelWonOverlay;


    [SerializeField]
    private GameSettings settings;

    [SerializeField]
    private MutationStat lifeSpan;

    [SerializeField]
    private GameObject startBTN, killEarlyBTN;

    [SerializeField]
    private ProgressBar progressBar;


    public bool KillEmAllBTNLocked;


    [SerializeField]
    private GameObject goalPrefab;

    private Goal goal;

    private bool gamestarted;

    private Level currentLevel;

    private float score;

    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
    }




    public List<int> colorCounts;

    [SerializeField]
    private List<Text> countTexts;

    [SerializeField]
    private List<GameObject> countIcons;
    public bool levelWon;
    private float timeElapsed;
    private bool timerRunning;

    [SerializeField]
    private Text timeElapsedText;


    public Action OnGameStarted = delegate { };




    private void Awake()
    {
        if (instance == null)
            instance = this;

        colorCounts = new List<int> { 0, 0, 0 };

    }


    void Start()
    {

        if (!Music.Instance.audioSource.isPlaying)
            Music.Instance.audioSource.Play();


        currentLevel = GameManager.Instance.currentLevel;

        Instantiate(currentLevel.grid);

        goal = Instantiate(goalPrefab, currentLevel.goalPosition, Quaternion.identity).GetComponent<Goal>();


        SetScale();

        SetInitialScores();

    }

    private void SetScale()
    {
        var zoom = currentLevel.zoomLevel;

        cam.orthographicSize *= zoom;

        if (zoom > 1)
        {
            for (int i = 0; i < canvi.Length; i++)
            {
                var oldUISCale = canvi[i].GetComponent<RectTransform>().localScale;
                var newScale = oldUISCale * currentLevel.zoomLevel;
                canvi[i].GetComponent<RectTransform>().localScale = newScale;

            }
        }

    }

    public void StartGameBTNClicked()
    {
        LockKillemAllBTN();
        GatesManager.Instance.UnleashFirstRound();
        startBTN.SetActive(false);
        progressBar.gameObject.SetActive(true);
        timerRunning = true;
        OnGameStarted();
    }


    private void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;
            timeElapsedText.text = NumberConverter.FormatTimeToString(timeElapsed);
        }
    }

    void SetInitialScores()
    {
        //trigger to set bars
        score = -1;
        IncreaseScoreByOne();

        countIcons[0].GetComponent<Image>().color = settings.colors[currentLevel.gateColors[0]];

        if (currentLevel.gateColors.Length == 1)
        {
            countIcons[1].SetActive(false);
            countIcons[2].SetActive(false);

            countIcons.RemoveAt(1);
            countIcons.RemoveAt(1);
        }
        else
        {
            countIcons[1].GetComponent<Image>().color = settings.colors[currentLevel.gateColors[1]];
            countIcons[2].GetComponent<Image>().color = settings.colors[currentLevel.mergedColor];
        }

        ChangeLightCount(-1, 0);
    }


    public void ChangeLightCount(int countToChange, int changeBy)
    {
        if (countToChange == -1)
        {
            for (int i = 0; i < colorCounts.Count; i++)
            {
                colorCounts[i] = 0;
                countTexts[i].text = 0.ToString();
            }
        }
        else
        {
            colorCounts[countToChange] += changeBy;
            countTexts[countToChange].text = colorCounts[countToChange].ToString();
        }
    }


    public void IncreaseScoreByOne()
    {
        if (levelWon)
            return;

        score++;
        progressBar.ChangeBarFill(score, (float)lightsAtStart.currentValue);

        if (score == lightsAtStart.currentValue)
            LevelWon();
    }




    private void LevelWon()
    {
        levelWon = true;
        timerRunning = false;

        if (GameManager.Instance.SaveData.levelsUnlocked <= currentLevel.levelID)
        {
            GameManager.Instance.SaveData.levelsUnlocked = currentLevel.levelID + 1;
        }



        levelWonOverlay.gameObject.SetActive(true);

        levelWonOverlay.ShowScore(timeElapsed, currentLevel);
        StartCoroutine(goal.LevelWonLaunchLights());


      SaveManager.Instance.SaveGame();

    }






    public void LockKillemAllBTN()
    {
        KillEmAllBTNLocked = true;
        killEarlyBTN.SetActive(false);
    }






    public IEnumerator UnlockKillEmAllButton(float currentLifespan)
    {
        yield return new WaitForSeconds(lifeSpan.currentValue);

        if ((currentLifespan - (2 * lifeSpan.currentValue) > 0.1f))
            killEarlyBTN.SetActive(true);
        yield return new WaitForSeconds(currentLifespan - (2 * lifeSpan.currentValue));
        killEarlyBTN.SetActive(false);

    }


}

