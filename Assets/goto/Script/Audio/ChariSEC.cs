using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChariSEC : MonoBehaviour
{
    private AudioSource _asOwn;

    // Start is called before the first frame update
    void Start()
    {
        _asOwn = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ChariSEStop()
    {
        _asOwn.Stop();
    }

    public void ChariSEStart()
    {
        _asOwn.Play();
    }
}
