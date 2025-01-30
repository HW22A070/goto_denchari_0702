using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManManager : MonoBehaviour
{
    //労働者データ
    public WorkManData.DataInfo workManData;

    [Header("パラメータリスト")]
    public List<Parameters> prmList = new List<Parameters>();
    [System.Serializable]
    public struct Parameters
    {
        [Header("パラメータ名")]
        public string prmName;

        [Header("パラメータオブジェクト")]
        public GameObject prmObj;
    }


    //Key：パラメータ名、Value：パラメータオブジェクトのディクショナリ
    public Dictionary<string, GameObject> prmDic = new Dictionary<string, GameObject>();

    //アイテム使用中か
    [HideInInspector] public bool isUsingItem = false;
    //アイテム使用クールタイム
    [HideInInspector] public float itemCooldownTime = 0;

    [HideInInspector] public GameObject nameUI;
    [HideInInspector] public GameObject likeUI;

    //----------------------------------------------------------

    void Start()
    {
        SetUp();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isUsingItem)
        {
            ItemCoolDown();
        }
    }

    //初期設定-------------------------------------------------
    private void SetUp()
    {
        DicAdd();
        SetDataValue();
    }

    //データ引き渡し--------------------------------------------
    private void SetDataValue()
    {
        foreach (var _parameter in prmList)
        {
            _parameter.prmObj.GetComponent<WorkManParameter>().GetDataValue(workManData.parameters[_parameter.prmName]);
        }

        this.gameObject.GetComponent<LikingManager>().GetDataValue(workManData.likingInfo);

        nameUI.GetComponent<NameUI>().DisplayNameUI(workManData.nameInfo.name);
        likeUI.GetComponent<NameUI>().DisplayLikeUI(workManData.likingInfo.likes, workManData.likingInfo.dislikes);
    }

    //Dictionaryの作成--------------------------------------------
    void DicAdd()
    {
        for (int i1 = 0; i1 < prmList.Count; i1++)
        {
            prmDic.Add(prmList[i1].prmName, prmList[i1].prmObj);
        }

        this.gameObject.GetComponent<WorkManLife>().hasAddDic = true;
    }

    //全てのパラメータの値をゼロにする---------------------------
    public void ZeroParameter()
    {
        for (int i1 = 0; i1 < prmList.Count; i1++)
        {
            prmList[i1].prmObj.GetComponent<WorkManParameter>().parameter = 0;
        }
    }

    //アイテム使用中------------------------------------------
    private void ItemCoolDown()
    {
        itemCooldownTime -= Time.deltaTime;
        if (itemCooldownTime <= 0)
        {
            isUsingItem = false;
        }
    }
}
