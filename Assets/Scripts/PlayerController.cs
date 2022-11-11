using Assets.Scripts.Data;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;

    public float rotationSpeed;

    public GameObject projectilePrefab;

    public Transform projectileInitPosition;

    public float upVectorY = 1.0f;

    private Rigidbody myBody;

    private float sideForce = 0.0f;
    
    private float forwardForce = 0.0f;
    
    private bool isThrowWithAngle;

    private bool isShot;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ReadKeyBoard();
    }

    private void FixedUpdate()
    {
        Move();
        Shot();
    }

    private void ReadKeyBoard() 
    {
        sideForce = Input.GetAxis("Horizontal") * rotationSpeed;
        forwardForce = Input.GetAxis("Vertical") * movementSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShot = true;

            switch (projectilePrefab.tag)
            {
                case "Grenade":
                    isThrowWithAngle = true;
                    break;
                case "TennisBall":
                    isThrowWithAngle = true;
                    break;
                case "Bullet":
                    isThrowWithAngle = false;
                    break;
            }
        }
    }

    private void Move()
    {
        if (sideForce != 0.0f)
        {
            myBody.angularVelocity = new Vector3(0.0f, sideForce, 0.0f);
        }
        if (forwardForce != 0.0f)
        {
            myBody.MovePosition(transform.position + transform.forward * forwardForce * Time.fixedDeltaTime);
        }
    }

    private void Shot() 
    {
        if (isShot) 
        {
            projectilePrefab.GetComponent<Projectile>().Shot(projectileInitPosition, isThrowWithAngle);
            isShot = false;
        }
    }
}