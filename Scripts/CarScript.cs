using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public WheelCollider FL;
    public WheelCollider FR;
    public WheelCollider BL;
    public WheelCollider BR;


    private void FixedUpdate()
    {
        BL.motorTorque = Input.GetAxis("Vertical")*5000;
        BR.motorTorque = Input.GetAxis("Vertical")*5000;

        FL.steerAngle = Input.GetAxis("Horizontal")*60;
        FR.steerAngle = Input.GetAxis("Horizontal")*60;
    }
}
