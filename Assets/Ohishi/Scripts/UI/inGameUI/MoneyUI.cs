using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private MoneyManager moneyManager;

    private TextMeshProUGUI textMeshProUGUI;

    //--------------------------------------------------------
    void Start()
    {
        textMeshProUGUI = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (displayMoney != moneyManager.money)
        {
            DisplayMoney();
        }
    }

    //お金表示--------------------------------------------------

    private float displayMoney = 0;

    private void DisplayMoney()
    {
        displayMoney = moneyManager.money;
        if (displayMoney <= 0)
        {
            textMeshProUGUI.text = "0";
        }
        else
        {
            textMeshProUGUI.text = (displayMoney.ToString("#,#"));
        }
    }
}
