using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ItemPopUpC : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textPopUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PopUpText(string name)
    {
        _textPopUp.text = name;
    }
}
