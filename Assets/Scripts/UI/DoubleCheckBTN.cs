using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleCheckBTN : MonoBehaviour
{


    [SerializeField]
    private GameObject checkBoxes;

    [SerializeField]
    private Text originalText;

    [SerializeField]
    private Button noBTN;

    private bool mouseIsOverSelf, mouseisOveryes;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ActivateCheckbox);
        noBTN.onClick.AddListener(DeActivateCheckbox);

    }


    private void ActivateCheckbox()
    {
        checkBoxes.SetActive(true);
        originalText.enabled = false;
    }

    private void DeActivateCheckbox()
    {
        checkBoxes.SetActive(false);
        originalText.enabled = true;
    }


    public void PointerEnterSelf()
    {
        mouseIsOverSelf = true;
    }
    public void PointerExitSelf()
    {
        mouseIsOverSelf = false;
    }

    public void PointerEnterYes()
    {
        mouseisOveryes = true;
    }
    public void PointerExitYes()
    {
        mouseisOveryes = false;
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!mouseisOveryes && !mouseIsOverSelf)
                DeActivateCheckbox();
        }
    }

}
 




