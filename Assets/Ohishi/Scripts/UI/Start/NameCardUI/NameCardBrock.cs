using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameCardBrock : MonoBehaviour
{
    [Header("表示ブロック")]
    [SerializeField] private List<Image> brocks = new List<Image>();

    //--------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    //ブロック表示-----------------------------------
    public void DisplayBrock(int _count)
    {
        for (int i1 = 0; i1 < _count; i1++)
        {
            brocks[i1].enabled = true;
        }
    }
}
