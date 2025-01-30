using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using WorkManData;

public class NameCardEmployeeNum : MonoBehaviour
{
    [Header("表示場所")]
    [SerializeField] private List<GameObject> displayPos = new List<GameObject>();

    [Header("数字UI")]
    [SerializeField] private GameObject numberUI;

    //-------------------------------------------------
    void Start()
    {
        SetUp();
        DisplayEmployeeNum();
    }

    void Update()
    {

    }

    //初期化--------------------------------------------

    private DataInfo workmanData;

    private void SetUp()
    {
        if (this.gameObject.transform.parent.TryGetComponent<WorkManDataCreater>(out var comp1)) workmanData = comp1.workManData;
    }

    //社員番号表示--------------------------------------
    private void DisplayEmployeeNum()
    {
        List<NumberSwitcher> numbers = new List<NumberSwitcher>();  //生成した数字リスト

        //数字UI生成
        foreach (var _pos in displayPos)
        {
            GameObject obj = Instantiate(numberUI, _pos.transform);
            if (obj.TryGetComponent<NumberSwitcher>(out var comp1)) numbers.Add(comp1);
        }

        for (int i1 = numbers.Count, _number = workmanData.otherInfo.employeeNum; 0 < i1; i1--)
        {
            int digit = 0;
            if (_number != 0)
            {
                digit = _number % 10;
            }

            numbers[i1 - 1].SwitchNumbers(digit);

            _number /= 10;
        }
    }
}
