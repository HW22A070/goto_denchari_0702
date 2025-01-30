using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WorkManData;

public class GameManager : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private BatteryUI batteryUISc;
    [SerializeField] private NameCardManager nameCardManager;

    [Header("日毎のノルマ")]
    //入力用ノルマリスト
    public List<float> quotaOfDays = new List<float>();
    //処理用ノルマキュー
    private Queue<float> quotas = new Queue<float>();
    //その日のノルマ
    [HideInInspector] public float quota = 0;
    //全体のノルマ
    [HideInInspector] public float totalQuota = 0;
    //参照用全体のノルマ
    public static float finalQuota = 0;

    [Header("労働者スポーン位置リスト")]
    [SerializeField] private List<Transform> workManSpawns = new List<Transform>();
    //労働者リスト
    [HideInInspector] public List<GameObject> workMans = new List<GameObject>();
    //労働者情報リスト
    [HideInInspector] public List<GameObject> workManDatas = new List<GameObject>();

    [Header("墓")]
    [SerializeField] private GameObject graveObj;

    [Header("日毎の発電速度補正")]
    [Range(1f, 10f)]
    [SerializeField] private List<float> generatingSpeeds = new List<float>();

    [Header("パラメータUI")]
    [SerializeField] private List<GameObject> parameterUIs = new List<GameObject>();

    [Header("名前UI")]
    [SerializeField] private List<GameObject> nameUIs = new List<GameObject>();

    [Header("好物UI")]
    [SerializeField] private List<GameObject> likeUIs = new List<GameObject>();

    [Header("死亡時ペナルティパラメータ名")]
    [SerializeField] private string deathPenaltyPrmName = "";
    [Header("死亡時ペナルティ値")]
    [SerializeField] private float deathPenalty = 0f;

    //1日分の発電量
    [HideInInspector] public float electricity = 0f;
    //総発電量
    [HideInInspector] public float battery = 0f;
    //最終的な発電量
    public static float finalBattery = 0f;

    //一日開始処理が終了したか
    private bool hasStartedOneDay = false;

    //＝＝＝＝＝＝＝後藤追加＝＝＝＝＝＝＝
    /// <summary>
    /// オーディオソース
    /// </summary>
    private AudioSource _audioManager;

    /// <summary>
    /// アイテム使用音
    /// </summary>
    [SerializeField]
    private AudioClip _soundDead, _soundElec;
    //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

    //--------------------------------------------------
    void Start()
    {
        //後藤追加
        _audioManager = GameObject.Find("Chanri").GetComponent<AudioSource>();

        SetQuota();
        _audioManager = GameObject.Find("Chanri").GetComponent<AudioSource>();
    }

    void Update()
    {
        //デバッグ。escでタイトル戻る
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    void FixedUpdate()
    {
        if (hasStartedOneDay)
        {
            AliveCheck();
        }
    }

    //ノルマ作成----------------------------------------
    private void SetQuota()
    {
        foreach (var _quota in quotaOfDays)
        {
            quotas.Enqueue(_quota);
            totalQuota += _quota;
        }

        finalQuota = totalQuota;
    }

    //一日開始------------------------------------------
    public void StartOneDay()
    {
        DestroyDeshabariPops();
        quota = quotas.Dequeue();

        for (int i1 = 0; i1 < workManSpawns.Count; i1++)
        {
            //労働者生成
            DataInfo workmanData = workManDatas[i1].GetComponent<WorkManDataCreater>().workManData;
            GameObject workMan = Instantiate(workmanData.workmanModel, workManSpawns[i1].transform.position, workManSpawns[i1].transform.rotation);
            workMan.GetComponent<WorkManManager>().workManData = workmanData;
            workMans.Add(workMan);

            workMan.GetComponent<ElectricGenerating>().generatingSpeed = generatingSpeeds[timeManager.dayCount - 1];

            //パラメータUI適応
            for (int i2 = 0; i2 < parameterUIs[i1].transform.childCount; i2++)
            {
                GameObject parameterUI = parameterUIs[i1].transform.GetChild(i2).gameObject;
                GameObject parameterObj = workMan.GetComponent<WorkManManager>().prmList[i2].prmObj;

                ParameterUI parameterUISc = parameterUI.GetComponent<ParameterUI>();
                parameterUISc.PrmUIStart(parameterObj.GetComponent<WorkManParameter>());
            }

            //名前UI適応
            workMan.GetComponent<WorkManManager>().nameUI = nameUIs[i1];
            //好物UI適応
            workMan.GetComponent<WorkManManager>().likeUI = likeUIs[i1];

        }

        batteryUISc.UIReset();

        hasStartedOneDay = true;

        GameObject.Find("Chanri").GetComponent<ChariSEC>().ChariSEStart();
    }

    //一日終了------------------------------------------
    public void EndOneDay()
    {
        DestroyDeshabariPops();
        GameObject.Find("Chanri").GetComponent<ChariSEC>().ChariSEStop();

        Charging();

        for (int i1 = 0; i1 < workMans.Count; i1++)
        {
            Destroy(workMans[i1]);
        }
        workMans.Clear();

        foreach (var _obj in workManDatas)
        {
            Destroy(_obj);
        }
        workManDatas.Clear();

        foreach (var _obj in graves)
        {
            Destroy(_obj);
        }
        graves.Clear();

        nameCardManager.CreateNameCard();

        hasStartedOneDay = false;
    }

    //一日の発電分を全体の発電量に足す--------------------
    private List<GameObject> graves = new List<GameObject>();

    public void Charging()
    {
        //＝＝＝＝＝＝＝後藤追加＝＝＝＝＝＝＝
        _audioManager.PlayOneShot(_soundElec);
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        battery += electricity;
        finalBattery = battery;
        electricity = 0;
    }

    //特定の労働者が死んだ際の他の労働者の処理------------
    public void OnDeath(GameObject deadWorkMan)
    {
        workMans.Remove(deadWorkMan);

        //＝＝＝＝＝＝＝後藤追加＝＝＝＝＝＝＝
        _audioManager.PlayOneShot(_soundDead);
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

        Destroy(deadWorkMan);
        graves.Add(Instantiate(graveObj, deadWorkMan.transform.position, deadWorkMan.transform.rotation));

        for (int i1 = 0; i1 < workMans.Count; i1++)
        {
            workMans[i1].GetComponent<WorkManManager>().prmDic[deathPenaltyPrmName].GetComponent<WorkManParameter>().parameter -= deathPenalty;
        }
    }

    //労働者の生存確認-----------------------------------
    void AliveCheck()
    {
        if (workMans.Count == 0)
        {
            hasStartedOneDay = false;
            timeManager.DayEnd();
        }
    }

    /// <summary>
    /// でしゃばりアイテム説明ポップを全部潰す
    /// </summary>
    private void DestroyDeshabariPops()
    {
        GameObject[] tag = GameObject.FindGameObjectsWithTag("Pop");
        foreach (GameObject tagO in tag)
        {
            Destroy(tagO);
        }
    }
}
