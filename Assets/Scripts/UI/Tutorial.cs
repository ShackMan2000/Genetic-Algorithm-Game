using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{


    [SerializeField]
    private GameObject wClickStart, wRandomDirection, wTimeIsShort,
                        wAllDeadSorting, wRepeatingPath, wModifiyIt, wKillEarly;




    [SerializeField]
    private Button mutationBTN, menuBTN;




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
        if (SaveManager.Instance.saveData.showTutorial1)
        {
            wClickStart.SetActive(true);
            LockButtons();
        }

        else if (SaveManager.Instance.saveData.showTutorial2 && GameManager.Instance.currentLevel.levelID != 0) 
        {
            LockButtons();
            wKillEarly.SetActive(true);
        }
        else
        {
            FindObjectOfType<ShowHidePanel>().HideBTNclicked();
            gameObject.SetActive(false);

        }


    }


    private void StartTutorial()
    {
        //triggered through event when Start Game is clicked
        if (SaveManager.Instance.saveData.showTutorial1)
        {
            StartCoroutine(ShowRandom());
            wClickStart.SetActive(false);

        }
    }


    private IEnumerator ShowRandom()
    {
        yield return new WaitForSeconds(0.5f + GameManager.Instance.settings.launchDelay);
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



    public void Finishtutorial1()
    {
        wModifiyIt.SetActive(false);
        StartCoroutine(SpeedUpTime());
        FindObjectOfType<ShowHidePanel>().HideBTNclicked();
        SaveManager.Instance.saveData.showTutorial1 = false;
        UnlockButtons();
    }


    private IEnumerator ShowKillEarly()
    {
        yield return new WaitForSeconds(0.5f + GameManager.Instance.settings.launchDelay);
        wKillEarly.SetActive(true);
        StartCoroutine(SlowDownTime());
    }


    public void Finishtutorial2()
    {
        wKillEarly.SetActive(false);
        StartCoroutine(SpeedUpTime());
        FindObjectOfType<ShowHidePanel>().HideBTNclicked();
        SaveManager.Instance.saveData.showTutorial2 = false;
        UnlockButtons();
    }



    private void LockButtons()
    {
        mutationBTN.interactable = false;
        menuBTN.interactable = false;
    }

    private void UnlockButtons()
    {
        mutationBTN.interactable = true;
        menuBTN.interactable = true;
    }





    private IEnumerator SlowDownTime()
    {
        float t = 1;
        while (t > 0)
        {
            t -= (Time.deltaTime * 5);
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
        while (t < 1)
        {
            t += Time.deltaTime * 5;
            Time.timeScale = t;

            if (t > 1)
                t = 1;
            yield return null;
        }

    }


}
