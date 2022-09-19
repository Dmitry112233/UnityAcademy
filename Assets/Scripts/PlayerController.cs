using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;

    public float rotationSpeed;

    public GameObject allProjectiles;

    public GameObject projectilePrefab;

    public Transform projectileInitPosition;

    public float upVectorY = 1.0f;

    private Rigidbody myBody;

    private float sideForce = 0.0f;
    
    private float forwardForce = 0.0f;
    
    private bool IsThrowWithAngle;
    
    private bool IsThrowForward;

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
        Shoot();
    }

    private void ReadKeyBoard() 
    {
        sideForce = Input.GetAxis("Horizontal") * rotationSpeed;
        forwardForce = Input.GetAxis("Vertical") * movementSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (projectilePrefab.tag)
            {
                case "Grenade":
                    IsThrowWithAngle = true;
                    break;
                case "TennisBall":
                    IsThrowWithAngle = true;
                    break;
                case "Bullet":
                    IsThrowForward = true;
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

    private void Shoot() 
    {
        if (IsThrowWithAngle) 
        {
            var upVector = new Vector3(0.0f, upVectorY, 0.0f);
            var direction = (transform.forward + upVector);
            direction.Normalize();
            var bullet = Instantiate(projectilePrefab, projectileInitPosition.position, Quaternion.identity);
            bullet.transform.SetParent(allProjectiles.transform);
            bullet.GetComponent<Rigidbody>()?.AddForce(direction * bullet.GetComponent<Projectile>().speed, ForceMode.Impulse);
            IsThrowWithAngle = false;
        }
        if (IsThrowForward) 
        {
            var bullet = Instantiate(projectilePrefab, projectileInitPosition.position, Quaternion.identity);
            bullet.transform.SetParent(allProjectiles.transform);
            bullet.GetComponent<Rigidbody>()?.AddForce(transform.forward * bullet.GetComponent<Projectile>().speed, ForceMode.Impulse);
            IsThrowForward = false;
        }
    }
}