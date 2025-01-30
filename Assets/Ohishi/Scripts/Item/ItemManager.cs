using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemStatsNamespace;
using ItemInfomationNamespace;

namespace ItemStatsNamespace
{
    [Serializable]
    public struct ItemStats
    {
        [Header("関与パラメータ名")]
        public string prmName;
        [Header("パラメータ増減量")]
        public float prmDelta;

        [Header("詳細設定")]
        public Settings settings;
    }

    [Serializable]
    public struct Settings
    {
        [Header("アイテム効果適応対象")]
        public bool like;
        public bool neutral;
        public bool dislike;
        [Header("好き嫌い補正対象")]
        public bool likeAdjust;
        public bool dislikeAdjust;
    }
}

namespace ItemInfomationNamespace
{
    [Serializable]
    public struct ItemInfomation
    {
        [Header("アイテム名")]
        public string itemName;

        [Header("アイテムクールタイム")]
        public float cooldownTime;

        [Header("飽き、中毒対象")]
        public bool boredom;
        public bool addiction;

        [Header("飽きやすさ")]
        [Range(0, 10)]
        public int boredomLevel;

        [Header("中毒進行度")]
        [Range(0, 10)]
        public int addictionLevel;



    }
}

public class ItemManager : MonoBehaviour
{
    public ItemInfomation itemInfo = new ItemInfomation();

    [Header("アイテム効果")]
    public List<ItemStats> itemStats = new List<ItemStats>();

    [Header("価格")]
    [Range(0, 5000)]
    public int PriceItem;

    [Header("アイテムカテゴリ"), SerializeField]
    private ItemKind _itemKind;

    /// <summary>
    /// アイテムカテゴリ
    /// </summary>
    private enum ItemKind
    {
        Food,
        Drink,
        Ohter
    }


    //Unity-----------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    //アイテム使用----------------------------
    public void UseItem(GameObject workManObj)
    {
        WorkManManager workManManager = workManObj.GetComponent<WorkManManager>();
        workManManager.itemCooldownTime = itemInfo.cooldownTime;
        workManManager.isUsingItem = true;
        workManObj.GetComponent<LikingManager>().ApplyItemEffect(itemInfo, itemStats, workManObj.GetComponent<WorkManManager>().prmDic);
        workManObj.GetComponent<PlayerAnimC>().UseItemAnimation((int)_itemKind);
    }
}
