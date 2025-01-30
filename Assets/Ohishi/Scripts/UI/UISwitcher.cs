using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class UISwitcher : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private TimeManager timeManager;

    [Header("それぞれのUI")]
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject choiceUI;
    [SerializeField] private GameObject playUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject resultUI;

    //--------------------------------------------
    void Start()
    {
        //表示されているUIがあれば非表示にする
        if (startUI != null) if (startUI.activeSelf) startUI.SetActive(false);
        if (choiceUI != null) if (choiceUI.activeSelf) choiceUI.SetActive(false);
        if (playUI != null) if (playUI.activeSelf) playUI.SetActive(false);
        if (pauseUI != null) if (pauseUI.activeSelf) pauseUI.SetActive(false);
        if (resultUI != null) if (resultUI.activeSelf) resultUI.SetActive(false);
    }

    void Update()
    {
        switch (timeManager.gameState)
        {
            case GameState.start:
                if (!startUI.activeSelf) SwitchDisplay(startUI);
                break;

            case GameState.choice:
                if (!choiceUI.activeSelf) SwitchDisplay(choiceUI);
                break;

            case GameState.play:
                if (!playUI.activeSelf) SwitchDisplay(playUI);
                break;

            case GameState.pause:
                if (!pauseUI.activeSelf) SwitchDisplay(pauseUI);
                break;

            case GameState.result:
                if (!resultUI.activeSelf) SwitchDisplay(resultUI);
                break;
        }
    }

    void FixedUpdate()
    {

    }

    //UI切り替え-------------------------------

    //表示中UI
    private GameObject displayingUI;

    private void SwitchDisplay(GameObject _targetUI)
    {
        if (displayingUI != null) displayingUI.SetActive(false);
        displayingUI = _targetUI;
        displayingUI.SetActive(true);
    }

    //暗転開始---------------------------------
    public void BlackoutStart()
    {
        timeManager.gameState = GameState.blackout;
    }

    //暗転した---------------------------------
    public void Blackouted()
    {
        SwitchDisplay(playUI);
    }
}
