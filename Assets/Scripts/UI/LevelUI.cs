using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{

    public GameObject InfoPanel, menuPanel, menuButtons, credits, freeBirdBTN;


    [SerializeField]
    private Text infoText, titleText;

    private int lastMutationID = -1;

    [SerializeField]
    private ShowHidePanel mutationPanel;


    private void Start()
    {
        if(GameManager.Instance.currentLevel.levelID >=8)
        {
            freeBirdBTN.SetActive(true);

        }
        else
            freeBirdBTN.SetActive(false);
    }








    public void HidePanel()
    {
        var uiManager = FindObjectOfType<LevelUI>();
        uiManager.menuPanel.SetActive(false);
    }


    public void MenuBTNCLicked()
    {

        if (!InfoPanel.activeSelf)
        {
            if (!mutationPanel.hidden)
            {
                mutationPanel.HideBTNclicked();
            }
            menuButtons.SetActive(true);
            credits.SetActive(false);
            menuPanel.SetActive(true);
        }
        else
            menuPanel.SetActive(!menuPanel.activeSelf);
    }




    public void ShowCredits()
    {
        menuButtons.SetActive(false);
        credits.SetActive(true);
    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }



    public void OpenInfoPanel(MutationStat stat)
    {
        if (menuPanel.activeSelf)
            menuPanel.SetActive(false);


        if (InfoPanel.activeSelf && titleText.text == stat.name)
            InfoPanel.SetActive(false);
        else
        {
            InfoPanel.SetActive(true);

            titleText.text = stat.name;
            infoText.text = stat.infoText;
        }
    }


    public void CloseInfoPanel()
    {
        InfoPanel.SetActive(false);
    }

    public void closeMenu()
    {
        menuPanel.SetActive(false);
    }


    private void UpdateInfoText(int mutationID)
    {

    }


}
