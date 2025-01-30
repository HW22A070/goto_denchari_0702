using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorkManData;

public class WorkManDataCreater : MonoBehaviour
{
    //必要なスクリプトたち
    [HideInInspector] public NameCreater nameCreater;
    [HideInInspector] public ItemList itemList;

    [Header("労働者モデル")]
    [SerializeField] private GameObject workmanModel;

    [Header("パラメータリスト")]
    [SerializeField] private List<Parameters> parameters = new List<Parameters>();
    [System.Serializable]
    private struct Parameters
    {
        [Header("パラメータ名")]
        public string name;
        [Header("最小値")]
        public float rndMin;
        [Header("最大値")]
        public float rndMax;
    }

    [Header("好き嫌い作成モード")]
    [SerializeField] private LikingCreateMode likingCreateMode;
    private enum LikingCreateMode
    {
        Manual,         //手入力
        Semi,           //確率により仕分ける
        All,            //全てを仕分ける
    }

    [Header("好き嫌い個数上限")]
    [SerializeField] private int likeCountMax = 0;
    [SerializeField] private int dislikeCountMax = 0;

    [Header("好き嫌い選出確率（％）<Semi>"), Range(0f, 100f)]
    [SerializeField] private float likingCreateProbability = 0;

    [Header("年齢幅")]
    [SerializeField] private int ageMin = 0;
    [SerializeField] private int ageMax = 0;

    //-------------------------------------------
    void Awake()
    {
        Setup();
    }

    void Start()
    {
        CreateWorkManData();
    }

    void OnEnable()
    {

    }

    void Update()
    {

    }

    //初期設定------------------------------------

    public DataInfo workManData;   //労働者データ
    private List<string>[] likings; //好き嫌いリスト
    private int[] likingCountMaxs;  //好き嫌い個数上限

    private void Setup()
    {
        likings = new List<string>[2] { workManData.likingInfo.likes, workManData.likingInfo.dislikes };
        likingCountMaxs = new int[2] { likeCountMax, dislikeCountMax };
    }

    //労働者データ作成------------------------------
    private void CreateWorkManData()
    {
        workManData.workmanModel = workmanModel;
        string[] nameData = nameCreater.CreateName();
        workManData.nameInfo.name = nameData[0];
        workManData.nameInfo.furigana = nameData[1];
        CreateParameter();
        CreateLiking();
        CreateEnployeeNum();
        CreateBirthday();
    }

    //パラメータデータ作成-----------------------------
    private void CreateParameter()
    {
        workManData.parameters = new Dictionary<string, float>();
        foreach (var _parameter in parameters)
        {
            float value = Random.Range(_parameter.rndMin, _parameter.rndMax);
            value = (float)(int)value / 1;
            workManData.parameters.Add(_parameter.name, value);
        }
    }

    //好き嫌いデータ作成---------------------------------
    private void CreateLiking()
    {
        switch (likingCreateMode)
        {
            case LikingCreateMode.Semi:
                Semi();
                break;

            case LikingCreateMode.All:
                All();
                break;
        }

        void Semi()
        {
            foreach (var _item in itemList.items)
            {
                float rnd = Random.Range(0f, 100f);
                if (rnd <= likingCreateProbability)
                {
                    SplitLiking(_item);
                }
            }
        }

        void All()
        {
            foreach (var _item in itemList.items)
            {
                SplitLiking(_item);
            }
        }

        //好きか嫌いかに仕分ける
        void SplitLiking(string _item)
        {
            int rnd = Random.Range(0, 2);
            if (likings[rnd].Count < likingCountMaxs[rnd])
            {
                likings[rnd].Add(_item);
            }
        }
    }

    //社員番号作成---------------------------------------
    private void CreateEnployeeNum()
    {
        int rnd = Random.Range(0, 1000);
        workManData.otherInfo.employeeNum = rnd;
    }

    //誕生年月日作成----------------------------------------
    private void CreateBirthday()
    {
        //誕生年作成
        int nowYear = System.DateTime.Now.Year;
        int yearMin = nowYear - ageMax;
        int yearMax = nowYear - ageMin;
        workManData.otherInfo.birthday.year = Random.Range(yearMin, yearMax);

        //誕生月作成
        workManData.otherInfo.birthday.month = Random.Range(1, 13);

        //誕生日作成
        workManData.otherInfo.birthday.day = Random.Range(1, 32);
    }
}
