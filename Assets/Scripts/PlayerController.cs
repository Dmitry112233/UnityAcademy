using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private GameObject allBullet;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Transform bulletInitPosition;

    private Rigidbody myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float sideForce = Input.GetAxis("Horizontal") * rotationSpeed;

        if (sideForce != 0.0f)
        {
            myBody.angularVelocity = new Vector3(0.0f, sideForce, 0.0f);
        }

        float forwardForce = Input.GetAxis("Vertical") * movementSpeed;

        if (forwardForce != 0.0f)
        {
            myBody.velocity = myBody.transform.forward * forwardForce;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var direction = (transform.forward + transform.up);
            direction.Normalize();
            var bullet = Instantiate(bulletPrefab, bulletInitPosition.position, Quaternion.identity);
            bullet.transform.SetParent(allBullet.transform);
            bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }
    }
}