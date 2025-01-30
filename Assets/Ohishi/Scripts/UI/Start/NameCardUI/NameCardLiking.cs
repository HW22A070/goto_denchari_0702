using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameCardLiking : MonoBehaviour
{
    [Header("好き嫌いテキスト")]
    [SerializeField] private TextMeshProUGUI likeText;
    [SerializeField] private TextMeshProUGUI dislikeText;

    [Header("好き嫌いアイコンポジション")]
    [SerializeField] private GameObject likeIconPos;
    [SerializeField] private GameObject dislikeIconPos;

    [Header("表示テキスト")]
    [SerializeField] List<ItemName> itemNames = new List<ItemName>();
    [System.Serializable]
    private struct ItemName
    {
        public string nameEng;
        public string nameJp;
        public GameObject icon;
    }

    [Header("好き嫌いアイテムアイコン")]
    [SerializeField] private List<Image> likeItems = new List<Image>();
    [SerializeField] private List<Image> dislikeItems = new List<Image>();

    //--------------------------------------------
    void Start()
    {
        SetUp();
        DisplayLiking();
    }

    void Update()
    {

    }

    //初期化--------------------------------------

    private WorkManData.DataInfo workmanData;
    private Dictionary<string, string> itemData = new Dictionary<string, string>();
    private Dictionary<string, GameObject> itemIcons = new Dictionary<string, GameObject>();

    private void SetUp()
    {
        likeText.text = "";
        dislikeText.text = "";

        if (this.gameObject.transform.parent.TryGetComponent<WorkManDataCreater>(out var comp1)) workmanData = comp1.workManData;

        foreach (var _itemName in itemNames)
        {
            itemData.Add(_itemName.nameEng, _itemName.nameJp);
            itemIcons.Add(_itemName.nameEng, _itemName.icon);
        }
    }

    //好き嫌い表示---------------------------------
    private void DisplayLiking()
    {
        foreach (var _like in workmanData.likingInfo.likes)
        {
            likeText.text += itemData[_like] + " ";
            itemIcons[_like].transform.SetParent(likeIconPos.transform);
            itemIcons[_like].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            itemIcons[_like].GetComponent<Image>().enabled = true;
        }
        foreach (var _dislike in workmanData.likingInfo.dislikes)
        {
            dislikeText.text += itemData[_dislike] + " ";
            itemIcons[_dislike].transform.SetParent(dislikeIconPos.transform);
            itemIcons[_dislike].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            itemIcons[_dislike].GetComponent<Image>().enabled = true;
        }
    }
}
