using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BonusItemCoreC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    protected GameObject _goItemPopUp;

    /// <summary>
    /// 使用中ポップ
    /// </summary>
    protected ItemPopUpC _scUsingPop;

    [SerializeField]
    protected string _itemNameJP;

    [SerializeField]
    protected string _itemEffectJP;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ホールド判定とり
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //ポップ表示
        _scUsingPop = Instantiate(_goItemPopUp, transform.position + (transform.right * 20), Quaternion.Euler(0, 0, 0)).GetComponent<ItemPopUpC>();
        _scUsingPop.PopUpText("<color=#FFEE00>" + _itemNameJP + "</color>　<color=#88FFFF>効果：" + _itemEffectJP + " <color=#88FFFF>(持続中)</color>");
        //Debug.Log("エンター");
    }

    /// <summary>
    /// ホールド離脱とり
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_scUsingPop != null)
        {
            Destroy(_scUsingPop.gameObject);
        }
        //Debug.Log("離脱");
    }
}
