using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterUIOld : MonoBehaviour
{
    [Header("表示パラメータスクリプト")]
    [SerializeField]
    public WorkManParameter parameterSc;

    private Slider slider;

    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 100;
    }

    void Update()
    {
        slider.value = parameterSc.parameter;
    }
}
