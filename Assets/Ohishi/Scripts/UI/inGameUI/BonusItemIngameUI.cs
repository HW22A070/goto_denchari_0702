using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusItemIngameUI : MonoBehaviour
{
    [Header("ボーナスアイテム")]
    [SerializeField] private List<BonusItems> bonusItems = new List<BonusItems>();
    [System.Serializable]
    private struct BonusItems
    {
        [Header("オブジェクト")]
        public GameObject bonusItemObj;
        [Header("UI")]
        public GameObject bonusItemUI;
    }

    [Header("表示ポジション")]
    [SerializeField] private List<GameObject> displayPos = new List<GameObject>();

    //----------------------------------------

    //処理用キュー
    private Queue<GameObject> displayPosQueue = new Queue<GameObject>();

    void Start()
    {
        foreach (var _value in displayPos)
        {
            displayPosQueue.Enqueue(_value);
        }
    }

    void OnEnable()
    {
        Display();
    }

    void Update()
    {

    }

    //UI表示---------------------------------
    private void Display()
    {
        foreach (var _value in bonusItems)
        {
            if (_value.bonusItemObj.activeSelf)
            {
                if (!_value.bonusItemUI.GetComponent<Image>().enabled)
                {
                    GameObject UI = _value.bonusItemUI;
                    GameObject obj = displayPosQueue.Dequeue();

                    UI.gameObject.transform.SetParent(obj.transform);
                    UI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    UI.GetComponent<Image>().enabled = true;
                }
            }
        }
    }
}
