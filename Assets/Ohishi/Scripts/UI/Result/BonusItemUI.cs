using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusItemUI : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private ResultUI resultUI;

    [Header("UI演出待機時間")]
    [SerializeField] private float waitTime = 0;

    [Header("色付きゲージUI")]
    [SerializeField] private Image colorUI;
    [Header("ゲージの動く速度")]
    [SerializeField] private float moveSpeed = 0;

    [Header("クエスチョンUI")]
    [SerializeField] private Image shadowImage;

    [Header("ボーナスアイテム")]
    [SerializeField] private List<BonusItem> bonusItems = new List<BonusItem>();

    [System.Serializable]
    private struct BonusItem
    {
        public GameObject bonusItem;
        public Image bonusItemImage;
    }

    //--------------------------------------------
    void Start()
    {
        SetQueue();
    }

    void OnEnable()
    {
        if (timeManager.dayCount > timeManager.limitDay)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            HideBonusItemUI();
            DisplayBonusItemUI();
            if (quotaOfDaysQueue.Count > 0) nextQuotaValue += quotaOfDaysQueue.Dequeue();
        }
        colorUI.fillAmount = 0;
        displayBar = false;
        activeBonusItem = false;
        time1 = 0;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (displayBar)
        {
            DisplayBarUI();
        }
        else
        {
            WaitForBarUIShow();
        }
    }

    //処理用キューの作成--------------------------------------------

    private Queue<float> quotaOfDaysQueue = new Queue<float>();

    private void SetQueue()
    {
        for (int i1 = 0; i1 < gameManager.quotaOfDays.Count; i1++)
        {
            quotaOfDaysQueue.Enqueue(gameManager.quotaOfDays[i1]);
        }
        nextQuotaValue = quotaOfDaysQueue.Dequeue();
    }

    //バーUI表示待機時間--------------------------------------------

    private bool displayBar = false;
    private float time1 = 0;

    private void WaitForBarUIShow()
    {
        time1 += Time.deltaTime;

        if (time1 >= waitTime)
        {
            displayBar = true;
        }
    }

    //ゲージUI表示---------------------------------------------------

    private uint thisAnimatingFlag = 0b_0010;
    private float score = 0;
    [SerializeField] private float nextQuotaValue = 0;

    private void DisplayBarUI()
    {
        if (!activeBonusItem)
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

            colorUI.fillAmount = score / nextQuotaValue;

            if (score + 0.5 >= nextQuotaValue)
            {
                ActiveBonusItem();
            }

            bool finishAnimation = (score + 0.5 >= gameManager.battery) || (score >= nextQuotaValue);
            if (finishAnimation)
            {
                if (isAnimating)
                {
                    resultUI.isAnimatingFlag &= ~thisAnimatingFlag;
                }
            }
        }
    }

    //ボーナスアイテムUI表示------------------------------------------

    private bool activeBonusItem = false;
    [SerializeField] private List<int> hasActivedBonusItemIndexs = new List<int>();
    private int rnd = -1;

    private void DisplayBonusItemUI()
    {
        for (int i1 = 0; i1 < 100; i1++)
        {
            bool flag1 = true;

            rnd = Random.Range(0, bonusItems.Count);
            for (int i2 = 0; i2 < hasActivedBonusItemIndexs.Count; i2++)
            {
                if (rnd == hasActivedBonusItemIndexs[i2]) flag1 = false;
            }

            if (flag1)
            {
                bonusItems[rnd].bonusItemImage.enabled = true;
                hasActivedBonusItemIndexs.Add(rnd);
                break;
            }
        }
    }

    //ボーナスアイテムUI非表示---------------------------------------
    private void HideBonusItemUI()
    {
        if (rnd != -1)
        {
            bonusItems[rnd].bonusItemImage.enabled = false;
            shadowImage.enabled = true;
        }
    }

    //ボーナスアイテムアクティブ化
    private void ActiveBonusItem()
    {
        bonusItems[rnd].bonusItem.SetActive(true);
        shadowImage.enabled = false;
        activeBonusItem = true;
    }
}
