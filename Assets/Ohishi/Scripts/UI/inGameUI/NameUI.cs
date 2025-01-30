using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NameUI : MonoBehaviour
{

    [Header("テキストUI")]
    [SerializeField] private TextMeshProUGUI text;

    /// <summary>
    /// アイテムアイコン
    /// </summary>
    [SerializeField]
    private Sprite[] _spItems;

    [SerializeField]
    private Sprite _spNone;

    [SerializeField]
    private Image _imgLike, _imgDislike;
    private ItemList _itemList;

    //---------------------------------------------
    void Start()
    {
        _itemList = GameObject.Find("ItemList").GetComponent<ItemList>();
    }

    void Update()
    {

    }

    //名前表示------------------------------------
    public void DisplayNameUI(string _name)
    {
        text.text = _name;
    }

    public void DisplayLikeUI(List<string> likes, List<string> unlikes)
    {
        List<string> itemlistData = _itemList.items;
        //好物があれば実行
        if (likes.Count > 0)
        {
            //_imgLike.gameObject.SetActive(true);
            foreach (string like in likes)
            {
                int index = itemlistData.IndexOf(like);
                if (index >= 0)
                {
                    SetImage(_imgLike, index);
                }
            }
        }
        else
        {
            SetImage(_imgLike, -1);
            //_imgLike.gameObject.SetActive(false);
        }
        if (unlikes.Count > 0)
        {
            //_imgDislike.gameObject.SetActive(true);
            foreach (string unlike in unlikes)
            {
                int index = itemlistData.IndexOf(unlike);
                if (index >= 0)
                {
                    SetImage(_imgDislike, index);
                }
            }
        }
        else
        {
            SetImage(_imgDislike, -1);
            //_imgDislike.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 指定のアイテム番号を表示。0未満で表示しなくする
    /// </summary>
    /// <param name="image"></param>
    /// <param name="SpriteIndex"></param>
    private void SetImage(Image image, int SpriteIndex)
    {
        if (SpriteIndex >= 0) image.sprite = _spItems[SpriteIndex];
        else image.sprite = _spNone;
    }
}
