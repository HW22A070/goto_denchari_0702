using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItemChangeGeear : BonusItemCoreC
{
    //ゲームマネージャースクリプト
    private GameManager gameManagerSc;

    [SerializeField]
    [Header("発電ボーナス倍率(%)")]
    [Range(0f, 100f)]
    private float generatingBonusPercent = 0;

    //-----------------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    void OnEnable()
    {
        gameManagerSc = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i1 = 0; i1 < gameManagerSc.workMans.Count; i1++)
        {
            gameManagerSc.workMans[i1].GetComponent<ElectricGenerating>().generatingBonus = (1 + (generatingBonusPercent / 100));
        }
    }

    void OnDisable()
    {
        for (int i1 = 0; i1 < gameManagerSc.workMans.Count; i1++)
        {
            gameManagerSc.workMans[i1].GetComponent<ElectricGenerating>().generatingBonus = 1;
        }
    }
}
