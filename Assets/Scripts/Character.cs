using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 5.0f;

    private float gravity = -9.81f;
    private float ySpeed;

    CharacterController controller;
    public AudioSource audioSource;
    private AudioSource breath;

    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }
    public AudioSource Breath { get { return breath = breath ?? GetComponent<AudioSource>(); } }

    void Start()
    {
    }

    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        ySpeed += gravity * Time.deltaTime;
        if (Controller.isGrounded)
        {
            ySpeed = -0.5f;
        }

            Vector3 movement = new Vector3(horizontal, ySpeed, vertical);
        float magnitude = Math.Clamp(movement.magnitude, 0.0f, 1.0f);

        movement.Normalize();
        Controller.Move(transform.TransformDirection(movement) * magnitude * speed * Time.deltaTime);

        if(horizontal != 0.0f || vertical != 0.0f) 
        {
            if (!audioSource.isPlaying) 
            {
                audioSource.Play();
            }
        }
        if (!Breath.isPlaying)
        {
            Breath.Play();
        }

    }

    private void Rotate()
    {
        float horizontal = Input.GetAxis("Mouse X");
        Controller.transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
    }
}
