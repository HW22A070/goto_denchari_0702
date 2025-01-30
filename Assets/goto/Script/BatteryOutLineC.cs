using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryOutLineC : MonoBehaviour
{
    private int _batteryTrun = 0, _batteryTotal = 0;
    private Animator _animatorOwn;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("再起動");
        _animatorOwn = GetComponent<Animator>();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {

        }
    }
    */

    /// <summary>
    /// 枠変更
    /// </summary>
    public void ChangeOutLine(int hoge)
    {
        _batteryTrun = hoge;
        SetAmim(_batteryTrun + _batteryTotal);
    }

    public void SetNowLevel()
    {
        _batteryTotal = _batteryTrun;
        _batteryTrun = 0;
    }

    /// <summary>
    /// 電池アニメーション
    /// 0=黒、1=緑、2=黄
    /// </summary>
    /// <param name="powerLevel"></param>
    private void SetAmim(int powerLevel)
    {
        _animatorOwn.SetInteger("Level", powerLevel);

    }
}
