using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManParameter : MonoBehaviour
{
    [SerializeField]
    [Header("最大値のランダム上限")]
    private float prmRndMax = 0;

    [SerializeField]
    [Header("最大値のランダム下限")]
    private float prmRndMin = 0;

    // [HideInInspector]
    public float prmMax = 0;

    [Header("初期値(%)")]
    [SerializeField]
    [Range(0f, 100f)]
    private float prmStarting = 0;

    [Header("デバッグ用:パラメータ")]
    public float parameter = 0;

    //-----------------------------------------------------
    void Start()
    {
        // prmMax = CreatePrmMax(prmRndMax, prmRndMin);
        // parameter = prmMax * (prmStarting / 100);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Adjustment();
    }

    //パラメータ最大値作成---------------------------------
    float CreatePrmMax(float rndMax, float rndMin)
    {
        return (float)((int)Random.Range(rndMax, rndMin + 1) / 1 * 1);
    }

    //パラメータを範囲内に調整する--------------------------
    void Adjustment()
    {
        if (parameter > prmMax)
        {
            parameter = prmMax;
        }
        if (parameter < 0)
        {
            parameter = 0;
        }
    }

    //データ受け取り------------------------------------
    public void GetDataValue(float _value)
    {
        prmMax = _value;
        parameter = prmMax * (prmStarting / 100);
    }
}
