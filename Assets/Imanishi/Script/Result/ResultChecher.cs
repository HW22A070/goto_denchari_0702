using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class ResultChecker : MonoBehaviour
{
    [Header("スコア")]
    public float score;
    public int calculatedScore;

    //ノルマ　
    [HideInInspector] public float quota = 0;

    [Header("ゲームオブジェクト")]
    [SerializeField] private GameObject tittleButton;
    [SerializeField] private GameObject zeroScoreObject;
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject fullScoreObject;
    [SerializeField] private GameObject[] luminescene;

    [Header("テキスト")]
    [SerializeField] private TextMeshProUGUI zeroScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI oneHandletScoreText;

    [Header("フラグ")]
    private bool judge = true;
    private bool setActive = true;
    private bool successflag = true;
    private bool failflag = true;
    private bool small = true;
    private bool zero = true;
    private bool isActivating = false;

    [Header("Bloom")]
    public PostProcessVolume ppVolume;
    private Bloom bloom;

    [Header("関数呼び出しの変数")]
    public CharacterText characterText;
    public EndingStoryManager endingStoryManager;

    void Start()
    {
        zeroScoreObject.SetActive(false);
        scoreObject.SetActive(false);
        fullScoreObject.SetActive(false);
        tittleButton.SetActive(false);

        characterText.TextSetActive();
        characterText.FullScoreTextSetActive();
        characterText.ZeroScoreTextSetActive();

        Change();

        bloom = ppVolume.profile.GetSetting<Bloom>();
    }

    void Update()
    {
        ActivateObjectsByScore();

        if (judge == true)
        {
            ScoreJudge();
        }

        SetBloomIntensity();
    }

    public void ScoreFlag()
    {
        if (zero == false)
        {
            ZeroScore();
        }
        else if (small == false)
        {
            SmallScore();
        }
        else if (failflag == false)
        {
            FailJudge();
        }
        else if (successflag == false)
        {
            SuccessJudge();
        }
        else
        {
            FullScore();
        }
    }

    public void ScoreJudge()
    {
        if (calculatedScore <= 0)
        {
            judge = false;
            zero = false;
            SetActive();
        }
        else if (calculatedScore <= 9 && calculatedScore >= 1)
        {
            judge = false;
            small = false;
            SetActive();
        }
        else if (calculatedScore <= 49 && calculatedScore >= 10)
        {
            judge = false;
            failflag = false;
            SetActive();
        }
        else if (calculatedScore >= 50 && calculatedScore <= 99)
        {
            judge = false;
            successflag = false;
            SetActive();
        }
        else
        {
            SetActive();
        }
    }

    public void ActivateObjectsByScore()
    {
        if (!isActivating)
        {
            StartCoroutine(ActivateObjectsWithDelay());
        }
    }

    private IEnumerator ActivateObjectsWithDelay()
    {
        int count = Mathf.Clamp(calculatedScore * 2, 0, luminescene.Length);

        for (int i = 0; i < luminescene.Length; i++)
        {
            luminescene[i].SetActive(false);
        }

        for (int i = 0; i < count; i++)
        {
            luminescene[i].SetActive(true);
            isActivating = true;

            if (i < count - 1 && isActivating)
            {
                yield return new WaitForSeconds(0.1f);
                isActivating = false;
            }
        }
        ScoreFlag();
    }

    void SetIntensity(float intensity)
    {
        bloom.intensity.value = intensity;
    }

    public void SetBloomIntensity()
    {
        for (int i = 0; i < calculatedScore; i++)
        {
            SetIntensity(1.0f * (i / 100));
        }
    }

    public void Change()
    {
        calculatedScore = Mathf.FloorToInt(score / 4883 * 100);
    }

    public void ZeroChangeDataType()
    {
        zeroScoreText.text = calculatedScore.ToString();
        characterText.ZeroScoreTextManager();
    }

    public void ChangeDataType()
    {
        scoreText.text = calculatedScore.ToString();
        characterText.TextManager();
    }

    public void OneHandletChangeDataType()
    {
        characterText.FullScoreTextManager();
    }

    public void SetActive()
    {
        setActive = false;
    }

    public void SuccessJudge()
    {
        if (setActive == false)
        {
            ChangeDataType();
            scoreObject.SetActive(true);
            characterText.TextManager();
            tittleButton.SetActive(true);
        }
    }

    public void FailJudge()
    {
        if (setActive == false)
        {
            ChangeDataType();
            scoreObject.SetActive(true);
            characterText.TextManager();
            tittleButton.SetActive(true);
        }
    }

    public void SmallScore()
    {
        if (setActive == false)
        {
            ZeroChangeDataType();
            zeroScoreObject.SetActive(true);
            characterText.ZeroScoreTextManager();
            tittleButton.SetActive(true);
        }
    }

    public void ZeroScore()
    {
        if (setActive == false)
        {
            ZeroChangeDataType();
            zeroScoreObject.SetActive(true);
            characterText.ZeroScoreTextManager();
            tittleButton.SetActive(true);
        }
    }

    public void FullScore()
    {
        if (setActive == false)
        {
            OneHandletChangeDataType();
            fullScoreObject.SetActive(true);
            characterText.FullScoreTextManager();
            tittleButton.SetActive(true);
            endingStoryManager.canvas.SetActive(true);
            endingStoryManager.window.SetActive(true);
            endingStoryManager.CallSetStoryElement();
        }
    }
}