using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyAudio
{
    BGM,
    SE
}

public class BGMManagerC : MonoBehaviour
{

    [SerializeField]
    private MyAudio _myAudio;

    private AudioSource _asOwn;

    // Start is called before the first frame update
    void Start()
    {
        _asOwn = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _asOwn.volume = BGMData.AudioVolume[(int)_myAudio + 1] * BGMData.AudioVolume[0];
    }

}
