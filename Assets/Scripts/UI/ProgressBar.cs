using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour 
{

    [SerializeField]
    private Image progressBarfill;

    [SerializeField]
    private GameSettings settings;

    [SerializeField]
    private Text progressText;

    private void Awake()
    {
        SetBarcolor();
    }

    private void SetBarcolor()
    {
        Color barColor;
        if (GameManager.Instance.currentLevel.gateColors.Length > 1)        
            barColor = settings.colors[GameManager.Instance.currentLevel.mergedColor];        
        else
            barColor = settings.colors[GameManager.Instance.currentLevel.gateColors[0]];

        progressBarfill.color = barColor;
    }

    public void ChangeBarFill(float currentScore, float scoreNeeded)
    {
        progressBarfill.fillAmount = currentScore / scoreNeeded;
        progressText.text = ((currentScore / scoreNeeded) * 100).ToString("F1") + "%";
    }
   
}
