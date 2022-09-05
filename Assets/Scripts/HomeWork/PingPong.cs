using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField]
    public Vector3 pointA = new Vector3(1.0f, 0.0f, 0.0f);
    [SerializeField]
    public Vector3 pointB = new Vector3(-1.0f, 0.0f, 0.0f);

    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float distance = 100.0f;
    [SerializeField]
    private float angle = 0.0f;
    [SerializeField]
    private float radius = 10.0f;

    private float initPosition;
    private Vector3 initPositionCircle;

    private bool isMoveRight;
    private bool isMoveLeft;

    void Start()
    {
        initPosition = transform.position.x;
        initPositionCircle = transform.position;
    }

    void Update()
    {
        Move();
        //MoveByCircle();
    }

    private void Move()
    {
        SetDirection();

        if (isMoveRight)
        {
            transform.position = transform.position + pointA * speed * Time.deltaTime;
        }
        else if (isMoveLeft)
        {
            transform.position = transform.position + pointB * speed * Time.deltaTime;
        }
    }

    private void SetDirection()
    {
        if (transform.position.x <= initPosition)
        {
            isMoveRight = true;
            isMoveLeft = false;
        }
        else if (transform.position.x >= initPosition + distance)
        {
            isMoveRight = false;
            isMoveLeft = true;
        }
    }

    private void MoveByCircle()
    {
        angle += Time.deltaTime;

        var x = Mathf.Cos(angle*speed) * radius;
        var y = Mathf.Sin(angle * speed) * radius;
        transform.position = new Vector3(x, y, transform.position.z) + initPositionCircle;
    }
}
