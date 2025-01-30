using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameCardAnimeter : MonoBehaviour
{
    [Header("選択時外枠")]
    [SerializeField] Image selectOutline;

    [Header("拡大倍率")]
    [SerializeField] private float scaleFactor = 1;

    [Header("アニメーション速度")]
    [SerializeField] private float animSpeed = 0;

    //----------------------------------------
    void Start()
    {
        SetUp();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        AnimScale();
        AnimPosition();
    }

    //初期化----------------------------------

    RectTransform rectTransform;
    private GameObject parent;
    Vector2 scaleBig;

    private void SetUp()
    {
        if (this.gameObject.TryGetComponent<RectTransform>(out var comp1)) rectTransform = comp1;
        parent = this.gameObject.transform.parent.gameObject;
        scaleBig = new Vector2(scaleFactor, scaleFactor);
    }

    //カーソルが重なった時----------------------
    public void OnSelect()
    {
        if (!selectOutline.enabled) selectOutline.enabled = true;
    }

    //カーソルが離れた時------------------------
    public void DeSelect()
    {
        if (selectOutline.enabled) selectOutline.enabled = false;
    }

    //クリックされた時-------------------------

    private bool hasClicked = false;

    public void OnClick()
    {
        if (hasClicked)
        {
            targetSize = 1;
            this.gameObject.transform.SetParent(parent.transform);
            hasClicked = false;
        }
        else
        {
            targetSize = scaleFactor;
            this.gameObject.transform.SetParent(parent.transform.parent.transform.parent);
            hasClicked = true;
        }
    }

    //スケール変更アニメーション----------------------------

    private float targetSize = 1;

    private void AnimScale()
    {
        float scaleX = Mathf.Lerp(rectTransform.localScale.x, targetSize, animSpeed);
        float scaleY = Mathf.Lerp(rectTransform.localScale.y, targetSize, animSpeed);
        rectTransform.localScale = new Vector2(scaleX, scaleY);
    }

    //ポジション変更アニメーション--------------------------
    private void AnimPosition()
    {
        float posX = Mathf.Lerp(rectTransform.localPosition.x, 0, animSpeed);
        float posY = Mathf.Lerp(rectTransform.localPosition.y, 0, animSpeed);
        rectTransform.localPosition = new Vector2(posX, posY);
    }
}
