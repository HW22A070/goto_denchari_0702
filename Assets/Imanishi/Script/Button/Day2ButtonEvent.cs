using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //これがないとButtonクラスが使用できない

public class Day2ButtonEvent : MonoBehaviour
{
    //public SceneLoader sceneLoader;
    public Day2StoryManager day2StoryManager;
    public GameObject talkButton;
    public GameObject characterNameText;
    public GameObject windowText;
    public CountdownManager countdownManager;
    
    public void OnPressed() //ボタンを押したら何らかの処理を続行する関数
    {
        //sceneLoader.NextScene();
        day2StoryManager.CallSetStoryElement();
        talkButton.SetActive(false);
        characterNameText.SetActive(true);
        windowText.SetActive(true);
        countdownManager.ResultHideObject();
    }

}