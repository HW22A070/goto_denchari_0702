using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorkManData;

public class NameCardBirthDay : MonoBehaviour
{
    [Header("数字UI")]
    [SerializeField] private GameObject numberUI;

    [Header("数字表示場所")]
    [SerializeField] private GameObject[] yearPos;
    [SerializeField] private GameObject[] monthPos;
    [SerializeField] private GameObject[] dayPos;

    //--------------------------------------------------
    void Start()
    {
        SetUp();
        SetPosNum();
    }

    void Update()
    {

    }

    //初期化---------------------------------------------

    private DataInfo workmanData;
    private GameObject[][] numPos;

    private GameObject[] yearNum;
    private GameObject[] monthNum;
    private GameObject[] dayNum;
    private GameObject[][] numbers;

    private void SetUp()
    {
        if (this.gameObject.transform.parent.gameObject.TryGetComponent<WorkManDataCreater>(out var comp1)) workmanData = comp1.workManData;

        numPos = new GameObject[][] { yearPos, monthPos, dayPos };

        yearNum = new GameObject[yearPos.Length];
        monthNum = new GameObject[monthPos.Length];
        dayNum = new GameObject[dayPos.Length];
        numbers = new GameObject[][] { yearNum, monthNum, dayNum };
    }

    //数字UI生成-----------------------------------------
    private void SetPosNum()
    {
        for (int i1 = 0; i1 < numPos.Length; i1++)
        {
            int len = numPos[i1].Length;
            for (int i2 = 0; i2 < len; i2++)
            {
                GameObject obj = Instantiate(numberUI, numPos[i1][i2].transform);
                numbers[i1][i2] = obj;
            }

            // numbers[i1].Reverse();
        }

        DisplayNum(workmanData.otherInfo.birthday.year, numbers[0]);
        DisplayNum(workmanData.otherInfo.birthday.month, numbers[1]);
        DisplayNum(workmanData.otherInfo.birthday.day, numbers[2]);
    }

    //数字表示-------------------------------------------
    private void DisplayNum(int _number, GameObject[] _numArray)
    {
        for (int i1 = 0; i1 < _numArray.Length; i1++)
        {
            int digit = _number % 10;
            if (_numArray[i1].TryGetComponent<NumberSwitcher>(out var comp1)) comp1.SwitchNumbers(digit);
            _number /= 10;
        }
    }
}
