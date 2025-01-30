using UnityEngine;

public class SecondHandController : MonoBehaviour
{
    private int countdownRepeats = 2; // 繰り返し回数(最大日数) //public
    private int currentCount = 0;    // 現在のカウントダウン回数
    private int pressCount = 0;

    private float delay = 5f; // 最終日の遅延時間を設定 //public
    private float day_delay; // 遅延時間を設定 //public
    private float currentRotation = 1800.0f; // 秒針の現在の回転角度を追跡、360度で１分

    private bool bool_ButtonPause; //public
    private bool isDelaying = false; // 遅延中かどうかを示すフラグ
    private bool isSecondHandMoving = false; // 秒針の動きを制御するフラグ

    private SceneLoader sceneLoader; //public
    private Vector3 rotationAxis = new Vector3(0, 0, -1);
    

    void Start()
    {
        PauseCountdown();
        Invoke("ResumeCountdown",delay);
    }

    void Update()
    {
        ButtonPause();
        // 秒針が動いている場合のみ回転させる
        if (!isSecondHandMoving)
        {
            // 1秒あたり6度回転させる
            float rotationAngle = 6f * Time.deltaTime;

            // 秒針を回転させる
            transform.Rotate(rotationAxis, rotationAngle);

            // 現在の回転角度を更新
            currentRotation -= rotationAngle;

            // 回転角度が360度を超えたらリセット
            if (currentRotation <= 0f)
            {
                currentCount++;
                currentRotation = 1800.0f;
                if (currentCount < countdownRepeats)
                {
                    PauseCountdown();
                    Invoke("ResumeCountdown", delay);
                }
                else
                {
                    PauseCountdown();
                    isDelaying = true;
                }
            }
        }

        if (isDelaying)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                delay = 0; // delayが0以下にならないように
                isDelaying = false;
                sceneLoader.NextScene(); // 遅延後に次のシーンに移動
            }
        }
    }

    private void PauseCountdown() // Pauseする関数
    {
        isSecondHandMoving = true; // 秒針の動きを停止
        //Debug.Log("カウントダウンが一時停止しました");
    }

    private void ResumeCountdown() // Pauseを解除する関数
    {
        isSecondHandMoving = false; // 秒針の動きを再開
        //Debug.Log("カウントダウンが再開しました");
    }

    private void ButtonPause() // ボタン入力でPauseする
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pressCount++;
        }
        if(pressCount % 2 == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}