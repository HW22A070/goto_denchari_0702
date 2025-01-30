using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using WorkManData;

public class NameCardName : MonoBehaviour
{
    [Header("テキスト")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI furiganaText;

    //-------------------------------------------
    void Start()
    {
        SetUp();
        DisplayName();
    }

    void Update()
    {

    }

    //初期化--------------------------------------

    private DataInfo workmanData;  //労働者データ

    private void SetUp()
    {
        if (this.gameObject.transform.parent.TryGetComponent<WorkManDataCreater>(out var comp1)) workmanData = comp1.workManData;
    }

    //名前表示-------------------------------------
    private void DisplayName()
    {
        nameText.text = workmanData.nameInfo.name;
        furiganaText.text = workmanData.nameInfo.furigana;
    }
}
