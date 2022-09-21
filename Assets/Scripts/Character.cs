using System;
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
    public Transform transformCamera;

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
        Jump();
    }

    private void Move()
    {
        float vertical = joystick.Vertical;
        float horizontal = joystick.Horizontal;

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        float magnitude = Math.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection = Quaternion.AngleAxis(transformCamera.rotation.eulerAngles.y, Vector3.up) * movementDirection;

        Vector3 velocity = movementDirection * magnitude * speed;
        velocity.y = ySpeed;

        movementDirection.Normalize();

        Controller.Move(velocity * Time.deltaTime);

        if(movementDirection != Vector3.zero) 
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
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
