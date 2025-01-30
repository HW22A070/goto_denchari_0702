using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadMusicC : MonoBehaviour
{
    
    /// <summary>
    /// オーディオソース
    /// </summary>
    private AudioSource _audioManager,_audioBGMManager, _audioBGSManager;

    /// <summary>
    /// アイテム使用音
    /// </summary>
    [SerializeField]
    private AudioClip _soundLoadScene;

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GetComponent<AudioSource>();
        _audioBGMManager =GameObject.Find("BGMManager").GetComponent<AudioSource>();
        _audioBGSManager =GameObject.Find("Chanri").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveSceneSE()
    {
        _audioManager.PlayOneShot(_soundLoadScene);
        StartCoroutine(DeleteBGMs());
    }

    private IEnumerator DeleteBGMs()
    {
        for(int hoge=0; hoge < 20; hoge++)
        {
            _audioBGMManager.volume -= 0.05f;
            _audioBGSManager.volume -= 0.05f;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
