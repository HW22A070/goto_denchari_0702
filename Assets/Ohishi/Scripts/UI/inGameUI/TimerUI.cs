using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private TimeManager timeManager;


    [Header("テキスト")]
    [SerializeField] private TextMeshProUGUI minText;
    [SerializeField] private TextMeshProUGUI secText;


    private TextMeshProUGUI textMeshProUGUI;

    //------------------------------------------
    void Start()
    {
        textMeshProUGUI = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (displayTime != timeManager.gameTime)
        {
            DisplayTimer();
        }
    }

    //時間表示------------------------------------

    private float displayTime = 0;

    private void DisplayTimer()
    {
        displayTime = timeManager.gameTime;

        minText.text = ((int)(displayTime / 60)).ToString("d2");
        secText.text = ((int)(displayTime % 60)).ToString("d2");
    }
}
