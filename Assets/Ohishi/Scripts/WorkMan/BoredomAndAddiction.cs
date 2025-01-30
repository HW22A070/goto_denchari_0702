using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoredomAndAddiction : MonoBehaviour
{
    [SerializeField]
    [Header("飽き(ON,OFF)")]
    private bool boredomActive = false;
    [SerializeField]
    [Header("中毒(ON,OFF)")]
    private bool addictionActive = false;

    [Header("飽き===============================================")]
    [SerializeField]
    [Header("飽きのしきい値")]
    private int boredomThreshold = 0;

    [SerializeField]
    [Header("飽きの上限値")]
    private int boredomMax = 0;

    [SerializeField]
    [Header("飽きた際の補正倍率(%)")]
    [Range(0f, 100f)]
    private float boredomModifire = 0;

    //アイテム毎の飽き具合を格納するディクショナリ
    private Dictionary<string, int> boredomItems = new Dictionary<string, int>();

    [Header("中毒===============================================")]
    [SerializeField]
    [Header("中毒のしきい値")]
    private int addictionThreshold = 0;

    [SerializeField]
    [Header("中毒の上限値")]
    private int addictionMax = 0;

    [SerializeField]
    [Header("中毒時の幸福度バフ倍率(%)")]
    [Range(100f, 1000f)]
    private float addictionUpModifire = 0;

    [SerializeField]
    [Header("中毒時の幸福度デバフ倍率(%)")]
    [Range(1f, 100f)]
    private float addictionDownModifire = 0;

    [SerializeField]
    [Header("中毒症状回復回数")]
    private int addictionRecovery = 0;

    //アイテム毎の中毒具合を格納するディクショナリ
    private Dictionary<string, int> addictionItems = new Dictionary<string, int>();
    //アイテム毎の中毒発症しているかを格納するディクショナリ
    [HideInInspector] public Dictionary<string, bool> isAddictionItems = new Dictionary<string, bool>();

    //--------------------------------------------------
    void Start()
    {

    }


    void Update()
    {

    }

    //飽き適応------------------------------------------
    public void ApplyBoredom(string itemName, bool active, int boredomLevel)
    {
        if (boredomActive)
        {
            if (active)
            {
                if (boredomItems.ContainsKey(itemName))
                {
                    if (boredomItems[itemName] < boredomMax)
                    {
                        if (boredomItems[itemName] + boredomLevel > boredomMax)
                        {
                            boredomItems[itemName] = boredomMax;
                        }
                        else
                        {
                            boredomItems[itemName] += boredomLevel;
                        }
                    }
                }
                else
                {
                    boredomItems.Add(itemName, boredomLevel);
                }
            }

            DicValueDecrease(itemName);
        }
    }

    //飽き判定------------------------------------------
    public float BoredomJudge(string itemName, bool active)
    {
        if (boredomActive && active)
        {
            if (boredomItems[itemName] >= boredomThreshold)
            {
                return boredomModifire / 100;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 1;
        }
    }

    //中毒適応--------------------------------------------
    public void ApplyAddiction(string itemName, bool active, int addictionLevel)
    {
        if (addictionActive)
        {
            if (active)
            {
                if (addictionItems.ContainsKey(itemName))
                {
                    if (addictionItems[itemName] < addictionMax)
                    {
                        if (addictionItems[itemName] + addictionLevel > addictionMax)
                        {
                            addictionItems[itemName] = addictionMax;
                        }
                        else if (addictionItems[itemName] < 0)
                        {
                            addictionItems[itemName] = addictionLevel;
                        }
                        else
                        {
                            addictionItems[itemName] += addictionLevel;
                        }
                    }
                }
                else
                {
                    addictionItems.Add(itemName, addictionLevel);
                    isAddictionItems.Add(itemName, false);
                }

                if (addictionItems[itemName] >= addictionThreshold)
                {
                    isAddictionItems[itemName] = true;
                    isAddiction = true;
                }
            }

            DicValueDecrease(itemName);
        }
    }

    //中毒補正-----------------------------------------------
    public float AddictionAdjustment()
    {
        float modifire = 1;
        List<int> valueList = addictionItems.Values.ToList();

        for (int i1 = 0; i1 < valueList.Count; i1++)
        {
            if (valueList[i1] < 0)
            {
                modifire = addictionDownModifire / 100;
            }
            else if (valueList[i1] >= addictionThreshold)
            {
                modifire = addictionUpModifire / 100;
            }
        }

        return modifire;
    }

    //飽き値、中毒値減少--------------------------------------
    void DicValueDecrease(string itemName)
    {
        // Debug.Log("飽き================");

        List<string> keyList = boredomItems.Keys.ToList();

        for (int i1 = 0; i1 < keyList.Count; i1++)
        {
            if (keyList[i1] != itemName)
            {
                if (boredomItems[keyList[i1]] > 0)
                {
                    boredomItems[keyList[i1]]--;
                }
            }

            // Debug.Log($"Item:{keyList[i1]} Value:{boredomItems[keyList[i1]]}");
        }

        // Debug.Log("中毒================");

        keyList = addictionItems.Keys.ToList();

        for (int i1 = 0; i1 < keyList.Count; i1++)
        {
            if (keyList[i1] != itemName)
            {
                if (addictionItems[keyList[i1]] > -1 * addictionRecovery)
                {
                    addictionItems[keyList[i1]]--;
                }

                if (addictionItems[keyList[i1]] <= -1 * addictionRecovery)
                {
                    addictionItems[keyList[i1]] = 0;
                    isAddictionItems[keyList[i1]] = false;
                }
            }

            // Debug.Log($"Item:{keyList[i1]} Value:{addictionItems[keyList[i1]]}");
        }

        AddictionJudge();
    }

    //中毒判定--------------------------------

    [HideInInspector] public bool isAddiction = false;

    private void AddictionJudge()
    {
        isAddiction = false;

        foreach (var _kvp in isAddictionItems)
        {
            if (_kvp.Value)
            {
                isAddiction = true;
                break;
            }
        }
    }
}
