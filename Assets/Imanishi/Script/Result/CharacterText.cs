using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterText : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject fullScoreText;
    [SerializeField] private GameObject zeroScoreText;

    // 以下関数
    public void TextManager()  //「％」のテキストを表示する
    {
        text.SetActive(true);
    }

    public void TextSetActive()  //「％」のテキストを非表示にする
    {
        text.SetActive(false);
    }

    //------------------------------------------------------------

    public void FullScoreTextManager()  // FullScoreのテキストを表示する
    {
        fullScoreText.SetActive(true);
    }

    public void FullScoreTextSetActive()  // FullScoreのテキストを非表示にする
    {
        fullScoreText.SetActive(false);
    }

    //------------------------------------------------------------

    public void ZeroScoreTextManager()  // ZeroScoreのテキストを表示する
    {
        zeroScoreText.SetActive(true);
    }

    public void ZeroScoreTextSetActive()  // ZeroScoreのテキストを非表示にする
    {
        zeroScoreText.SetActive(false);
    }
}
