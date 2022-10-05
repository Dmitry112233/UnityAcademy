using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMotor : MonoBehaviour
{
    HingeJoint2D hinge;

    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        if(transform.rotation.z <= 0.0f) 
        {
            var motor = hinge.motor;
            motor.motorSpeed = 300;

            hinge.motor = motor;
        }
        if(transform.rotation.z >= -180.0f) 
        {
            var motor = hinge.motor;
            motor.motorSpeed = -300;

            hinge.motor = motor;
        }
        
    }
}
