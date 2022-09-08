using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float xAngle;

    [SerializeField]
    private float yAngle;

    [SerializeField]
    private float zAngle;


    void Start()
    {
        
    }

    void Update()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
    }
}
