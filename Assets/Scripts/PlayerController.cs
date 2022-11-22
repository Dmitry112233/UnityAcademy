using Assets.Scripts.Data;
using Assets.Scripts.Projectiles;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;

    public float rotationSpeed;

    public ProjectileType projectileType;

    public Transform projectileInitPosition;

    public float upVectorY = 1.0f;

    private Rigidbody myBody;

    private float sideForce = 0.0f;
    
    private float forwardForce = 0.0f;

    private bool isShot;

    void Start()
    {
        projectileType = ProjectileType.TennisBall;
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
            var projectile = InstatiateProjectile();
            
            Vector3 direction = new Vector3();

            switch (projectileType)
            {
                case ProjectileType.Grenade:
                    direction = Quaternion.AngleAxis(-45.0f, projectileInitPosition.right) * projectileInitPosition.forward;
                    break;
                case ProjectileType.TennisBall:
                    direction = Quaternion.AngleAxis(-45.0f, projectileInitPosition.right) * projectileInitPosition.forward;
                    break;
                case ProjectileType.Bullet:
                    direction = projectileInitPosition.forward;
                    break;
            }
    
            direction.Normalize();

            projectile.GetComponent<Projectile>().Shot(direction);

            isShot = false;
        }
    }

    private GameObject InstatiateProjectile() 
    {
        var projectile = PoolObjectManager.Instance.GetPooledObject(projectileType);
        projectile.GetComponent<Projectile>().Init(new PoolInitProjectileData(projectileInitPosition));
        return projectile;
    }
}