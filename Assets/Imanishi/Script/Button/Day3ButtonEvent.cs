using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //これがないとButtonクラスが使用できない

public class Day3ButtonEvent : MonoBehaviour
{
    //public SceneLoader sceneLoader;
    public Day3StoryManager day3StoryManager;
    public GameObject talkButton;
    public GameObject characterNameText;
    public GameObject windowText;
    public CountdownManager countdownManager;
    
    public void OnPressed() //ボタンを押したら何らかの処理を続行する関数
    {
        //sceneLoader.NextScene();
        day3StoryManager.CallSetStoryElement();
        talkButton.SetActive(false);
        characterNameText.SetActive(true);
        windowText.SetActive(true);
        countdownManager.ResultHideObject();
    }

}