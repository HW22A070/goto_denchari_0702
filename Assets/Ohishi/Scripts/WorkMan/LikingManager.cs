using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemStatsNamespace;
using ItemInfomationNamespace;

public class LikingManager : MonoBehaviour
{
    //ランダムモード
    enum RandomMode
    {
        Manual,
        Semi,
        SemiUperLimit,
        All,
    }

    //好き嫌い作成用のアイテムリスト
    private ItemList itemListSc;
    [SerializeField] private ShowLikingIcon showLikingIcon;

    [Header("好き嫌い補正倍率")]
    [Tooltip("好きな物時補正(float)")]
    [SerializeField] private float likeModifire = 0;
    [Tooltip("嫌いな物時補正(float)")]
    [SerializeField] private float dislikeModifire = 0;

    [Header("好き嫌いランダムモード")]
    [SerializeField] private RandomMode randomMode;

    [Header("好き嫌い選出確率<Semi, SemiUperLimit>")]
    [Range(0, 100)]
    [SerializeField] private int percentage = 0;

    [Header("ランダム時好き嫌い個数上限<SemiUperLimit>")]
    [Tooltip("好きな物の個数上限(int)")]
    [SerializeField] private int likeMax = 0;
    [Tooltip("嫌いな物の個数上限(int)")]
    [SerializeField] private int dislikeMax = 0;

    //処理用好き嫌い個数上限配列
    private int[] likingMaxs = new int[2];

    [Header("好き嫌いリスト")]
    [Tooltip("好きな物リスト(string)")]
    [SerializeField] private List<string> likes = new List<string>();
    [Tooltip("嫌いな物リスト(string)")]
    [SerializeField] private List<string> dislikes = new List<string>();

    //処理用好き嫌い配列
    private List<string>[] likings = new List<string>[2];

    //==========後藤＝＝＝＝＝＝＝
    /// <summary>
    /// パラメータ名前
    /// </summary>
    private string[] prmNameText = new string[4];

    /// <summary>
    /// パラメータ増減値
    /// </summary>
    private float[] prmDelta = new float[4];
    //==========後藤＝＝＝＝＝＝＝

    //Unity-----------------------------------------
    void Start()
    {
        // itemListSc = GameObject.Find("ItemList").GetComponent<ItemList>();

        // likings[0] = likes;
        // likings[1] = dislikes;

        // likingMaxs[0] = likeMax;
        // likingMaxs[1] = dislikeMax;

        // LikingMake();
    }

    //好き嫌い作成------------------------------------
    void LikingMake()
    {
        switch (randomMode)
        {
            case RandomMode.Manual:
                break;
            case RandomMode.Semi:
                ListClear();
                RandomSemi();
                break;
            case RandomMode.SemiUperLimit:
                ListClear();
                RandomSemiUperLimit();
                break;
            case RandomMode.All:
                ListClear();
                RandomAll();
                break;
        }
    }

    //リストの全要素を消す------------------------------
    private void ListClear()
    {
        for (int i1 = 0; i1 < likings.Length; i1++)
        {
            likings[i1].Clear();
        }
    }

    //0~100のランダムな数値を返す------------------------
    int rnd = 0;
    private int MyRandom()
    {
        rnd = (int)(Random.value * 100);
        return rnd;
    }

    //いくつかをランダムに好き嫌いに仕分ける--------------
    private void RandomSemi()
    {
        for (int i1 = 0; i1 < itemListSc.items.Count; i1++)
        {
            if (MyRandom() <= percentage)
            {
                likings[MyRandom() % 2].Add(itemListSc.items[i1]);
            }
        }
    }

