using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteHandController : MonoBehaviour
{
    private int currentCount = 0;    // 現在のカウントダウン回数

    private float currentRotation = 360.0f; // 秒針の現在の回転角度
    private float delay = 5f; // 最終日の遅延時間を設定

    private Vector3 rotationAxis = new Vector3(0, 0, -1); // Z軸に沿った回転軸

    private bool isMinuteHandMoving = false; // 秒針の動きを制御するフラグ

    // Start is called before the first frame update
    void Start()
    {
        PauseCountdown();
        Invoke("ResumeCountdown", delay);
    }

    // Update is called once per frame
    void Update()
    {
        // 分針が動いている場合のみ回転させる
        if (!isMinuteHandMoving)
        {
            // 1秒あたり6度回転させる
            float rotationAngle = 6f * Time.deltaTime;
            currentRotation -= rotationAngle;

            // currentRotation <= 0fの条件が満たされたとき
            if (currentRotation <= 0f)
            {
                // オブジェクトをZ軸に沿って30度回転させる
                float rotationAngle2 = 6.0f; // Z軸に沿って回転させる角度
                transform.Rotate(rotationAxis, rotationAngle2);
                currentCount++;
                currentRotation = 360.0f;

                // 5回転ごとにカウントダウンを一時停止
                if (currentCount % 5 == 0)
                {
                    PauseCountdown();
                    Invoke("ResumeCountdown", delay);
                }
            }
        }
    }

    private void PauseCountdown() // Pauseする関数
    {
        isMinuteHandMoving = true; // 分針の動きを停止
        //Debug.Log("カウントダウンが一時停止しました");
    }

    private void ResumeCountdown() // Pauseを解除する関数
    {
        isMinuteHandMoving = false; // 分針の動きを再開
        //Debug.Log("カウントダウンが再開しました");
    }
}