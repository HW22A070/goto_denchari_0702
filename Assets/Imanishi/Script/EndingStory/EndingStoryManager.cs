using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingStoryManager : MonoBehaviour
{
    [Header("ストーリーデータを格納する配列")]
    [SerializeField] private StoryData[] storyDatas;

    [Header("キャラクターの名前")]
    [SerializeField] public GameObject canvas;
    [SerializeField] private GameObject canvasName;
    [SerializeField] private TextMeshProUGUI characterName;

    [Header("ストーリーテキスト")]
    [SerializeField] public GameObject window;
    [SerializeField] private GameObject canvasText;
    [SerializeField] private TextMeshProUGUI storyText;

    [Header("ストーリーオブジェクト")]
    [SerializeField] public GameObject day1Story;

    public int storyIndex { get; private set; }
    public int textIndex { get; private set; }

    private bool finishText = false; // 会話に関する変数呼び出し

    private Coroutine typingCoroutine; // コルーチンの参照を保持

    void Start()
    {
        storyText.text = "";
        characterName.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && finishText) //1文目の会話が終わったら呼び出される。二言目に行くための処理。今は必要ない。
        {
            textIndex++;
            ProgressionStory(storyIndex);
            finishText = false; // これがないと二言目以降の会話で話が終わってないのに次に進んでしまう
        }
    }

    public void CallSetStoryElement() // 外部から呼び出す関数
    {
        SetStoryElement(storyIndex, textIndex);
    }

    /*private void SetStoryElement(int _storyIndex, int _textIndex) //クリックで文章表示の場合必要
    {
        // インデックス範囲チェック
        if (_textIndex >= storyDatas[_storyIndex].stories.Count)
        {
            //Debug.LogWarning("テキストインデックスが範囲外です"); //Debug.LogWarningにすると警告文に変化する
            //Debug.LogError("テキストインデックスが範囲外です"); Debug.LogErrorにするとエラー文に変化する
            return;
        }

        var storyElement = storyDatas[_storyIndex].stories[_textIndex];

        // テキストをリセット
        storyText.text = "";
        characterName.text = storyElement.CharacterName;

        // 既存のコルーチンを停止
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // 新しいコルーチンを開始
        typingCoroutine = StartCoroutine(TypeSentence(_storyIndex, _textIndex));
    }*/

    private void SetStoryElement(int _storyIndex, int _textIndex)
    {
        // コルーチンが進行中の場合は新たに開始しない
        if (typingCoroutine != null) return;

        var storyElement = storyDatas[_storyIndex].stories[_textIndex];
        storyText.text = "";
        characterName.text = storyElement.CharacterName;

        typingCoroutine = StartCoroutine(TypeSentence(_storyIndex, _textIndex));
    }


    private void ProgressionStory(int _storyIndex)
    {
        if (textIndex < storyDatas[_storyIndex].stories.Count)
        {
            SetStoryElement(storyIndex, textIndex);
        }
    }

    private void ChangeStoryElement()
    {
        textIndex = 0;
        storyIndex++;
        SetStoryElement(storyIndex, textIndex);
    }

    private IEnumerator TypeSentence(int _storyIndex, int _textIndex)
    {
        var storyElement = storyDatas[_storyIndex].stories[_textIndex];
        foreach (var letter in storyElement.StoryText.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        finishText = true;
    }
}
