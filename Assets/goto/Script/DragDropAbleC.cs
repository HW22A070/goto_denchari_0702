using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragDropAbleC : MonoBehaviour
{
    /// <summary>
    /// is it Dragging
    /// </summary>
    private bool _isDragging;

    /// <summary>
    /// DropRay Setting
    /// </summary>
    private Ray _dropRay;

    /// <summary>
    /// Own To TriggerObject HitSetting
    /// </summary>
    private RaycastHit _ownToTriggerRayHit;

    /// <summary>
    /// Cricked Object Data
    /// </summary>
    private GameObject _foundObject;

    /// <summary>
    /// 労働者たち
    /// </summary>
    private GameObject[] _Workmans;

    /// <summary>
    /// Last Dropped Object
    /// </summary>
    private GameObject _lastDroppedObject;

    /// <summary>
    /// Item's Last position
    /// </summary>
    private Vector3 _lastOwnPosition;
    private PlayerC _targetObjScript;

    /// <summary>
    /// ItemNumber
    /// </summary>
    private int _itemNumber;

    /// <summary>
    /// カメラオブジェクト
    /// </summary>
    private GameObject _camera;

    /// <summary>
    /// 親サモナー
    /// </summary>
    private ItemSummonerC _summonerParent;

    /// <summary>
    /// アイテムマネージャ
    /// </summary>
    private ItemManager _scItemManager;

    /// <summary>
    /// 金マネージャ
    /// </summary>
    private MoneyManager _scMoneyManager;

    /// <summary>
    /// カメラ場所
    /// </summary>
    private Vector3 _cameraPos;

    /// <summary>
    /// オーディオソース
    /// </summary>
    private AudioSource _audioManager;



    /// <summary>
    /// アイテム使用音
    /// </summary>
    [SerializeField, Header("アイテム使用音")]
    private AudioClip _soundUseItem, _soundDestroyItem, _soundCursor;

    [SerializeField, Header("上昇値UI")]
    private UpValueEffC _scUpVaEfC;

    [SerializeField, Header("クールダウンUI")]
    private CoolDownC _PrfbCooldown;

    [SerializeField, Header("クールダウンUI表示座標オフセット")]
    private Vector3 _posCooldownUIOfset = new Vector3(0, 3.5f, 0);

    private bool _isCursor;


    [SerializeField, Header("アイテムの日本語名")]
    private string _nameJP;


    // Start is called before the first frame update
    void Start()
    {

        _Workmans = GameObject.FindGameObjectsWithTag("Player");

        _audioManager = GameObject.Find("Chanri").GetComponent<AudioSource>();

        //自分のアイテムマネージャー取得
        _scItemManager = GetComponent<ItemManager>();
        //Debug.Log(_scItemManager);

        _scMoneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetMousePosition();

        _cameraPos = GameObject.Find("Main Camera").transform.position;
        _dropRay = new Ray(_cameraPos, transform.position - _cameraPos);

        //カーソル合わせ
        if (Physics.Raycast(_dropRay.origin, _dropRay.direction, out _ownToTriggerRayHit, Mathf.Infinity, 128))
        {
            _foundObject = _ownToTriggerRayHit.collider.gameObject;
            if (!_foundObject.GetComponent<WorkManManager>().isUsingItem)
            {
                IsCursor(_foundObject);
                if (!_isCursor)
                {
                    _audioManager.PlayOneShot(_soundCursor);
                }
            }
            else
            {
                NotIsCursor();
            }
        }
        else
        {
            NotIsCursor();
        }


        if (Input.GetMouseButtonUp(0))
        {

            //プレイヤーの上にドロップ
            if (_isCursor)
            {
                int price = _scItemManager.PriceItem;

                _foundObject = _ownToTriggerRayHit.collider.gameObject;
                //使用中か確認
                if (!_foundObject.GetComponent<WorkManManager>().isUsingItem)
                {
                    //金払って実行
                    //所持金参照
                    if (_scMoneyManager.UseMoney(price))
                    {
                        _audioManager.PlayOneShot(_soundUseItem);

                        //アイテムマネージャーに、落とされた労働者の情報を渡す
                        _scItemManager.UseItem(_foundObject);

                        _summonerParent.StartCooltime();
                        //SendtextMessage(_foundObject);
                        SetCoolDown(_foundObject);

                        NotIsCursor();

                        Destroy(gameObject);
                    }
                    else
                    {
                        //金ないので使えず
                        NotAbleUse();
                    }

                }
                else
                {
                    //口の中にものが入ってるので使えず
                    NotAbleUse();
                }
            }

            //虚空にドロップ
            else
            {
                NotAbleUse();
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (_isCursor)
        {
            transform.localEulerAngles += new Vector3(0, 4, 0);
        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    /// <summary>
    /// アイテム使用中の人にカーソル合わせた時の挙動
    /// </summary>
    private void NotIsCursor()
    {
        foreach (GameObject workmanOut in _Workmans)
        {
            if (workmanOut != null)
                workmanOut.GetComponent<PlayerOutlineManagerC>().SetOutlineOff();
        }
        _isCursor = false;
    }


    /// <summary>
    /// アイテム使える人にカーソル合わせた時の挙動
    /// </summary>
    private void IsCursor(GameObject workmanGO)
    {
        if (workmanGO != null)
        {
            workmanGO.GetComponent<PlayerOutlineManagerC>().SetOutlineOn(GetParamUp());
        }
        _isCursor = true;
    }

    /// <summary>
    /// アイテムが使えんかったときの処理
    /// </summary>
    public void NotAbleUse()
    {
        _audioManager.PlayOneShot(_soundDestroyItem);
    }

    /// <summary>
    /// 親サモナー取得
    /// </summary>
    /// <param name="parent"></param>
    public void GetParentSummoner(GameObject parent)
    {
        _summonerParent = parent.GetComponent<ItemSummonerC>();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 3;
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        return target;
    }

    private void SendtextMessage(GameObject foundObj)
    {
        LikingManager liking = foundObj.GetComponent<LikingManager>();
        float[] prmDelta = liking.SendPrmValue();
        string[] prmNameText = liking.SendPrmNameText();

        Instantiate(_scUpVaEfC, foundObj.transform.position + _posCooldownUIOfset, Quaternion.Euler(0, 90, 0))
            .SetText(prmDelta, prmNameText, _scItemManager.PriceItem);
    }


    /// <summary>
    /// クールタイムのバーを表示＆セットアップ
    /// </summary>
    /// <param name="foundObj"></param>
    private void SetCoolDown(GameObject foundObj)
    {
        Instantiate(_PrfbCooldown, foundObj.transform.position + _posCooldownUIOfset, Quaternion.Euler(0, 90, 0))
            .CoolDown(_scItemManager.itemInfo.cooldownTime, _scItemManager.itemInfo.itemName);
    }

    /// <summary>
    /// ポップテキスト作成
    /// </summary>
    /// <returns></returns>
    public string SendPopMessage()
    {
        bool[] isPrmUps = GetParamUp();
        string[] namePrm = new string[2] { "体力 ", "幸福 " };
        string textPrm = "効果：";

        for (int i = 0; i < isPrmUps.Length; i++)
        {
            if (isPrmUps[i]) textPrm += namePrm[i];
        }

        string textPrice = "";
        _scMoneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        if (GetComponent<ItemManager>().PriceItem < _scMoneyManager.money)
        {
            //買える
            textPrice = "";
        }
        else
        {
            //高い
            textPrice = "<color=#CC0000>お金が足りない</color>";
        }


        return "<color=#FFEE00>" + GetName() + "</color>　" + textPrm + "　" + textPrice;
    }

    private string GetName()
    {
        return _nameJP;
    }

    public bool[] GetParamUp()
    {
        ItemManager scItemManager = GetComponent<ItemManager>();
        int prmValue = scItemManager.itemStats.Count;
        bool[] isParamUps = new bool[2];
        for (int i = 0; i < prmValue; i++)
        {
            if (scItemManager.itemStats[i].prmName == "Health" && scItemManager.itemStats[i].prmDelta > 0) isParamUps[0] = true;
            if (scItemManager.itemStats[i].prmName == "Hapiness" && scItemManager.itemStats[i].prmDelta > 0) isParamUps[1] = true;
        }
        return isParamUps;
    }
}