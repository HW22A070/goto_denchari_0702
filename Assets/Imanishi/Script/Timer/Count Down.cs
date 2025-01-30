using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public int countdownMinutes;
    private float countdownSeconds;
    private Text timeText;
    public int countdownRepeats = 2; // 繰り返し回数(最大日数)
    private int currentCount = 0;    // 現在のカウントダウン回数
    public Going_Day going_Scene;
    public float delay = 5f;  
    public float day_delay; // 遅延時間を設定
    public bool isPaused = false;   // カウントダウンが一時停止中かどうかを示すフラグ
    public bool bool_ButtonPause;
    private bool isDelaying = false; // 遅延中かどうかを示すフラグ

    private void Start()
    {
        timeText = GetComponent<Text>();
        StartCountdown();
        //PauseCountdown(); //チュートリアル用のPause
    }

    void Update()
    {
        if (bool_ButtonPause)
        {
            ButtonPause();
        }

        if (!isPaused && countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            if (countdownSeconds <= 0)
            {
                //==================大石差込==========================
                GameObject.Find("GameManager").GetComponent<GameManager>().EndOneDay();
                //====================================================
                currentCount++;
                Debug.Log($"{currentCount}日目終了");

                if (currentCount < countdownRepeats)
                {
                    PauseCountdown();
                    Invoke("StartCountdown", delay);
                }
                else
                {
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
                // StartCountdown();
                Debug.Log("シーン移動");
                going_Scene.NextScene(); // 遅延後に次のシーンに移動
            }
        }
    }

    // 以下関数
    public void OneDay() // 1日の時間管理
    {
        if (!isPaused && countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");
        }
    }

    //===============大石差込===========================
    public void DaysEnd()
    {
        Debug.Log("一日強制終了");
        countdownSeconds = 0.1f;
    }
    //==================================================

    public void PauseTime() // Pause中の時間管理
    {
        delay = day_delay; // Pause解除の秒数を再設定
        isDelaying = true;
    }

    private void PauseCountdown() // Pauseする関数
    {
        isPaused = true;
        Debug.Log("一時停止");
    }

    private void ResumeCountdown() // Pauseを解除する関数
    {
        isPaused = false;
        Debug.Log("再開");
    }

    private void StartCountdown()
    {
        countdownSeconds = countdownMinutes * 60;
        isPaused = false;

        //===========大石差込==================
        GameObject.Find("GameManager").GetComponent<GameManager>().StartOneDay();
        //=====================================
    }

    private void ButtonPause() // ボタン入力でPauseする
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PauseCountdown();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ResumeCountdown();
            }
        }
    }
}

