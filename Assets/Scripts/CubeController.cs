using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed;

    private Rigidbody myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float sideForce = Input.GetAxis("Horizontal") * rotationSpeed;
        
        if(sideForce != 0.0f) 
        {
            myBody.angularVelocity = new Vector3(0.0f, sideForce, 0.0f);
        }

        float forwardForce = Input.GetAxis("Vertical") * movementSpeed;

        if(forwardForce != 0.0f) 
        {
            myBody.velocity = myBody.transform.forward * forwardForce;
        }
    }
}