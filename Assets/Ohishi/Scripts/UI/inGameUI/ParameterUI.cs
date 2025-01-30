using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterUI : MonoBehaviour
{
    [HideInInspector] public WorkManParameter workManParameterSc;

    [Header("ブロックを生成する親オブジェクト")]
    [SerializeField] private GameObject prmBrockObjParent;

    [Header("パラメーターブロック")]
    [SerializeField] private GameObject prmBrockObj;

    [Header("外枠UI")]
    [SerializeField] private RectTransform outlineUI;

    [Header("ブロック間隔幅")]
    [SerializeField] private float brockIntervalWidth = 0;

    [Header("ブロック一個あたりの数値量")]
    [SerializeField] private float prmBrockValue = 0f;

    [Header("アニメーション速度")]
    [Range(0f, 1f)]
    [SerializeField] private float prmAnimetionSpeed = 0;

    //パラメータ保存変数
    private float myParameter = 0f;

    //ブロック個数
    private int brockCount = 0;

    //表示中のブロックのスタック
    private Stack<GameObject> displayBrocks = new Stack<GameObject>();
    //非表示中のブロックのスタック
    private Stack<GameObject> hideBrocks = new Stack<GameObject>();

    //ブロック生成済みか
    private bool hasCreatedBrock = false;

    //------------------------------------------------------
    void Start()
    {
        // PrmUIStart();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (hasCreatedBrock)
        {
            AnimetionParameter();
        }

        if (workManParameterSc.prmMax != 0 && !hasCreatedBrock)
        {
            brockCount = (int)(workManParameterSc.prmMax / prmBrockValue);
            CreateParameterBrock(prmBrockObj);
            myParameter = workManParameterSc.parameter;
        }
    }

    //一日開始処理--------------------------------------------
    public void PrmUIStart(WorkManParameter myWorkManParameterSc)
    {
        UIReset();
        hasCreatedBrock = false;
        workManParameterSc = myWorkManParameterSc;
    }

    //ブロック生成--------------------------------------------
    void CreateParameterBrock(GameObject obj)
    {
        //ブロックの横幅
        float brockWidth = prmBrockObj.GetComponent<RectTransform>().rect.width;
        //配置ポジション
        Vector2 createPos = new Vector2(brockWidth, 0);
        //外枠の横幅
        float outlineWidth = 110;

        outlineWidth -= (brockWidth + brockIntervalWidth) * brockCount;

        outlineUI.offsetMax = new Vector2(-outlineWidth, 0);

        for (int i1 = 0; i1 < brockCount; i1++)
        {
            GameObject cloneObj = Instantiate(obj, prmBrockObjParent.transform);
            RectTransform cloneRect = cloneObj.GetComponent<RectTransform>();

            cloneRect.sizeDelta = new Vector2(brockWidth, cloneRect.rect.height);
            cloneRect.anchoredPosition = createPos;
            createPos += new Vector2(brockWidth + brockIntervalWidth, 0);

            displayBrocks.Push(cloneObj);
        }

        hasCreatedBrock = true;
    }

    //パラメータのアニメーション--------------------------------

    //アニメーションさせるブロックオブジェクト
    GameObject animetionBrock = null;
    void AnimetionParameter()
    {
        myParameter = Mathf.Lerp(myParameter, workManParameterSc.parameter, prmAnimetionSpeed);

        if (displayBrocks.Count > myParameter / prmBrockValue + 1 || workManParameterSc == null)
        {
            if (displayBrocks.Count != 0)
            {
                animetionBrock = displayBrocks.Pop();
            }

            animetionBrock.GetComponent<Image>().enabled = false;

            if (animetionBrock != null)
            {
                hideBrocks.Push(animetionBrock);
            }
        }
        if (displayBrocks.Count + 1 < myParameter / prmBrockValue)
        {
            if (hideBrocks.Count != 0)
            {
                animetionBrock = hideBrocks.Pop();
            }
            //Debug.Log(animetionBrock.name);
            animetionBrock.GetComponent<Image>().enabled = true;

            if (animetionBrock != null)
            {
                displayBrocks.Push(animetionBrock);
            }
        }
    }

    //初期化--------------------------------------------
    void UIReset()
    {
        Stack<GameObject>[] stacks = new Stack<GameObject>[2];
        stacks[0] = displayBrocks;
        stacks[1] = hideBrocks;

        for (int i1 = 0; i1 < stacks.Length; i1++)
        {
            int count = stacks[i1].Count;
            for (int i2 = 0; i2 < count; i2++)
            {
                GameObject obj = stacks[i1].Pop();
                Destroy(obj);
            }
        }
    }
}
