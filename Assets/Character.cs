using UnityEngine;

public class Character : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speed = 10.0f;
    public float jumpSpeed = 20.0f;
    public float rotationSpeed = 5.0f;

    private float ySpeed;

    CharacterController controller;

    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }

    void Start()
    {

    }

    void Update()
    {
        float rotation = Input.GetAxis("Mouse X");

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        ySpeed += gravity * Time.deltaTime;

        if (Controller.isGrounded) 
        {
            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
        }
        else 
        {
            vertical = 0.0f;
            horizontal = 0.0f;
        }

        Vector3 movement = new Vector3(horizontal * speed, ySpeed, vertical * speed);
        print(movement);

        Controller.Move(transform.TransformDirection(movement)  * Time.deltaTime);
        Controller.transform.Rotate(Vector3.up, rotation * rotationSpeed);
    }


}
