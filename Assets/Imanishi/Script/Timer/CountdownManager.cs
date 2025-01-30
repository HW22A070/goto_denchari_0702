using System;
using UnityEngine;
using UnityEngine.UI; // 時計のUIを使う場合、不要になる
using UnityEngine.SceneManagement;

public class CountdownManager : MonoBehaviour
{
    [Header("カウントダウン処理")]
    public int seconds; // タイマーの時間を秒数で設定
    public int countdownRepeats; // 繰り返し回数(最大日数)
    private int countdownMinutes = 1;
    private float countdownSeconds;
    private int currentCount = 0;    // 現在のカウントダウン回数
    private Text timeText; // 時計のUIを使う場合、不要になる

    [Header("シーン移動")]
    public SceneLoader sceneLoader;

    [Header("遅延処理")]
    public float delay = 5f; // 最終日の遅延時間を設定
    public float day_delay; // 遅延時間を設定

    [Header("Pause判定")]
    public bool isPaused = false;   // カウントダウンが一時停止中かどうかを示すフラグ
    public bool bool_ButtonPause;
    private bool isDelaying = false; // 遅延中かどうかを示すフラグ

    [Space(10)]
    private int pressCount = 0;

    [Header("会話テキストデータ")]
    public GameObject day2text; // 会話に関する
    public GameObject day3text; // 会話に関する

    [Header("ボーナスアイテム")]
    public GameObject[] bonusItem;
    public bool day2bounusItem = false; // 二日目のテキスト表示のフラグ
    private bool day3bounusItem = false; // 三日目のテキスト表示のフラグ

    [Header("ボタンで会話")]
    public Day2ButtonEvent day2ButtonEvent;
    public Day3ButtonEvent day3ButtonEvent;

    [Header("リザルト表示")]
    public GameObject rezult;

    [Header("他人のコード")]
    public GameManager gameManager; //8月11日追加。俺のコードではない。

    private void Start()
    {
        timeText = GetComponent<Text>(); // 時計のUIを使う場合、不要になる
        StartCountdown();
    }

    void Update()
    {
        EndGame();
        if (bool_ButtonPause)
        {
            ButtonPause();
        }

        if (!isPaused && countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss"); // 時計のUIを使う場合、不要になる

            if (Input.GetKeyDown(KeyCode.A)) // 1日強制終了
            {
                DaysEnd();
            }

            if (countdownSeconds <= 0)
            {
                currentCount++;
                gameManager.EndOneDay(); // 8月11日追加。俺のコードではない。
                //Debug.Log($"{currentCount}日目終了");

                //RandomBonusItem();

                if (currentCount < countdownRepeats)
                {
                    //PauseCountdown();
                    Time.timeScale = 0;
                    Invoke("StartCountdown", delay);
                }
                else
                {
                    isDelaying = true;
                }
            }

            if (currentCount == 1) //会話に関する
            {
                if (!day2bounusItem) // 二日目のテキストを一度だけ表示するフラグ
                {
                    Invoke("Day2SetActive", delay);
                    Invoke("Day2ButtonSetActive", delay);
                    ResultSetActive();
                    RandomBonusItem();
                    day2bounusItem = true;
                }
            }

            if (currentCount == 2) //会話に関する
            {
                if (!day3bounusItem) // 三日目のテキストを一度だけ表示するフラグ
                {
                    Invoke("Day3SetActive", delay);
                    Invoke("Day3ButtonSetActive", delay);
                    ResultSetActive();
                    SetAllObjectsActive(false); // すべてのボーナスアイテムを非表示にする
                    RandomBonusItem(); // ボーナスアイテムをランダムに表示
                    day3bounusItem = true; // フラグを設定
                }
            }

            if (currentCount == 3)
            {
                SetAllObjectsActive(false); //ボーナスアイテムを非アクティブ化している(true)で表示できる
                ResultSetActive();
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

    // 以下関数
    public void DaysEnd() // 1日を強制終了する関数
    {
        countdownSeconds = 0.1f; //大石変更 0f → 0.1f 外部から呼び出し時に正常に動作しなかったため
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timeText.text = span.ToString(@"mm\:ss"); // 時計のUIを使う場合、不要になる
    }

    public void RandomBonusItem() // ボーナスアイテムをアクティブにする関数
    {
        if (bonusItem.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, bonusItem.Length); // 配列の範囲でランダムなインデックスを取得
            bonusItem[randomIndex].SetActive(true); // ランダムに選ばれたアイテムをアクティブ化
            //Debug.Log("Random Index: " + randomIndex); // randomIndexの値をコンソールに出力
        }
    }

    void SetAllObjectsActive(bool isActive) // ボーナスアイテムだけを非アクティブにする関数
    {
        foreach (GameObject obj in bonusItem)
        {
            obj.SetActive(isActive); // 各オブジェクトに対してSetActiveを呼び出す
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
        //Debug.Log("一時停止");
    }

    private void ResumeCountdown() // Pauseを解除する関数
    {
        isPaused = false;
        //Debug.Log("再開");
    }

    private void StartCountdown()
    {
        countdownSeconds = countdownMinutes * seconds;
        isPaused = false;
    }

    // 2日目のテキストをアクティブにする
    private void Day2SetActive() //会話に関する
    {
        if (day2text != null) // オブジェクトがnullでないことを確認
        {
            day2text.SetActive(true);
        }
        else
        {
            //Debug.LogWarning("Day 2 text object is null.");
        }
    }

    // 3日目のテキストをアクティブにする //会話に関する
    private void Day3SetActive()
    {
        if (day3text != null) // オブジェクトがnullでないことを確認
        {
            day3text.SetActive(true);
        }
        else
        {
            //Debug.LogWarning("Day 3 text object is null.");
        }
    }

    private void Day2ButtonSetActive() // 二日目最初にボタンを表示する関数
    {
        day2ButtonEvent.talkButton.SetActive(true);
    }

    private void Day3ButtonSetActive() // 三日目最初にボタンを表示する関数
    {
        day3ButtonEvent.talkButton.SetActive(true);
    }

    public void ResultSetActive() // リザルトを表示する関数。8/13追加。
    {
        rezult.SetActive(true);
    }

    public void ResultHideObject()
    {
        rezult.SetActive(false);
    }

    private void ButtonPause() // ボタン入力でPauseする
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pressCount++;
        }
        if (pressCount % 2 == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void EndGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            // UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        }
    }
}