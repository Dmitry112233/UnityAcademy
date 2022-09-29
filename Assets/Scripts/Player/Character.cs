using Assets.Scripts;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 1000.0f;
    public AudioSource steps;
    public AudioSource breath;

    private float gravity = -9.81f;
    private float ySpeed;
    private CharacterController controller;
    private InputManager inputManager;

    public InputManager InputManager { get { return inputManager = inputManager ?? GetComponent<InputManager>(); } }
    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }

    void Start()
    {
    }

    void Update()
    {
        RecalculateGravity();
        Move();
        Rotate();
        PlayBreathSound();
        PlayStepsSound();
    }

    private void Move()
    {
        Vector3 movement = InputManager.MovementInput;
        movement.y = ySpeed;

        float magnitude = Math.Clamp(movement.magnitude, 0.0f, 1.0f);
        movement.Normalize();

        Controller.Move(transform.TransformDirection(movement) * magnitude * speed * Time.deltaTime);
    }

    private void RecalculateGravity()
    {
        ySpeed += gravity * Time.deltaTime;

        if (Controller.isGrounded)
        {
            ySpeed = -0.5f;
        }
    }

    private void Rotate()
    {
        Controller.transform.Rotate(Vector3.up, InputManager.MouseHorizontalInput * rotationSpeed * Time.deltaTime);
    }

    private void PlayBreathSound() 
    {
        if (!breath.isPlaying)
        {
            breath.Play();
        }
    }

    private void PlayStepsSound() 
    {
        Vector3 movement = InputManager.MovementInput;

        if (movement.x != 0.0f || movement.z != 0.0f)
        {
            if (!steps.isPlaying)
            {
                steps.Play();
            }
        }
    }
}
