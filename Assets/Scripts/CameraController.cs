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
            Touch touch = Input.GetTouch(0);

            if (touch.position.x > Screen.width / 2f)
            {
                touch = Input.GetTouch(0);
            }
            else if (Input.touchCount > 1 && Input.GetTouch(1).position.x > Screen.width / 2f)
            {
                touch = Input.GetTouch(1);
            }
            if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width / 2f)
            {
                float mouseY = touch.deltaPosition.y / 3.0f * rotationSpeed * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, 0f, 40f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }
        }
    }
}
