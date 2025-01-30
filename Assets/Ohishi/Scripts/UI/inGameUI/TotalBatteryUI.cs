using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalBatteryUI : MonoBehaviour
{
    [Header("GameManagerオブジェクト")]
    [SerializeField]
    private GameObject gameManagerObj;

    [Header("表示テキスト")]
    [SerializeField]
    private Text text;

    //表示用格納変数
    private float myBattery = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(myBattery != gameManagerObj.GetComponent<GameManager>().battery)
        {
            OnGUI();
        }
    }

    void OnGUI()
    {
        myBattery = gameManagerObj.GetComponent<GameManager>().battery;
        text.text = "total:" + myBattery.ToString();
    }
}
