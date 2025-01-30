using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameCardParameter : MonoBehaviour
{
    [Header("表示するパラメータ")]
    [SerializeField] NameCardBrock lifePrm;
    [SerializeField] NameCardBrock hapinessPrm;

    //------------------------------------------------
    void Start()
    {
        SetUp();
        DisplayParameter();
    }

    void Update()
    {

    }

    //初期化-------------------------------------------

    private WorkManData.DataInfo workmanData;

    private void SetUp()
    {
        if (this.gameObject.transform.parent.TryGetComponent<WorkManDataCreater>(out var comp1)) workmanData = comp1.workManData;
    }

    //パラメータUI表示----------------------------------
    private void DisplayParameter()
    {
        lifePrm.DisplayBrock((int)workmanData.parameters["Health"] / 10);
        hapinessPrm.DisplayBrock((int)workmanData.parameters["Hapiness"] / 10);
    }
}
