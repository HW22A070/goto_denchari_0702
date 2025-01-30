using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day3StoryManager : MonoBehaviour
{
    [SerializeField] private StoryData[] storyDatas;

    //[SerializeField] private Image background;
    //[SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI characterName;

    [SerializeField] private GameObject canvasName;
    [SerializeField] private GameObject canvasText;
    [SerializeField] private GameObject day3Story;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject window;

    [Header("他人のコード")]
    public GameManager gameManager; //8月11日追加。俺のコードではない。

    public int storyIndex {get; private set;}
    public int textIndex {get; private set;}

    private bool finishText = false;

    public CountdownManager countdownManager;
    // Start is called before the first frame update
    void Start()
    {
        countdownManager.isPaused = true; //会話に関する変数呼び出し
        storyText.text = "";
        characterName.text = "";
        gameManager.StartOneDay(); //8月11日追加。俺のコードではない。
        //SetStoryElement(storyIndex, textIndex);
        //canvas.SetActive(true);
        //window.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && finishText == true)
        {
            textIndex++;
            characterName.text = "";
            storyText.text = "";
            ProgressionStory(storyIndex);
            finishText = false;
        }
    }

    public void CallSetStoryElement() //追加
    {
        SetStoryElement(storyIndex, textIndex); //Start関数のInvoke関数になる前の元々の部分
    }

    private void SetStoryElement (int _storyIndex, int _textIndex)
    {
        var storyElement = storyDatas[_storyIndex].stories[_textIndex];

        //background.sprite = storyElement.Background;
        //characterImage.sprite = storyElement.CharacterImage;
        characterName.text = storyElement.CharacterName;

        StartCoroutine(TypeSentence(_storyIndex, _textIndex));
    }

    private void ProgressionStory(int _storyIndex)
    {
        if (textIndex < storyDatas[_storyIndex].stories.Count)
        {
            SetStoryElement(storyIndex, textIndex);
        }
        else
        {
            Destroy(canvasName);
            Destroy(canvasText);
            Destroy(day3Story);
            canvas.SetActive(false);
            window.SetActive(false);
            countdownManager.isPaused = false; //会話に関する変数呼び出し
            //選択肢を出す
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
        foreach (var letter in storyDatas[_storyIndex].stories[_textIndex].StoryText.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        finishText = true;
    }
}