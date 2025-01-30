using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItemSubGenerator : BonusItemCoreC
{
    //ゲームマネージャースクリプト
    private GameManager gameManagerSc;

    [SerializeField]
    [Header("発電量")]
    private float generatingCapacity = 0;

    [SerializeField]
    [Header("発電にかかる時間")]
    private float generatingTime = 0;

    //-------------------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Generating();
    }

    void OnEnable()
    {
        if (gameManagerSc == null)
        {
            gameManagerSc = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    //発電----------------------------------------------------
    private float time1 = 0;
    void Generating()
    {
        time1 += Time.deltaTime;

        if (time1 >= generatingTime)
        {
            gameManagerSc.electricity += generatingCapacity;
            time1 = 0;
        }
    }
}
