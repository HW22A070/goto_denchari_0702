using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ResultUI resultUI;

    [Header("UI表示待機時間")]
    [SerializeField] private float waitTime = 0;

    [Header("バーUI")]
    [SerializeField] private Image progressBarColor;
    [Header("バーの動く速度")]
    [SerializeField] private float moveSpeed = 0;

    [Header("ノルマUI")]
    [SerializeField] private List<GameObject> quotaUIs = new List<GameObject>();

    //--------------------------------------------------------
    void Start()
    {
        SetQueue();
        SetQuotaUI();
    }

    void OnEnable()
    {
        time1 = 0;
        animate = false;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (animate)
        {
            AnimateBarUI();
        }
        else
        {
            WaitForBarUIShow();
        }
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

    //バーUI表示待機時間--------------------------------------------

    private bool animate = false;
    private float time1 = 0;

    private void WaitForBarUIShow()
    {
        time1 += Time.deltaTime;

        if (time1 >= waitTime)
        {
            animate = true;
        }
    }

    //バーUI表示---------------------------------------------------

    private uint thisAnimatingFlag = 0b_0001;
    private float score = 0;
    private GameObject nextQuotaUI;
    private float nextQuotaValue = 0;

    private void AnimateBarUI()
    {
        bool isAnimating = (resultUI.isAnimatingFlag & thisAnimatingFlag) != 0;
        if (isAnimating)
        {
            score = Mathf.Lerp(score, gameManager.battery, moveSpeed);
        }
        else
        {
            score = gameManager.battery;
        }

        progressBarColor.fillAmount = score / gameManager.totalQuota;

        if (score >= nextQuotaValue)
        {
            nextQuotaUI.GetComponent<QuotaUI>().Coloring();
            if (quotaUIsQueue.Count > 0) nextQuotaUI = quotaUIsQueue.Dequeue();
            if (quotaOfDaysQueue.Count > 0) nextQuotaValue += quotaOfDaysQueue.Dequeue();
        }

        if (score + 0.5 >= gameManager.battery)
        {
            if (isAnimating)
            {
                resultUI.isAnimatingFlag &= ~thisAnimatingFlag;
            }
        }
    }

    //ノルマUIの配置------------------------------------------------

    [Header("UIの幅")]
    [SerializeField] private float progressBarWidth = 0;

    private void SetQuotaUI()
    {
        float quotaValue = 0;
        for (int i1 = 0; i1 < quotaUIs.Count; i1++)
        {
            quotaValue += gameManager.quotaOfDays[i1];
            float setPosX = Mathf.Lerp(0, progressBarWidth, quotaValue / gameManager.totalQuota);
            quotaUIs[i1].GetComponent<RectTransform>().anchoredPosition = new Vector2(setPosX, 0);
        }
    }

}
