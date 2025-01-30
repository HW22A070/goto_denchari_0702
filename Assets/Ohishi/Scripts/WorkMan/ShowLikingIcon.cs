using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLikingIcon : MonoBehaviour
{
    [Header("好き嫌いパーティクル")]
    [SerializeField] GameObject LikeIcon;
    [SerializeField] GameObject DislikeIcon;

    //---------------------------------------------
    void Start()
    {

    }

    void Update()
    {

    }

    //好き嫌いアイコン表示-----------------------------
    public void ShowLiking(bool _like)
    {
        if (_like) Instantiate(LikeIcon, this.gameObject.transform.position, Quaternion.identity);
        else Instantiate(DislikeIcon, this.gameObject.transform.position, Quaternion.identity);
    }
}
