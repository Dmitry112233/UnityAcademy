using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 5.0f;

    CharacterController controller;

    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }

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

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        float magnitude = Math.Clamp(movement.magnitude, 0.0f, 1.0f);

        movement.Normalize();
        Controller.Move(transform.TransformDirection(movement) * magnitude * speed * Time.deltaTime);
    }

    private void Rotate()
    {
        float horizontal = Input.GetAxis("Mouse X");
        print(horizontal);
        Controller.transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
    }
}
