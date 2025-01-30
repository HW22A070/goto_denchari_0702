using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSystemC : MonoBehaviour
{
    [SerializeField]
    private bool _isDemo;

    private float _time = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (!_isDemo)
        {
            if (_time > 30)
            {
                SceneManager.LoadSceneAsync("Demo");
            }
        }
        else
        {
            if (_time > 60 || Input.anyKeyDown)
            {
                SceneManager.LoadSceneAsync("Title");
            }
        }
    }
}