    //個数上限を定めたうえでランダムに好き嫌いを仕分ける-----
    int rnd2 = 0;
    private void RandomSemiUperLimit()
    {
        if (itemListSc.items.Count < likeMax)
        {
            Debug.LogWarning("好きの上限数がアイテム総数を超えています", this);
        }
        if (itemListSc.items.Count < dislikeMax)
        {
            Debug.LogWarning("嫌いの上限数がアイテム総数を超えています", this);
        }

        for (int i1 = 0; i1 < itemListSc.items.Count; i1++)
        {
            if (MyRandom() <= percentage)
            {
                rnd2 = MyRandom() % 2;

                if (likings[rnd2].Count < likingMaxs[rnd2])
                {
                    likings[rnd2].Add(itemListSc.items[i1]);
                }
                else
                {
                    rnd2 = (rnd2 + 1) % 2;

                    if (likings[rnd2].Count < likingMaxs[rnd2])
                    {
                        likings[rnd2].Add(itemListSc.items[i1]);
                    }
                }
            }
        }
    }

    //全てをランダムに好き嫌いに仕分ける--------------------
    private void RandomAll()
    {
        for (int i1 = 0; i1 < itemListSc.items.Count; i1++)
        {
            likings[MyRandom() % 2].Add(itemListSc.items[i1]);
        }
    }

    //アイテム効果適応-------------------------------------
    public void ApplyItemEffect(ItemInfomation itemInfo, List<ItemStats> myList, Dictionary<string, GameObject> myDic)
    {
        for (int i = 0; i < 4; i++)
        {
            prmNameText[i] = null;
            prmDelta[i] = 0;
        }
        BoredomAndAddiction boredomAndAddictionSc = this.gameObject.GetComponent<BoredomAndAddiction>();

        boredomAndAddictionSc.ApplyBoredom(itemInfo.itemName, itemInfo.boredom, itemInfo.boredomLevel);
        boredomAndAddictionSc.ApplyAddiction(itemInfo.itemName, itemInfo.addiction, itemInfo.addictionLevel);

        for (int i1 = 0; i1 < myList.Count; i1++)
        {
            WorkManParameter prmSc = myDic[myList[i1].prmName].GetComponent<WorkManParameter>();

            prmNameText[i1] = myList[i1].prmName;

            prmDelta[i1] = myList[i1].prmDelta * LikingJudge(itemInfo.itemName, myList[i1]) * boredomAndAddictionSc.BoredomJudge(itemInfo.itemName, itemInfo.boredom);
            prmSc.parameter += prmDelta[i1];
        }
    }

    //好き嫌い判定------------------------------------------
    public float LikingJudge(string myName, ItemStats myItemStats)
    {
        if (myItemStats.settings.like && likes.Contains(myName))
        {
            if (myItemStats.settings.likeAdjust)
            {
                if (myItemStats.prmDelta < 0)
                {
                    if (likeModifire > 1)
                    {
                        showLikingIcon.ShowLiking(true);
                        return 1f / likeModifire;
                    }
                }
                showLikingIcon.ShowLiking(true);
                return likeModifire;
            }
            else
            {
                return 1;
            }
        }
        if (myItemStats.settings.dislike && dislikes.Contains(myName))
        {
            if (myItemStats.settings.dislikeAdjust)
            {
                if (myItemStats.prmDelta < 0)
                {
                    if (dislikeModifire < 1)
                    {
                        showLikingIcon.ShowLiking(false);
                        return dislikeModifire + 1;
                    }
                }
                showLikingIcon.ShowLiking(false);
                return dislikeModifire;
            }
            else
            {
                return 1;
            }
        }
        if (myItemStats.settings.neutral)
        {
            if (!likes.Contains(myName) && !dislikes.Contains(myName))
            {
                return 1;
            }
        }
        return 0;
    }

    //データ受け取り----------------------------------------
    public void GetDataValue(WorkManData.LikingInfo _liking)
    {
        likes = _liking.likes;
        dislikes = _liking.dislikes;
    }

    public float[] SendPrmValue()
    {
        return prmDelta;
    }

    public string[] SendPrmNameText()
    {
        return prmNameText;
    }
}
