using Assets.Scripts;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float xRotation = 0.0f;

    private InputManager inputManager;

    public InputManager InputManager { get { return inputManager = inputManager ?? GetComponent<InputManager>(); } }

    void Update()
    {
        RotateVertical();
    }

    private void RotateVertical()
    {
        xRotation -= InputManager.MouseVerticalInput * rotationSpeed;
        xRotation = Mathf.Clamp(xRotation, -70f, 40f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
