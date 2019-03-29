using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour 
{
    private Text moneyText;

    private void Awake()
    {
        moneyText = GetComponent<Text>();

    }



    private void OnEnable()
    {
        GameManager.Instance.OnMoneyChanged += SetText;
        SetText(GameManager.Instance.SaveData.money);        
    }

    private void OnDisable()
    {
        GameManager.Instance.OnMoneyChanged -= SetText;
    }



    private void SetText(float money)
    {
        moneyText.text = NumberConverter.NumberToString(money, showDecimal:false);
    }

}
