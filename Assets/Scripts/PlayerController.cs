using Assets.Scripts.Data;
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
    
    private bool isThrowWithAngle;
    
    private bool isThrowForward;

    private bool isShoot;

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
            isShoot = true;

            switch (projectilePrefab.tag)
            {
                case "Grenade":
                    isThrowWithAngle = true;
                    break;
                case "TennisBall":
                    isThrowWithAngle = true;
                    break;
                case "Bullet":
                    isThrowForward = true;
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
        if (isShoot) 
        {
            var bullet = Instantiate(projectilePrefab, projectileInitPosition.position, Quaternion.identity);
            bullet.transform.SetParent(allProjectiles.transform);
            bullet.GetComponent<Projectile>().PlayerTransform = transform;

            if (isThrowWithAngle)
            {
                var direction = Quaternion.AngleAxis(-45.0f, transform.right) * transform.forward;
                direction.Normalize();
                bullet.GetComponent<Rigidbody>()?.AddForce(direction * bullet.GetComponent<Projectile>().speed, ForceMode.Impulse);
                isThrowWithAngle = false;
            }
            if (isThrowForward)
            {
                bullet.GetComponent<Rigidbody>()?.AddForce(transform.forward * bullet.GetComponent<Projectile>().speed, ForceMode.Impulse);
                isThrowForward = false;
            }

            isShoot = false;
            AudioManager.Instance.PlayAudio(MyTags.AudioSourceNames.Shot);
        }
    }
}