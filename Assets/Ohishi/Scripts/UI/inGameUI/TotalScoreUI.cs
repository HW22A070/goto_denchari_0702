using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreUI : MonoBehaviour
{
    [Header("必要なスクリプトたち")]
    [SerializeField] private GameManager gameManager;

    [Header("色付きバー")]
    [SerializeField] private Image colorBar;
    [Header("バーの動く速度")]
    [SerializeField] private float moveSpeed = 0;

    [Header("ノルマUIたち")]
    [SerializeField] private List<GameObject> quotaUIs = new List<GameObject>();

    //-------------------------------------------
    void Start()
    {
        SetQueue();
    }

    void OnEnable()
    {
        SetQuotaUI();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        AnimateBarUI();
    }

    //処理用キューの作成--------------------------------------------

    private Queue<GameObject> quotaUIsQueue = new Queue<GameObject>();
    private Queue<float> quotaOfDaysQueue = new Queue<float>();

    private void SetQueue()
    {
        for (int i1 = 0; i1 < quotaUIs.Count; i1++)
        {
            quotaUIsQueue.Enqueue(quotaUIs[i1]);
        }
        nextQuotaUI = quotaUIsQueue.Dequeue();

        for (int i1 = 0; i1 < gameManager.quotaOfDays.Count; i1++)
        {
            quotaOfDaysQueue.Enqueue(gameManager.quotaOfDays[i1]);
        }
        nextQuotaValue = quotaOfDaysQueue.Dequeue();
    }

    //ノルマUIの配置------------------------------------------------
    private void SetQuotaUI()
    {
        float barHeight = colorBar.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        float quotaValue = 0;

        for (int i1 = 0; i1 < quotaUIs.Count; i1++)
        {
            quotaValue += gameManager.quotaOfDays[i1];
            float setPosY = Mathf.Lerp(0, barHeight, quotaValue / gameManager.totalQuota);
            quotaUIs[i1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, setPosY);
        }
    }

    //バーUIのアニメーション--------------------------------

    private float score = 0;
    private GameObject nextQuotaUI;
    private float nextQuotaValue = 0;

    private void AnimateBarUI()
    {
        score = Mathf.Lerp(score, gameManager.battery + gameManager.electricity, moveSpeed);
        colorBar.fillAmount = score / gameManager.totalQuota;

        if (score >= nextQuotaValue)
        {
            nextQuotaUI.GetComponent<QuotaUI>().Coloring();
            if (quotaUIsQueue.Count > 0) nextQuotaUI = quotaUIsQueue.Dequeue();
            if (quotaOfDaysQueue.Count > 0) nextQuotaValue += quotaOfDaysQueue.Dequeue();
        }
    }
}
