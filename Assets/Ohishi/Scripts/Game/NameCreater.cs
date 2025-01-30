using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NameCreater : MonoBehaviour
{
    [Header("労働者名リスト.csvのファイル名")]
    [SerializeField] private string csvFileName = "";

    [Header("レア度確率リスト（％）"), Range(0f, 100f)]
    [SerializeField] private List<float> rarityProbabilities = new List<float>();

    //---------------------------------------
    void Start()
    {
        ReadCSV();
        FormatInputData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            string[] name = CreateName();
            Debug.Log($"{name[0]} {name[1]}"); ;
        }
    }

    //CSVファイル読み込み-----------------------

    List<string[]> nameDatas = new List<string[]>();

    private void ReadCSV()
    {
        //ファイルを読み込む
        TextAsset nameFile = Resources.Load(csvFileName) as TextAsset;
        StringReader reader = new StringReader(nameFile.text);

        //読み込んだファイルを各行カンマ区切りで分ける
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            nameDatas.Add(line.Split(','));
        }
        nameDatas.Remove(nameDatas.First());
    }

    //入力データ整形---------------------------

    private struct Name
    {
        public string name;
        public string furigana;

        public Name(string _name, string _furigana)
        {
            name = _name;
            furigana = _furigana;
        }
    }

    private List<Name>[] lastNames;   //処理用苗字配列
    private List<Name>[] firstNames;  //処理用名前配列
    private List<Name>[][] names;     //処理用配列の配列

    private float totalProbability;

    private void FormatInputData()
    {
        //各配列の初期化
        lastNames = new List<Name>[rarityProbabilities.Count];
        firstNames = new List<Name>[rarityProbabilities.Count];
        names = new List<Name>[][] { lastNames, firstNames };
        for (int i1 = 0; i1 < names.Length; i1++)
        {
            for (int i2 = 0; i2 < names[i1].Length; i2++)
            {
                names[i1][i2] = new List<Name>();
            }
        }

        //ファイルのデータをレア度毎に苗字と名前の配列に仕分ける
        foreach (var _datas in nameDatas)
        {
            Name name = new Name(_datas[1], _datas[2]);
            int rarity = int.Parse(_datas[3]);

            if (_datas[0] == "苗字")
            {
                name.name += " ";
                name.furigana += " ";
                lastNames[rarity].Add(name);
            }
            else
            {
                firstNames[rarity].Add(name);
            }
        }

        //レア度毎の確率を降順にソートする
        rarityProbabilities.Sort();
        rarityProbabilities.Reverse();

        //確率の合計値を算出する
        foreach (var _probability in rarityProbabilities)
        {
            totalProbability += _probability;
        }
    }

    //名前生成--------------------------------
    public string[] CreateName()
    {
        string[] name = { "", "" };   //生成する名前

        foreach (var _array in names)
        {
            int rarity = 0; //選出されたレア度

            //レア度選出
            float rarityRnd = MyRandomF(totalProbability);
            float probabilityOffset = 0;
            foreach (var _probability in rarityProbabilities)
            {
                probabilityOffset += _probability;
                if (rarityRnd <= totalProbability - probabilityOffset)
                {
                    rarity++;
                }
                else break;
            }

            //名前選出
            int nameNum = MyRandomI(_array[rarity].Count);
            name[0] += _array[rarity][nameNum].name;
            name[1] += _array[rarity][nameNum].furigana;
        }

        return name;
    }

    //ランダム値生成(float)--------------------
    private float MyRandomF(float _max)
    {
        float rnd = Random.Range(0f, _max);
        return rnd;
    }

    //ランダム値生成(int)----------------------
    private int MyRandomI(int _max)
    {
        int rnd = Random.Range(0, _max);
        return rnd;
    }
}
