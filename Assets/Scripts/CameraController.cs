using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float xRotation = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        RotateVertical();
    }

    private void RotateVertical()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY * rotationSpeed;
        xRotation = Mathf.Clamp(xRotation, -70f, 40f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
