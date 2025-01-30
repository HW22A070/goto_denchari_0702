using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class TimeManager : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UISwitcher uiSwitcher;

    public GameState gameState;

    [Header("1日の時間")]
    [SerializeField] private float limitTime = 0;

    [Header("最大日数")]
    public int limitDay = 0;

    [HideInInspector] public int dayCount = 1;

    [Header("暗転UI")]
    [SerializeField] private Image blackoutUI;

    [Header("暗転速度")]
    [SerializeField] private float blackoutSpeed = 0;

    //------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        switch (gameState)
        {
            case GameState.blackout:
                Blackout();
                break;

            case GameState.play:
                if (flag1) flag1 = false;
                TimeManagement();
                break;
        }
    }

    //タイマー開始--------------------------------
    public void TimerStart()
    {
        gameTime = limitTime;
        gameState = GameState.play;
    }

    //時間管理----------------------------------

    [HideInInspector] public float gameTime = 0;

    private void TimeManagement()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            DayEnd();
        }
    }

    //1日終了-----------------------------------
    public void DayEnd()
    {
        gameState = GameState.result;
        gameManager.EndOneDay();
        dayCount++;
        gameTime = 0;
    }

    //ポーズ------------------------------------
    public void SwichPause()
    {
        switch (gameState)
        {
            case GameState.play:
                gameState = GameState.pause;
                Time.timeScale = 0;
                break;

            case GameState.pause:
                gameState = GameState.play;
                Time.timeScale = 1;
                break;
        }
    }

    //暗転------------------------------------

    private Color blackoutColor;
    [SerializeField] private bool flag1 = false;

    private void Blackout()
    {
        if (!flag1) blackoutColor.a = Mathf.Lerp(blackoutColor.a, 1, blackoutSpeed);
        else blackoutColor.a = Mathf.Lerp(blackoutColor.a, 0, blackoutSpeed);
        blackoutUI.color = blackoutColor;

        if (blackoutColor.a >= 0.99 && !flag1)
        {
            uiSwitcher.Blackouted();
            gameManager.StartOneDay();
            flag1 = true;
        }

        if (blackoutColor.a <= 0.01 && flag1) TimerStart();
    }
}
