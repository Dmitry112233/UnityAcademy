using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public Button jumpButton;
    public FixedJoystick joystick;
    public float gravity = -9.81f;
    public float speed = 10.0f;
    public float jumpSpeed = 20.0f;
    public float rotationSpeed = 5.0f;

    private float ySpeed;
    private bool isJump;

    CharacterController controller;

    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }

    void Start()
    {
        jumpButton.onClick.AddListener(() =>
        {
            isJump = true;
        });

    }

    void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    private void Move()
    {
        float vertical = joystick.Vertical;

        float horizontal = joystick.Horizontal;

        Vector3 movement = new Vector3(horizontal, ySpeed, vertical);
        movement.Normalize();
        Controller.Move(transform.TransformDirection(movement)* speed * Time.deltaTime);
    }

    private void Rotate()
    {
        float touchX = 0.0f;

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
            if (touch.position.x > Screen.width / 2f && touch.phase == TouchPhase.Moved)
            {
                touchX = touch.deltaPosition.x / 3.0f * rotationSpeed * Time.deltaTime;
            }

            Controller.transform.Rotate(Vector3.up, touchX * rotationSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        ySpeed += gravity * Time.deltaTime;
        if (Controller.isGrounded)
        {
            ySpeed = -0.5f;
            if (isJump)
            {
                ySpeed = jumpSpeed;
                isJump = false;
            }
        }
    }
}
