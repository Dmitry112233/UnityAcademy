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
        if (Input.touchCount > 0)
        {
            var touch = new List<Touch>(Input.touches).Find(x => x.position.x > Screen.width / 2);
            
            float mouseY = touch.deltaPosition.y / 3.0f * rotationSpeed * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, 0f, 40f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
