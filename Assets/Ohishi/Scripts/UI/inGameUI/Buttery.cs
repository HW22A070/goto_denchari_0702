using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttery : MonoBehaviour
{
    [Header("表示パラメータスクリプト")]
    [SerializeField]
    private GameManager gameManagerSc;

    private Slider slider;

    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 100;
    }

    void Update()
    {
        slider.value = gameManagerSc.electricity;
    }
}
