using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownC : MonoBehaviour
{
    private float _cooldownTime, _cooldownNow;

    [SerializeField]
    private Image _own;

    /// <summary>
    /// アイテムアイコン
    /// </summary>
    [SerializeField]
    private Sprite[] _spItems;

    [SerializeField]
    private Sprite _spNone;

    private ItemList _itemList;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _cooldownNow -= Time.deltaTime;
        _own.fillAmount = _cooldownNow / _cooldownTime;
        if (_cooldownNow <= 0) Destroy(gameObject);
    }

    public void CoolDown(float cooldownTime, string imageName)
    {
        List<string> itemlistData = GameObject.Find("ItemList").GetComponent<ItemList>().items;
        //_imgLike.gameObject.SetActive(true);
        int index = itemlistData.IndexOf(imageName);
        if (index >= 0)
        {
            _own.sprite = SetImage(index);
        }
        else _own.sprite = SetImage(-1);
        _cooldownTime = cooldownTime;
        _cooldownNow = _cooldownTime;

    }

    /// <summary>
    /// 指定のアイテム番号を表示。0未満で表示しなくする
    /// </summary>
    /// <param name="image"></param>
    /// <param name="SpriteIndex"></param>
    private Sprite SetImage(int SpriteIndex)
    {
        if (SpriteIndex >= 0) return _spItems[SpriteIndex];
        else return _spNone;
    }
}
