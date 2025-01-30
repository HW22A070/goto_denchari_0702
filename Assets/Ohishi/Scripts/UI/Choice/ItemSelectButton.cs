using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectButton : MonoBehaviour
{
    //---------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    //ボタンが押された時------------------------

    private bool hasPressed = false;

    public void OnPressed()
    {
        if (hasPressed)
        {
            hasPressed = false;
        }
        else
        {
            hasPressed = false;
        }
    }
}
