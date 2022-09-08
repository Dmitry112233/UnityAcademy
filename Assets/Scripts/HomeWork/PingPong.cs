using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField]
    public Vector3 pointA = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]
    public Vector3 pointB = new Vector3(0.0f, 10.0f, 0.0f);
    [SerializeField]
    public float speed = 1f;

    private bool isMoveToPointB;

    void Start()
    {
        transform.position = pointA;
        isMoveToPointB = true;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if ((pointB - transform.position).magnitude != 0 && isMoveToPointB) 
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
        }
        else
        {
            isMoveToPointB = false;
        }
        if ((pointA - transform.position).magnitude != 0 && !isMoveToPointB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
        }
        else
        {
            isMoveToPointB = true;
        }
    }
}
