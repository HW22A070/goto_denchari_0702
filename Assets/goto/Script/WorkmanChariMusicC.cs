using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkmanChariMusicC : MonoBehaviour
{
    /// <summary>
    /// オーディオソース
    /// </summary>
    private AudioSource _audioChariManager;

    // Start is called before the first frame update
    void Start()
    {
        _audioChariManager = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject[] workman = GameObject.FindGameObjectsWithTag("Player");
        if (workman.Length <= 0)
        {
            _audioChariManager.volume = 0.0f;
        }
        else
        {
            _audioChariManager.volume = 0.4f;
        }
    }
}
