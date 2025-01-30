using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManHapiness : MonoBehaviour
{
    [SerializeField]
    [Header("発電スクリプト")]
    private ElectricGenerating electricGeneratingSc;

    [SerializeField]
    [Header("幸福度パラメータスクリプト")]
    private WorkManParameter hapinessParameterSc;

    [System.Serializable]
    private struct generatingEfficiency
    {
        [SerializeField]
        [Header("ストレス反応しきい値(%)")]
        [Range(0f, 100f)]
        public float threshold;

        [SerializeField]
        [Header("発電効率(%)")]
        [Range(0f, 100f)]
        public float efficiency;
    }

    [SerializeField]
    [Header("ストレス反応のしきい値と発電効率")]
    private List<generatingEfficiency> efficiencys = new List<generatingEfficiency>(); 
    
    //-----------------------------------------------
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        StressManager();
    }

    //ストレス反応管理-----------------------------
    //ストレス反応度
    int stressLevel = 0;
    void StressManager()
    {
        if(hapinessParameterSc.parameter / hapinessParameterSc.prmMax < efficiencys[stressLevel].threshold / 100)
        {
            electricGeneratingSc.hapinessGeneratingBounus = efficiencys[stressLevel].efficiency / 100;

            if(efficiencys.Count - 1 == stressLevel)
            {
                stressLevel++;
            }
        }
        else if(stressLevel > 0)
        {
            if(hapinessParameterSc.parameter / hapinessParameterSc.prmMax >= efficiencys[stressLevel - 1].threshold / 100)
            {
                stressLevel--;

                electricGeneratingSc.hapinessGeneratingBounus = efficiencys[stressLevel].efficiency / 100;
            }
        }
        else if(stressLevel == 0)
        {
            electricGeneratingSc.hapinessGeneratingBounus = 1;
        }
    }
}
