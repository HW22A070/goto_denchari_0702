using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSummonerC : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    /// <summary>
    /// アイテム
    /// </summary>
    [SerializeField, Tooltip("Summon Item"), Header("アイテムアタッチ")]
    protected GameObject _dDAP;

    /// <summary>
    /// 値段テキスト
    /// </summary>
    [SerializeField, Tooltip("値段だ"), Header("一番気に入ってるのは、")]
    private TextMeshProUGUI _priceText;


    /// <summary>
    /// オーディオソース
    /// </summary>
    private AudioSource _audioManager;

    private Image _imageCom;

    /// <summary>
    /// アイテム使用音
    /// </summary>
    [SerializeField, Header("アイテム使用音")]
    private AudioClip _soundSummonItem, _faildItem;

    protected Vector3 _positionOwnFirst;

    /// <summary>
    /// 設定クールタイム
    /// </summary>
    [SerializeField, Header("クールタイム（秒）")]
    private float _timedDefaultCooltime = 1.0f;

    /// <summary>
    /// 現在のクールタイム
    /// </summary>
    private float _timeNowCoolTime;

    /// <summary>
    /// 使えるか否か
    /// </summary>
    private bool _isAbleUse = true;

    [SerializeField]
    private GameObject _goItemPopUp;

    /// <summary>
    /// 使用中ポップ
    /// </summary>
    private ItemPopUpC _scUsingPop;

    /// <summary>
    /// 金マネージャ
    /// </summary>
    private MoneyManager _scMoneyManager;

    private int _priceItem;


    // Start is called before the first frame update
    void Start()
    {
        _imageCom = GetComponent<Image>();
        _audioManager = GameObject.Find("Chanri").GetComponent<AudioSource>();
        _positionOwnFirst = transform.position;
        //transform.localEulerAngles = GetAngle(transform.position, GameObject.Find("Main Camera").transform.position);
        _scMoneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        _priceItem = _dDAP.GetComponent<ItemManager>().PriceItem;
        _priceText.text = "$ " + _priceItem.ToString();
    }

    private void Update()
    {
        //クールタイム挙動
        if (_timeNowCoolTime > 0)
        {
            float colorValue = _timeNowCoolTime / _timedDefaultCooltime;
            colorValue = 1 - colorValue;
            _imageCom.color = new Color(colorValue, colorValue, colorValue, 255);
            _timeNowCoolTime -= Time.deltaTime;
            if (_timeNowCoolTime <= 0)
            {
                BeAbleUseItem();
            }
        }

        //transform.localEulerAngles = GetAngle(transform.position, GameObject.Find("Main Camera").transform.position);
    }

    /// <summary>
    /// アイテムが使えるようになった時の動き
    /// </summary>
    private void BeAbleUseItem()
    {
        //Debug.Log("使えるぜ！" + gameObject.name);
        _isAbleUse = true;
        _timeNowCoolTime = 0;
        _imageCom.color = Color.white;
    }

    /// <summary>
    /// クールタイムスタート
    /// </summary>
    public void StartCooltime()
    {
        //Debug.Log("使えないぜ！" + gameObject.name);
        _isAbleUse = false;
        _timeNowCoolTime = _timedDefaultCooltime;
    }

    /// <summary>
    /// ホールド判定とり
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //ポップ表示
        _scUsingPop = Instantiate(_goItemPopUp, transform.position + (transform.right * 20), quaternion.Euler(0, 0, 0)).GetComponent<ItemPopUpC>();
        _scUsingPop.PopUpText(_dDAP.GetComponent<DragDropAbleC>().SendPopMessage());
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

    /// <summary>
    /// クリック判定とり
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //アイテム生成
        if (_isAbleUse && _priceItem <= _scMoneyManager.money)
        {
            Quaternion rot = transform.rotation;
            _audioManager.PlayOneShot(_soundSummonItem);
            Instantiate(_dDAP, _positionOwnFirst, Quaternion.Euler(0, 0, 0)).GetComponent<DragDropAbleC>().GetParentSummoner(gameObject);
        }
        else
        {
            _audioManager.PlayOneShot(_faildItem);
        }

    }

    /// <summary>
    /// Own座標から見たTarget座標への向きを割り出し、angleに進む際のVector3の増分を割り出す
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public Vector3 GetAngle(Vector3 ownPos, Vector3 targetPos)
    {
        Vector3 direction = targetPos - ownPos;
        float rad = Mathf.Atan2(direction.x, direction.y);
        float angle = rad * Mathf.Rad2Deg;
        Vector3 direction2 = new Vector3(
            Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);
        return direction2 * 10;
    }
}
