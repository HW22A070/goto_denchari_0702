using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreValueTransfer : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private ResultChecker resultChecker;

    //---------------------------------------------
    void Start()
    {
        ValueTransfer();
    }

    void Update()
    {

    }

    //変数引き渡し------------------------------------
    private void ValueTransfer()
    {
        resultChecker.score = GameManager.finalBattery;
        //resultChecker.quota = GameManager.finalQuota;
    }
}
