using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHidePanel : MonoBehaviour
{

    [SerializeField]
    private Sprite hideBTNSprite, showBTNSprite;

    public bool hidden, moving;

    [SerializeField]
    private Vector2 openPosition;

    private Vector2 hidePosition;

    [SerializeField]
    private Image hideBTNImage;

    private Vector2 goal;

    private RectTransform rec;

    [SerializeField]
    private float speed;

    private float moveSpeed;


    [SerializeField]
    private bool mutationPanel;



    private void Awake()
    {

        rec = GetComponent<RectTransform>();
        hidePosition = rec.anchoredPosition;
        hidden = true;

        //not needed because tutorial activates the hideBBTN
        //if (mutationPanel && !SaveManager.Instance.saveData.showTutorial)
        //{
        //    HideBTNclicked();
        //}
    }



    public void HideBTNclicked()
    {
        if (!moving)
        {
            if (hidden)
            {
                goal = openPosition;
                hideBTNImage.sprite = hideBTNSprite;
                moveSpeed = speed;
                moving = true;
            }
            else
            {
                //own method so it can be used by LevelWonOverlay
                HidePanel();
            }

        }
    }


    public void HidePanel()
    {
        goal = hidePosition;
        hideBTNImage.sprite = showBTNSprite;
        moveSpeed = speed * 2;
        moving = true;
    }





    private void Update()
    {
        if (moving)
        {
            rec.anchoredPosition = Vector3.MoveTowards(rec.anchoredPosition, goal, moveSpeed * Time.deltaTime);

            if (rec.anchoredPosition == goal)
            {
                moving = false;
                if (goal == hidePosition)
                {
                    hidden = true;
                }
                else
                {
                    hidden = false;
                }
            }
        }
    }


}
