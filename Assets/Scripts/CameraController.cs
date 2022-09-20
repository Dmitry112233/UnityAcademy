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
        Move();
    }

    private void Move() 
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x > Screen.width / 2f)
                {
                    float mouseY = touch.deltaPosition.y / 3.0f * rotationSpeed * Time.deltaTime;

                    xRotation -= mouseY;
                    xRotation = Mathf.Clamp(xRotation, -45f, 45f);

                    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                }
            }
        }
    }
}
