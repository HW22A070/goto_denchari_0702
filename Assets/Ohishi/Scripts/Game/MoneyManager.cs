using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TimeManager timeManager;

    [Header("支給金額")]
    [SerializeField] private List<float> payments = new List<float>();

    [Header("デバッグ用：所持金")]
    public float money = 0;

    [Header("初期金持ち越し")]
    [SerializeField] private bool carryOverMoney = false;

    [Header("スコアに応じ所持金増加")]
    [SerializeField] private bool increaseMoney = false;
    [Header("必要な発電量")]
    [SerializeField] private float energyToMonetize = 0;
    [Header("増加値")]
    [SerializeField] private float energyValue = 0;


    //--------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (increaseMoney)
        {
            if (gameManager.electricity != monetizedEnergy)
            {
                IncreaceMoney();
            }
        }
    }

    //初期化-----------------------------------------
    public void Reset()
    {
        float payment;
        if (payments.Count < timeManager.dayCount)
        {
            payment = payments[payments.Count - 1];
        }
        else
        {
            payment = payments[timeManager.dayCount - 1];
        }

        if (carryOverMoney)
        {
            money += payment;
        }
        else
        {
            money = payment;
        }
    }

    //所持金増加------------------------------------

    //換算前の発電量
    private float unmonetizedEnergy = 0;
    //換算済みの発電量
    private float monetizedEnergy = 0;

    private void IncreaceMoney()
    {
        unmonetizedEnergy += gameManager.electricity - monetizedEnergy;
        monetizedEnergy += gameManager.electricity - monetizedEnergy;

        if (unmonetizedEnergy >= energyToMonetize)
        {
            float convertingEnergy = unmonetizedEnergy - unmonetizedEnergy % energyToMonetize;
            money += convertingEnergy / energyToMonetize * energyValue;
            unmonetizedEnergy -= convertingEnergy;
        }
    }

    //所持金減少------------------------------------
    /// <summary>
    /// 所持金使用
    /// </summary>
    public bool UseMoney(int price)
    {
        bool isAblePay = money >= price;
        if (isAblePay) money -= price;

        return isAblePay;
    }
}
