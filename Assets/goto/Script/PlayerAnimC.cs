using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimC : MonoBehaviour
{
    [SerializeField]
    private GameObject _efFood, _efWater, _efHappy;

    [SerializeField]
    private GameObject _ownFace;

    [SerializeField]
    Animator _anmEat;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseItemAnimation(int itemKind)
    {
        switch (itemKind)
        {
            case 0:
                _anmEat.SetBool("eat", true);
                break;
            case 1:
                _anmEat.SetBool("eat", true);
                break;
        }
        //Debug.Log(itemKind);
        StartCoroutine(EatDaray(itemKind));
    }

    private IEnumerator EatDaray(int itemKind)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 posMouse = _ownFace.transform.position;
        Quaternion rot = Quaternion.Euler(-90, 0, 0);
        switch (itemKind)
        {
            case 0:
                Destroy(Instantiate(_efFood, posMouse, rot), 2);
                break;
            case 1:
                Destroy(Instantiate(_efWater, posMouse, rot), 2);
                break;
            case 2:
                Destroy(Instantiate(_efHappy, posMouse, rot), 2);
                break;
        }
        _anmEat.SetBool("eat", false);
    }
}


