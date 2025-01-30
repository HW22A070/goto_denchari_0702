using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseActionC : MonoBehaviour
{
    /// <summary>
    /// MouseRay Setting
    /// </summary>
    private Ray _MouseRay;

    /// <summary>
    /// MouseRay To Object HitSetting
    /// </summary>
    private RaycastHit _MouseToObjectRayHit;

    public GameObject MainCamera;

    //private bool _isCrickingAny;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _MouseRay = new Ray(GetMousePosition() - transform.forward * 10.0f, transform.forward);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_MouseRay.origin, _MouseRay.direction, out _MouseToObjectRayHit, Mathf.Infinity, 8))
            {
                Debug.Log(_MouseToObjectRayHit.collider.gameObject.name);

                //_MouseToObjectRayHit.collider.gameObject.GetComponent<ItemSummonerC>().Cricked();
                if (_MouseToObjectRayHit.collider.gameObject.tag == "Summoner")
                {

                }
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 3;
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        return target;
    }
}
