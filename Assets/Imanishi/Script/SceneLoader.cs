using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManager.LoadScene関数を使うにはここを追加する

public class SceneLoader : MonoBehaviour
{
    [SerializeField] //inspectorで変数の値を変えられる。属性というらしい。
    private string day = "Main 02"; //Sceneの"Day_02"をDay02という変数に突っ込んでいる。constを宣言すると値の変更不可。

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextScene() //ボタンを押したら何らかの処理を続行する関数
    {
        SceneManager.LoadScene(day); //SceneManager.LoadScene(day);の引数に入っているday変数のSceneを開く
    }
}