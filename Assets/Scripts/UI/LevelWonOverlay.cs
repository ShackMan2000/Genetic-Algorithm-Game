using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWonOverlay : MonoBehaviour
{


    [SerializeField]
    private ShowHidePanel mutationPanelmover;

    [SerializeField]
    private Text timeText, bestTimeText, rewardText;

    private Level currentLevel;

    [SerializeField]
    private GameSettings settings;

    public void ShowScore(float time, Level currentLevel)
    {



        this.currentLevel = currentLevel;



        mutationPanelmover.HidePanel();

        timeText.text = "Time: " + NumberConverter.FormatTimeToString(time) + "s";

        float bestTime = GameManager.Instance.SaveData.bestTimes[currentLevel.levelID];

        if (bestTime == 0 || time < bestTime)
        {
            GameManager.Instance.SaveData.bestTimes[currentLevel.levelID] = time;
            bestTimeText.color = Color.green;
        bestTimeText.text = "Best Time: " + NumberConverter.FormatTimeToString(time) + "s";
        }
        else
        {
            bestTimeText.text = "Best Time: " + NumberConverter.FormatTimeToString(bestTime) + "s";
        }


        GiveReward(time);


    }

    private void GiveReward(float time)
    {
        GameManager.Instance.Money += settings.rewards[currentLevel.levelID];

       
        rewardText.text = "Reward: " + NumberConverter.NumberToString(settings.rewards[currentLevel.levelID], false) +"$";
    }
}