/*using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public int countdownMinutes;
    private float countdownSeconds;
    private Text timeText;
    public int countdownRepeats = 2; // 繰り返し回数(最大日数)
    private int currentCount = 0;    // 現在のカウントダウン回数
    public Going_Day going_Scene;
    public float delay;  
    public float day_delay; // 遅延時間を設定
    public bool isPaused = false;   // カウントダウンが一時停止中かどうかを示すフラグ
    public bool bool_ButtonPause;
    private bool isDelaying = false; // 遅延中かどうかを示すフラグ

    private void Start()
    {
        timeText = GetComponent<Text>();
        StartCountdown();
        //PauseCountdown(); //チュートリアル用のPause
    }


    void Update()
    {
        if (bool_ButtonPause)
        {
            ButtonPause();
        }

        if (isDelaying)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                delay = 0; // delayが0以下にならないように
                isDelaying = false;
                StartCountdown();
            }
        }



        if (!isPaused && countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");
        if(isPaused == true)
        {
            isDelaying = true;
        }

        if(isPaused == false)
        {
            delay = 5;
        }

            if (countdownSeconds <= 0)
            {
                isDelaying = true;
                delay = 5;
                currentCount++;
                Debug.Log($"{currentCount}回目終了");
                if (currentCount < countdownRepeats)//最終日判定
                {
                    PauseCountdown();
                    PauseTime();
                }
                if(currentCount <= countdownRepeats)
                {
                    PauseTime();
                }
                if(isPaused == false)
                {
                    going_Scene.NextScene();//シーン移動
                }
            }
        }
    }
    // 以下関数
    public void OneDay() //1日の時間管理
    {
        if (!isPaused && countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");
        }
    }
    
     public void PauseTime()//Pause中の時間管理
    {
        delay = day_delay;//Pause解除の秒数を再設定
        isDelaying = true;
    }
        
    private void PauseCountdown()//Pauseする関数
    {
        isPaused = true;
        Debug.Log("一時停止");
    }
        
    private void ResumeCountdown()//Pauseを解除する関数
    {
        isPaused = false;
        Debug.Log("再開");
    }

    private void StartCountdown()
    {
        countdownSeconds = countdownMinutes * 60;
        isPaused = false;
    }

    private void ButtonPause()//ボタン入力でPauseする
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PauseCountdown();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ResumeCountdown();
            }
        }
    }
}
/*using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public int countdownMinutes;
    private float countdownSeconds;
    private Text timeText;
    public int countdownRepeats = 2; // 繰り返し回数(最大日数)
    private int currentCount = 0;    // 現在のカウントダウン回数
    public Going_Day going_Scene;
    public float delay = 5f;  
    public float day_delay; // 遅延時間を設定
    public float delay1;
    public bool isPaused = false;   // カウントダウンが一時停止中かどうかを示すフラグ
    public bool bool_ButtonPause;
    private bool isDelaying = false; // 遅延中かどうかを示すフラグ

    private void Start()
    {
        timeText = GetComponent<Text>();
        StartCountdown();
        //PauseCountdown(); //チュートリアル用のPause
    }

    void Update()
    {
        if (bool_ButtonPause)
        {
            ButtonPause();
        }


        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            if (countdownSeconds <= 0)
            {
                currentCount++;
                Debug.Log($"{currentCount}回目終了");
                if (currentCount < countdownRepeats)
                {
                    PauseCountdown();
                    Invoke("StartCountdown", delay);
                }
                if (isDelaying == false)
                {
                    delay -= Time.deltaTime;
                    if (delay <= 0)
                    {
                        delay = 0; // delayが0以下にならないように
                        isDelaying = false;
                        StartCountdown();
                        going_Scene.NextScene(); // 遅延後に次のシーンに移動
                    }
                }
            }
        }
    }

    // 以下関数
    public void OneDay() // 1日の時間管理
    {
        if (!isPaused && countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");
        }
    }

    public void PauseTime() // Pause中の時間管理
    {
        delay = day_delay; // Pause解除の秒数を再設定
        isDelaying = true;
    }

    private void PauseCountdown() // Pauseする関数
    {
        isPaused = true;
        Debug.Log("一時停止");
    }

    private void ResumeCountdown() // Pauseを解除する関数
    {
        isPaused = false;
        Debug.Log("再開");
    }

    private void StartCountdown()
    {
        countdownSeconds = countdownMinutes * 60;
        isPaused = false;
    }

    private void ButtonPause() // ボタン入力でPauseする
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PauseCountdown();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ResumeCountdown();
            }
        }
    }
}*/




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public static bool beingMeasured;
    public static bool beingMeasured1;
    [SerializeField] public float timelimit;
    [SerializeField] public float timeover;
    private string scene = "End";

    void Start()
    {
        beingMeasured = true;
        beingMeasured1 = false;
    }

    void Update()
    {
        if (beingMeasured)
        {
            timelimit -= Time.deltaTime;

            if (timelimit <= 890)
            {
                timelimit = 890;
                beingMeasured = false;
                beingMeasured1 = true;
                Debug.Log("1日目終了");
            }
        }

        if (beingMeasured1)
        {
            timeover -= Time.deltaTime;

            if (timeover <= 25)
            {
                timeover = 25;
                beingMeasured1 = false;
                beingMeasured = true;
                Debug.Log("2日目移行");
            }
        }

        if (beingMeasured)
        {
            timelimit -= Time.deltaTime;

            if (timelimit <= 880)
            {
                timelimit = 880;
                beingMeasured = false;
                beingMeasured1 = true;
                Debug.Log("2日目終了");
            }
        }

        if (timelimit <= 0)
        {
            timelimit = 0;
            beingMeasured = false;
            SceneManager.LoadScene(scene);
        }
    }
}

public class CountDown : MonoBehaviour
{
    public static bool beingMeasured;
    [SerializeField] public float timelimit;
    private string scene = "End";

    void Start()
    {
        beingMeasured = true;
    }

