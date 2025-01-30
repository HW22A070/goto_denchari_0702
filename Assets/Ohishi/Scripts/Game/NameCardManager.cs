using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameCardManager : MonoBehaviour
{
    [Header("必要なスクリプトたち")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private NameCreater nameCreater;
    [SerializeField] private ItemList itemList;

    [Header("生成するポジションリスト")]
    [SerializeField] private List<GameObject> createPositions = new List<GameObject>();

    [Header("社員証プレファブリスト")]
    [SerializeField] private List<NameCardOrigins> nameCardOrigins = new List<NameCardOrigins>();
    [System.Serializable]
    private struct NameCardOrigins
    {
        [Header("社員証プレファブ")]
        public GameObject nameCardOrigin;
        [Header("確率"), Range(0f, 100f)]
        public float probability;
    }

    //------------------------------------------------
    void Start()
    {
        FormatInputData();
        CreateNameCard();
    }

    void Update()
    {

    }

    //入力データ整形-----------------------------------

    private float totalProbability = 0; //確率の合計値

    private void FormatInputData()
    {
        //社員証プレファブリストを確率の降順にソートする
        nameCardOrigins.Sort((x, y) => x.probability.CompareTo(y.probability));
        nameCardOrigins.Reverse();

        //確率の合計値を算出する
        foreach (var _st in nameCardOrigins)
        {
            totalProbability += _st.probability;
        }
    }

    //労働者情報生成---------------------------------
    public void CreateNameCard()
    {
        foreach (var _pos in createPositions)
        {
            GameObject createObj = nameCardOrigins[0].nameCardOrigin;   //生成する労働者情報

            //生成する労働者情報の選出
            float rnd = MyRandomF(totalProbability);
            float probabilityOffset = 0;
            foreach (var _st in nameCardOrigins)
            {
                probabilityOffset += _st.probability;
                if (rnd <= totalProbability - probabilityOffset)
                {
                    createObj = _st.nameCardOrigin;
                }
                else break;
            }

            //UI生成
            GameObject obj = Instantiate(createObj, _pos.transform);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            WorkManDataCreater workManDataCreater = obj.GetComponent<WorkManDataCreater>();
            workManDataCreater.nameCreater = nameCreater;
            workManDataCreater.itemList = itemList;
            gameManager.workManDatas.Add(obj);
        }
    }

    //ランダム値生成(flaot)---------------------------
    private float MyRandomF(float _max)
    {
        float rnd = UnityEngine.Random.Range(0f, _max);
        return rnd;
    }
}
