using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SetAudio
{
    MAIN,
    BGM,
    SE
}

public class BGMVolumeC : MonoBehaviour
{
    [SerializeField]
    private SetAudio _setAudio;
    private Slider _sliOwn;

    // Start is called before the first frame update
    void Start()
    {
        _sliOwn = GetComponent<Slider>();
        _sliOwn.value = BGMData.AudioVolume[(int)_setAudio];
    }

    // Update is called once per frame
    void Update()
    {
        BGMData.AudioVolume[(int)_setAudio] = _sliOwn.value;
    }
}
