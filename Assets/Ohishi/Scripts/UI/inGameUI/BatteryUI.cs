using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    [SerializeField]
    [Header("ゲームマネージャースクリプト")]
    private GameManager gameManagerSc;

    [SerializeField]
    [Header("黄色ブロックリスト(左から)")]
    private List<GameObject> yellowBrocks = new List<GameObject>();

    [SerializeField]
    [Header("緑ブロックリスト(左から)")]
    private List<GameObject> greenBrocks = new List<GameObject>();

    [Header("過充電UI")]
    [SerializeField] private List<Image> overchargeUIs = new List<Image>();

    [Header("終業ボタン")]
    [SerializeField] private GameObject finishButton;

    [Header("半透明化")]
    [SerializeField] private GameObject buttonCover;

    //過充電UIのキュー
    private Queue<Image> hideOverchargeUIs = new Queue<Image>();

    //表示させるブロックのキュー
    private Queue<GameObject> displayBrocks = new Queue<GameObject>();

    //非表示させるブロックのキュー
    private Queue<GameObject> hideBrocks = new Queue<GameObject>();

    //キューに入れた回数
    private int inQueueCount = 0;

    //ブロック一個当たりの値量
    private float brockValue = 0;

    private bool hasDisplayedOutLine = false;

    [SerializeField]
    private BatteryOutLineC _scOutline;

    //---------------------------------------
    void Start()
    {

    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        AnimetionBattery();

        if (gameManagerSc.electricity >= quota)
        {
            if (!finishButton.activeSelf) finishButton.SetActive(true);
            if (buttonCover.activeSelf) buttonCover.SetActive(false);
        }
    }

    //次に表示させるブロックたちをキューに入れる---
    void InQueue(int i)
    {
        if (i == 0)
        {
            for (int i1 = 0; i1 < yellowBrocks.Count; i1++)
            {
                displayBrocks.Enqueue(yellowBrocks[i1]);
            }
        }
        else
        {
            for (int i1 = 0; i1 < greenBrocks.Count; i1++)
            {
                //Debug.Log("RRR!" + i.ToString());
                _scOutline.ChangeOutLine(i);
                displayBrocks.Enqueue(greenBrocks[i1]);
            }
        }

        if (i >= 2)
        {
            hideOverchargeUIs.Dequeue().enabled = true;
        }

        inQueueCount++;
    }

    //バッテリーのアニメーション--------------

    private float quota = 0;

    void AnimetionBattery()
    {
        if (displayBrocks.Count > (int)(yellowBrocks.Count - gameManagerSc.electricity / brockValue) + (inQueueCount - 1) * yellowBrocks.Count)
        {
            GameObject obj = displayBrocks.Dequeue();
            obj.GetComponent<Image>().enabled = true;

            if (inQueueCount > 1)
            {
                hideBrocks.Enqueue(obj);
            }
        }

        if (displayBrocks.Count == 0)
        {
            if (inQueueCount > 1)
            {
                for (int i1 = 0; i1 < yellowBrocks.Count; i1++)
                {
                    GameObject obj = hideBrocks.Dequeue();
                    obj.GetComponent<Image>().enabled = false;
                }
            }

            InQueue(inQueueCount);
        }
    }

    //初期化-----------------------------------------
    public void UIReset()
    {
        //ノルマの設定
        quota += gameManagerSc.quota;

        //ブロックの初期化
        List<GameObject>[] lists = new List<GameObject>[2];
        lists[0] = yellowBrocks;
        lists[1] = greenBrocks;

        for (int i1 = 0; i1 < lists.Length; i1++)
        {
            for (int i2 = 0; i2 < lists[i1].Count; i2++)
            {
                lists[i1][i2].GetComponent<Image>().enabled = false;
            }
        }

        displayBrocks.Clear();
        hideBrocks.Clear();

        brockValue = (quota - gameManagerSc.battery) / yellowBrocks.Count;
        inQueueCount = 0;
        InQueue(inQueueCount);

        //その他UIの初期化
        if (finishButton.activeSelf) finishButton.SetActive(false);
        if (!buttonCover.activeSelf) buttonCover.SetActive(true);

        //_scOutline.SetNowLevel();

        hideOverchargeUIs.Clear();
        foreach (var _value in overchargeUIs)
        {
            if (_value.enabled) _value.enabled = false;
            hideOverchargeUIs.Enqueue(_value);
        }
    }
}
