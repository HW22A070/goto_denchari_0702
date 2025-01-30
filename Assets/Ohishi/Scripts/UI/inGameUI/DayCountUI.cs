using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCountUI : MonoBehaviour
{
    [Header("必要なスクリプトたち")]
    [SerializeField] private TimeManager timeManager;

    [Header("日数UIたち")]
    [SerializeField] private List<Image> dayCountUIs = new List<Image>();

    //-------------------------------------------
    void Start()
    {
        foreach (var _UI in dayCountUIs)
        {
            if (_UI.enabled) _UI.enabled = false;
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!dayCountUIs[timeManager.dayCount - 1].enabled)
        {
            SwitchDayCountUI(dayCountUIs[timeManager.dayCount - 1]);
        }
    }

    //日数UI切り替え-------------------------------

    //表示中のUI
    private Image displayingUI;

    private void SwitchDayCountUI(Image _UI)
    {
        if (displayingUI != null) displayingUI.enabled = false;
        displayingUI = _UI;
        displayingUI.enabled = true;
    }
}
