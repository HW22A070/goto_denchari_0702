using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpValueEffC : MonoBehaviour
{
    [SerializeField]
    private Text _childText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * 0.03f;
        if (transform.position.y > 20) Destroy(gameObject);
    }

    /// <summary>
    /// テキストセット
    /// </summary>
    /// <param name="text"></param>
    public void SetText(float[] prmDelta, string[] prmNameText, int useMoney)
    {
        string[] textPrm = new string[4] { "", "", "", "" };
        string textMessage = "";
        for (int i = 0; i < prmDelta.Length; i++)
        {
            if (prmDelta[i] != 0)
            {
                switch (prmNameText[i])
                {
                    case "Health":
                        prmNameText[i] = "体力";
                        break;
                    case "Hapiness":
                        prmNameText[i] = "幸福";
                        break;
                }
                textPrm[i] = prmNameText[i] + " " + prmDelta[i].ToString() + "\n";
            }
        }
        useMoney *= -1;
        string textPrime = "<color=#CCCCFF>費用 ＄" + useMoney.ToString() + "</color>";
        for (int i = 0; i < prmDelta.Length; i++)
        {
            if (prmDelta[i] < 0) textPrm[i] = "<color=#CCCCFF>" + textPrm[i] + "</color>";
            textMessage += textPrm[i];
        }
        _childText.text = textMessage + textPrime;
    }


}
