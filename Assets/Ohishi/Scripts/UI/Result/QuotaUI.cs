using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuotaUI : MonoBehaviour
{
    [Header("切り替えUI")]
    [SerializeField] private Image before;
    [SerializeField] private Image after;

    //------------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    //しきいバー色切り替え-------------------------------
    public void Coloring()
    {
        before.enabled = false;
        after.enabled = true;
    }
}
