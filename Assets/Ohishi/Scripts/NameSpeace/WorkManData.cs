using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkManData
{
    [System.Serializable]
    public struct DataInfo
    {
        public GameObject workmanModel;                      //労働者モデル
        public NameInfo nameInfo;                       //名前情報
        public Dictionary<string, float> parameters;    //パラメータ情報
        public LikingInfo likingInfo;                   //好き嫌い情報
        public OtherInfo otherInfo;                     //その他情報
    }

    //名前~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [System.Serializable]
    public struct NameInfo
    {
        public string name;     //名前
        public string furigana; //ふりがな
    }

    //好き嫌い~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [System.Serializable]
    public struct LikingInfo
    {
        public List<string> likes;      //好きなものリスト
        public List<string> dislikes;   //嫌いなものリスト
    }

    //その他~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [System.Serializable]
    public struct OtherInfo
    {
        public int employeeNum; //社員番号
        public Date birthday;   //生年月日
    }

    //年月日~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [System.Serializable]
    public struct Date
    {
        public int year;
        public int month;
        public int day;
    }
}
