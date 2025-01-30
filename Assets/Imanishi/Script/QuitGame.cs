using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void Update()
    {
        EndGame();
    }

    public void EndGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            // UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        }
    }

}
