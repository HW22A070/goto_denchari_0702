using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerOutlineManagerC : MonoBehaviour
{
    private Outline _scOutline;

    private void Start()
    {
        _scOutline = GetComponent<Outline>();
    }
    public void SetOutlineOn(bool[] isPrm)
    {
        if (!_scOutline.enabled)
        {
            _scOutline.enabled = true;

        }



    }
    public void SetOutlineOff()
    {
        if (_scOutline.enabled)
        {
            _scOutline.enabled = false;
        }
    }
}
