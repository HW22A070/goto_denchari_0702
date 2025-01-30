using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManLife : MonoBehaviour
{
    [SerializeField]
    [Header("体力パラメータ")]
    private WorkManParameter prmLife;

    private GameManager gameManagerSc;

    [HideInInspector]
    public bool hasAddDic = false;

    [SerializeField, Header("紫にする対象")]
    private DeathMaterialC _body;

    [SerializeField, Header("HPがこの値を下回ると肌が紫になります")]
    private int _ValueHPUnderPurpleChange;

    //------------------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (hasAddDic)
        {
            LifeManagement();
        }
    }

    //体力管理----------------------------------------------
    void LifeManagement()
    {
        if (prmLife.parameter <= 0)
        {
            this.gameObject.GetComponent<WorkManManager>().ZeroParameter();
            gameManagerSc = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManagerSc.OnDeath(this.gameObject);
        }

        _body.SetDeathPurple(prmLife.parameter < _ValueHPUnderPurpleChange);
    }
}