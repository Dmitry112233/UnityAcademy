using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private GameObject bulletPrefab;

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
        Debug.Log(Input.GetAxis("Vertical"));

        if (forwardForce != 0.0f)
        {
            myBody.velocity = myBody.transform.forward * forwardForce;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * bulletSpeed, ForceMode.Impulse);
        }
    }
}