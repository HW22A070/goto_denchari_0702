using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMaterialC : MonoBehaviour
{
    [SerializeField]
    private Material _mDefault, _mDeathPurple;
    private SkinnedMeshRenderer _meshOwn;

    // Start is called before the first frame update
    void Start()
    {
        _meshOwn = GetComponent<SkinnedMeshRenderer>();
        SetDeathPurple(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 死の紫にする
    /// </summary>
    /// <param name="isDeath"></param>
    public void SetDeathPurple(bool isDeath)
    {
        if (isDeath) _meshOwn.material = _mDeathPurple;
        else _meshOwn.material = _mDefault;
    }
}
