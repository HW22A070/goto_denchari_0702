using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //これがないとButtonクラスが使用できない

public class ButtonEvent : MonoBehaviour
{
    public SceneLoader sceneLoader;
    
    public void OnPressed() //ボタンを押したら何らかの処理を続行する関数
    {
        sceneLoader.NextScene();
    }

}