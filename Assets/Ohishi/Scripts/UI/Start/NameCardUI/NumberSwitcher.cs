using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSwitcher : MonoBehaviour
{
    [Header("数字UI(0~9)")]
    [SerializeField] private Image[] numbers;

    //---------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    //数字切り替え-----------------------------------
    public void SwitchNumbers(int _num)
    {
        numbers[_num].enabled = true;
    }
}
