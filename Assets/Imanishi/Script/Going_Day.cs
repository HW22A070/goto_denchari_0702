using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManager.LoadScene関数を使うにはここを追加する
/*using UnityEngine.UI; //これがないとButtonクラスが使用できない*/

public class Going_Day : MonoBehaviour
{
    [SerializeField] //inspectorで変数の値を変えられる。属性というらしい。
    private string day = "Main"; //Sceneの"Day_02"をDay02という変数に突っ込んでいる。constを宣言すると値の変更不可。

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKey(KeyCode.A)) //Aキーが押されるとシーンが切り替わる
        {
            SceneManager.LoadScene(day);
        }*/
    }

    public void NextScene() //ボタンを押したら何らかの処理を続行する関数
    {
        StartCoroutine(MoveSceneCoroutine());
        //SceneManager.LoadScene(day); //SceneManager.LoadScene(day);の引数に入っているday変数のSceneを開く
    }

    //＝＝＝＝＝＝＝後藤追加＝＝＝＝＝＝＝

    /// <summary>
    /// 呼び出されたら音を鳴らし、1.5秒後にシーンをロードする。
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveSceneCoroutine()
    {
        GameObject se = GameObject.Find("Chanri");
        se.GetComponent<AudioSource>().pitch = 1.0f;
        se.GetComponent<AudioSource>().Stop();
        se.GetComponent<SceneLoadMusicC>().MoveSceneSE();
        //１.0秒待ての指示
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(day); //SceneManager.LoadScene(day);の引数に入っているday変数のSceneを開く
    }
    //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

}