using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    private float RotateSpeed = -0.1f;

    private float count = 10f;

    void Update()
    {
        count -= Time.deltaTime;
        XRoatate();
        if (count <= 8)
        {
            YRoatate();
        }

        if (count <= 7)
        {
            ZRoatate();
        }
        
        if (count <= 0)
        {
            transform.Rotate(new Vector3(0, 0, 0));
            count = 0;
            Rotation();
        }
    }

    public void XRoatate()
    {
        transform.Rotate(new Vector3(RotateSpeed, 0, 0));
    }

    public void YRoatate()
    {
        transform.Rotate(new Vector3(0, RotateSpeed, 0));
    }

    public void ZRoatate()
    {
        transform.Rotate(new Vector3(0, 0, RotateSpeed));
    }
    public void Rotation()
    {
        float x = -54.894f;
        float y = -145.54f;
        float z = -173.95f;
        this.transform.rotation = Quaternion.Euler(x, y, z);  //最終座標
    }
}