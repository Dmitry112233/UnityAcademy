using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 0.05f;
    public PlayerController playerController;

    public Vector3 InitPosition { get; private set; }

    void Start()
    {
        InitPosition = transform.position;
    }

    void Update()
    {
        if (playerController.IsMovingRight) 
        {
            transform.position = transform.position + new Vector3(speed, 0f, 0f);
        }
        else 
        {
            transform.position = transform.position + new Vector3(speed, 0f, 0f) * -1;
        }
    }
}