    void Update()
    {
        if (beingMeasured)
        {
            timelimit -= Time.deltaTime;

            if (timelimit <= 600)
            {
                Debug.Log("1日目終了");
            }

            if (timelimit <= 300)
            {
                Debug.Log("2日目終了");
            }
        }

        if (timelimit <= 0)
        {
            Debug.Log("3日目終了");
            timelimit = 0;
            beingMeasured = false;
            SceneManager.LoadScene(scene);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public int countdownMinutes;
    private float countdownSeconds;
    private Text timeText;
    private string scene = "End";

    private void Start()
    {
        timeText = GetComponent<Text>();
        countdownSeconds = countdownMinutes * 60;
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timeText.text = span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0)
        {
            Debug.Log("3日目終了");
            countdownSeconds = 0;
            SceneManager.LoadScene(scene);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public int countdownMinutes;
    private float countdownSeconds;
    private Text timeText;
    private string scene = "End";
    public int countdownRepeats = 2; // 繰り返し回数(最大日数)
    private int currentCount = 0;     // 現在のカウントダウン回数

    private void Start()
    {
        timeText = GetComponent<Text>();
        StartCountdown();
        InvokeRepeating("Delay", 10.0f, 10.0f);
        InvokeRepeating("Cancel", 15.0f, 15.0f);
    }

        private void Delay()
    {
        UnityEditor.EditorApplication.isPaused = true;
        Debug.Log("一時停止");
    }

    private void Cancel()
    {
        UnityEditor.EditorApplication.isPaused = true;
        Debug.Log("再開");
    }

    void Update()
    {
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");


            if (countdownSeconds <= 0)
            {
                currentCount++;
                
                Debug.Log($"{currentCount}回目終了");
                
                if (currentCount < countdownRepeats)
                {
                    StartCountdown(); // 次のカウントダウンを開始
                }
                else
                {
                    SceneManager.LoadScene(scene); // 最後のカウントダウン終了後にシーンを変更
                }
            }
        }
    }

    private void StartCountdown()
    {
        countdownSeconds = countdownMinutes * 60;
    }

}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public int countdownMinutes;
    private float countdownSeconds;
    private Text timeText;
    private string scene = "End";
    public int countdownRepeats = 2; // 繰り返し回数(最大日数)
    private int currentCount = 0;    // 現在のカウントダウン回数
    public float delay = 5;
    private bool isPaused = false;   // カウントダウンが一時停止中かどうかを示すフラグ
    public bool bool_ButtonPause;

    private void Start()
    {
        timeText = GetComponent<Text>();
        StartCountdown();
        //InvokeRepeating("PauseCountdown", 300.0f, 300.0f); // 10秒後に一時停止を開始し、25秒ごとに繰り返す
        //InvokeRepeating("ResumeCountdown", 305.0f, 305.0f); // 15秒後に再開を開始し、25秒ごとに繰り返す
    }

    private void PauseCountdown()
    {
        isPaused = true;
        Debug.Log("一時停止");
    }

    private void ResumeCountdown()
    {
        isPaused = false;
        Debug.Log("再開");
    }

    void Update()
    {
        if(bool_ButtonPause)
        {
            ButtonPause();
        }
        
        if (!isPaused && countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            if (countdownSeconds <= 0)
            {
                currentCount++;
                Debug.Log($"{currentCount}回目終了");
                PauseCountdown();
                // Invoke("ResumeCountdown", delay); //5秒後にResumeCountdown();関数が呼び出されるわけではなく、Invoke();関数が5秒後に呼び出される

                if (currentCount < countdownRepeats)
                {
                    StartCountdown(); // 次のカウントダウンを開始
                }
                else
                {
                    SceneManager.LoadScene(scene); // 最後のカウントダウン終了後にシーンを変更
                }
            }
        }
    }

    private void StartCountdown()
    {
        countdownSeconds = countdownMinutes * 60;
    }

    private void ButtonPause()
    {
        if (isPaused == false)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                PauseCountdown();
            }
        }

        if (isPaused == true)
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                ResumeCountdown();
            }
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public int countdownMinutes;
    private float countdownSeconds;
    private Text timeText;
    private string scene = "End";
    public int countdownRepeats = 2; // 繰り返し回数(最大日数)
    private int currentCount = 0;    // 現在のカウントダウン回数
    public float delay;
    private bool isPaused = false;   // カウントダウンが一時停止中かどうかを示すフラグ

    private void Start()
    {
        timeText = GetComponent<Text>();
        StartCountdown();
    }

    private void PauseCountdown()
    {
        isPaused = true;
        Debug.Log("一時停止");
    }

    private void ResumeCountdown()
    {
        isPaused = false;
        Debug.Log("再開");
    }

    void Update()
    {
        // カウントダウンがゼロになったときにキー入力で一時停止を解除
        if (countdownSeconds <= 0 && isPaused)
        {
            // Invoke("ResumeCountdown", delay);
            if (Input.GetKeyDown(KeyCode.A))
            {
                ResumeCountdown();
                if (currentCount < countdownRepeats)
                {
                    StartCountdown(); // 次のカウントダウンを開始
                }
                else
                {
                    SceneManager.LoadScene(scene); // 最後のカウントダウン終了後にシーンを変更
                }
            }
        }
        else if (!isPaused)
        {
            if (countdownSeconds > 0)
            {
                countdownSeconds -= Time.deltaTime;
                var span = new TimeSpan(0, 0, (int)countdownSeconds);
                timeText.text = span.ToString(@"mm\:ss");
            }
            else
            {
                currentCount++;
                Debug.Log($"{currentCount}回目終了");
                PauseCountdown();
            }
        }
    }

    private void StartCountdown()
    {
        countdownSeconds = countdownMinutes * 60;
    }
}*/