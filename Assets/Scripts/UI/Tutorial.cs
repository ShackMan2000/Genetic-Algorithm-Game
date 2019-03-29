using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{


    [SerializeField]
    private GameObject wClickStart, wRandomDirection, wTimeIsShort,
                        wAllDeadSorting, wRepeatingPath, wModifiyIt;


    private void OnEnable()
    {
        LevelManager.Instance.OnGameStarted += StartTutorial;
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnGameStarted -= StartTutorial;
    }


    private void Start()
    {
        if (SaveManager.Instance.saveData.showTutorial == false)
        {
            FindObjectOfType<ShowHidePanel>().HideBTNclicked();

            gameObject.SetActive(false);
        }
    }


    private void StartTutorial()
    {
        //triggered through event when Start Game is clicked
        wClickStart.SetActive(false);
        StartCoroutine(ShowRandom());
    }


    private IEnumerator ShowRandom()
    {
        yield return new WaitForSeconds(1.0f + GameManager.Instance.settings.launchDelay);

        wRandomDirection.SetActive(true);
        StartCoroutine(SlowDownTime());

        //NextOne triggered via Button


    }


    public void StartTimeIsShort()
    {
        StartCoroutine(SpeedUpTime());
        StartCoroutine(ShowTimeIsShort());
    }

    private IEnumerator ShowTimeIsShort()
    {
        wRandomDirection.SetActive(false);        

        yield return new WaitForSeconds(1.0f);


        wTimeIsShort.SetActive(true);
        StartCoroutine(SlowDownTime());
    }



    public void StartAllDead()
    {
        StartCoroutine(ShowAllDeadSorting());
    }



    private IEnumerator ShowAllDeadSorting()
    {
        wTimeIsShort.SetActive(false);
        StartCoroutine(SpeedUpTime());

        float timeToWait = FindObjectOfType<GatesManager>().lifeSpanCounter;
        yield return new WaitForSeconds(timeToWait + 0.1f);

        wAllDeadSorting.SetActive(true);
        Time.timeScale = 0;
    }



    public void StartRepeatingPath()
    {
        StartCoroutine(ShowRepeatPath());
    }

    private IEnumerator ShowRepeatPath()
    {
        wAllDeadSorting.SetActive(false);

        StartCoroutine(SpeedUpTime());

        yield return new WaitForSeconds(2.0f);

        wRepeatingPath.SetActive(true);
        Time.timeScale = 0;
    }


    public void StartModify()
    {
        wRepeatingPath.SetActive(false);
        wModifiyIt.SetActive(true);
    }



    public void Finishtutorial()
    {
        StartCoroutine(SpeedUpTime());
        FindObjectOfType<ShowHidePanel>().HideBTNclicked();
    }



    private IEnumerator SlowDownTime()
    {
        float t = 1;
        while (t > 0)
        {
            t -= (Time.deltaTime *3);
            print(t);
            if (t < 0.1)
            {
                t = 0;
            }
           Time.timeScale = t;

            yield return null;
        }

    }
    private IEnumerator SpeedUpTime()
    {
        float t = 0.1f;
        while (t <= 1)
        {
            t += Time.deltaTime * 3;
            Time.timeScale = t;

            if (t > 1)
                t = 1;
            yield return null;
        }

    }


}
