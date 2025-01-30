using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Going_Day going_Day;

    [Header("日数")]
    [SerializeField] TextMeshProUGUI dayCountText;

    public uint isAnimatingFlag = 0;

    [SerializeField]
    private GameObject _clickNavi;

    //-----------------------------------------
    void Start()
    {

    }

    void OnEnable()
    {
        isAnimatingFlag = 0b_0011;
        dayCountText.text = (timeManager.dayCount - 1).ToString();
    }

    void Update()
    {
        if (isAnimatingFlag == 0b_0000)
        {
            _clickNavi.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isAnimatingFlag == 0b_0000)
            {
                if (timeManager.dayCount > timeManager.limitDay)
                {
                    _clickNavi.SetActive(false);
                    going_Day.NextScene();
                }
                else
                {
                    timeManager.gameState = GameState.start;
                }
            }
            else
            {
                isAnimatingFlag = 0b_0000;
            }
        }
    }
}
