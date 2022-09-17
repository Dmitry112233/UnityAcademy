using UnityEngine;

public class Character : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speed = 10.0f;
    public float jumpSpeed = 20.0f;

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


        Debug.Log("Sped jump" + jumpSpeed);
        Debug.Log("IsGrounded:" + Controller.isGrounded);

        ySpeed += gravity * Time.deltaTime;

        if (controller.isGrounded) 
        {
            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
        }

        Vector3 movement = new Vector3(horizontal * speed, ySpeed, vertical * speed);

        Controller.Move(transform.TransformDirection(movement)  * Time.deltaTime);
        Controller.transform.Rotate(Vector3.up, rotation);
    }


}
