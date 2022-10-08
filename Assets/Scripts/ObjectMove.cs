using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float speed = 100;

    private Rigidbody rigidBody;
    private Rigidbody RigidBody { get { return rigidBody = rigidBody ?? GetComponent<Rigidbody>(); } }

    void Start()
    {
        var direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * transform.forward;
        RigidBody.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }
}
