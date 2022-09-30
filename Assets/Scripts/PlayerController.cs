using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsMovingRight { get; private set; }

    private SpriteRenderer spriteRender;
    private SpriteRenderer SpriteRender { get { return spriteRender = spriteRender ?? GetComponent<SpriteRenderer>(); } }

    private float playerPositionXRight = -93.58f;
    private float playerPositionXLeft = -96.29f;

    void Start()
    {
        IsMovingRight = true;
    }

    void Update()
    {
        if (IsMovingRight) 
        {
            var position = new Vector3(playerPositionXRight, transform.position.y, transform.position.z);
            transform.position = position;
        }
        else 
        {
            var position = new Vector3(playerPositionXLeft, transform.position.y, transform.position.z);
            transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SpriteRender.flipX = !SpriteRender.flipX;
            IsMovingRight = !IsMovingRight;
        }
    }
}
