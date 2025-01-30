using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //これがないとButtonクラスが使用できない

public class Button_Event : MonoBehaviour
{
    //public SceneLoader sceneLoader;
    public StoryManager storyManager;
    public GameObject talkButton;
    public GameObject characterNameText;
    public GameObject windowText;
    
    public void OnPressed() //ボタンを押したら何らかの処理を続行する関数
    {
        //sceneLoader.NextScene();
        storyManager.CallSetStoryElement(); //会話する関数呼び出し
        talkButton.SetActive(false); //ボタンが押されるとボタンオブジェクトを非表示にする
        characterNameText.SetActive(true); //キャラクターの名前のテキスト
        windowText.SetActive(true); //会話テキスト
    }

}