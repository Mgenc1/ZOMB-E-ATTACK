using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [HideInInspector] public float MouseY;
    [Range(0, 100)] public float xMax;
    public float xMin;

    public float MauseSpeedX;
    private float xClamp;

    
    
    void Update()
    {
        xClamp += -Input.GetAxis("Mouse Y") * MauseSpeedX; 
        xClamp = Mathf.Clamp(xClamp, xMin, xMax);
        transform.localEulerAngles = new Vector3(xClamp, 0f, 0f); 
    }

}
