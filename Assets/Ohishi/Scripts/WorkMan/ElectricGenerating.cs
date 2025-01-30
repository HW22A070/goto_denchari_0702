using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGenerating : MonoBehaviour
{
    //GameManagerのコピー
    private GameManager gameManagerSc;

    [Header("発電量")]
    [Range(0f, 100f)]
    [SerializeField] private float elGeneratingCapacity = 0;

    [Header("発電にかかる時間")]
    [SerializeField] private float generatingTime = 0;
    //発電時間補正
    [HideInInspector] public float generatingSpeed = 1;

    //発電ボーナス
    [HideInInspector] public float generatingBonus = 1;
    //発電効率(幸福度)
    [HideInInspector] public float hapinessGeneratingBounus = 1;

    [Header("幸福度パラメータ名")]
    [SerializeField] private string hapinessPrmName = "";

    //幸福度減少補正
    [HideInInspector] public float hapinessAdjustment = 1;

    [System.Serializable]
    private struct ParameterEffect
    {
        [SerializeField]
        [Header("関与パラメータ名")]
        public string prmName;

        [SerializeField]
        [Header("パラメータ増減量")]
        public float prmDeltaValue;
    }

    [SerializeField]
    [Header("関与パラメータリスト")]
    private List<ParameterEffect> prmEffectList = new List<ParameterEffect>();

    //WorkManManagerのprmDicのコピー-----------------------
    private Dictionary<string, GameObject> myPrmDic = new Dictionary<string, GameObject>();

    void Start()
    {
        gameManagerSc = GameObject.Find("GameManager").GetComponent<GameManager>();
        myPrmDic = this.gameObject.GetComponent<WorkManManager>().prmDic;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Generating();
    }

    //発電----------------------------------------------------

    private float electricity = 0;
    private float time1 = 0;

    void Generating()
    {
        time1 += Time.deltaTime;

        if (time1 >= generatingTime * (1 / generatingSpeed))
        {
            electricity = elGeneratingCapacity * hapinessGeneratingBounus * generatingBonus;
            gameManagerSc.electricity += electricity;

            for (int i1 = 0; i1 < prmEffectList.Count; i1++)
            {
                if (prmEffectList[i1].prmName == hapinessPrmName)
                {
                    myPrmDic[prmEffectList[i1].prmName].GetComponent<WorkManParameter>().parameter += prmEffectList[i1].prmDeltaValue * hapinessAdjustment * this.gameObject.GetComponent<BoredomAndAddiction>().AddictionAdjustment();
                }
                else
                {
                    myPrmDic[prmEffectList[i1].prmName].GetComponent<WorkManParameter>().parameter += prmEffectList[i1].prmDeltaValue;
                }
            }

            time1 = 0;
        }
    }

    //基礎発電量取得--------------------------------------
    public float GetGeneratingCapacity()
    {
        return elGeneratingCapacity;
    }

    //発電量取得------------------------------------------
    public float GetElectrisity()
    {
        return electricity;
    }
}
