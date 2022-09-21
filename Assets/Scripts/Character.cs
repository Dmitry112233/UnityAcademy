using System;
using System.Collections.Generic;
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
        float magnitude = Math.Clamp(movement.magnitude, 0.0f, 1.0f);

        movement.Normalize();
        Controller.Move(transform.TransformDirection(movement) * magnitude * speed * Time.deltaTime);
    }

    private void Rotate()
    {
        float touchX = 0.0f;

        if (Input.touchCount > 0)
        {
            var touch = new List<Touch>(Input.touches).Find(x => x.position.x > Screen.width / 2);
            touchX = touch.deltaPosition.x / 3.0f * rotationSpeed * Time.deltaTime;
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
