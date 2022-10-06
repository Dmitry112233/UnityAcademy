using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedX = 200f;
    public Animator animator;
    public AudioSource jumpSound;
    public Transform respawnPosition;
    public ResetEnemies resetEnemies;
    public bool isAlive = true;

    private bool isGround = false;
    private bool isJump = false;
    private bool isFacingRight = true;
    private float yBoundarie = -35f;

    private Rigidbody2D rigidBody;
    private InputManager inputManager;

    public InputManager InputManager { get { return inputManager = inputManager ?? GetComponent<InputManager>(); } }
    private Rigidbody2D RigidBody { get { return rigidBody = rigidBody ?? GetComponent<Rigidbody2D>(); }}

    void Start()
    {
    }

    private void Update()
    {
        if (isAlive) 
        {
            animator.SetFloat("speedX", Math.Abs(InputManager.Horizontal));
            
            if (InputManager.IsJump)
            {
                Jump();
            }
            if(transform.position.y < yBoundarie) 
            {
                PlayerDie();
            }
        }
        else 
        {
            StartCoroutine(Respawn());
        }
    }

    private void FixedUpdate()
    {
        if (isAlive) 
        {
            RigidBody.AddForce(transform.right * InputManager.Horizontal * speedX);

            if (isJump)
            {
                RigidBody.AddForce(new Vector2(0f, 500f));
                isGround = false;
                isJump = false;
            }
            if (InputManager.Horizontal > 0f && !isFacingRight)
            {
                Flip();
            }
            else if (InputManager.Horizontal < 0f && isFacingRight)
            {
                Flip();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    public void Jump()
    {
        if (!isGround) return;
        isJump = true;
        jumpSound.Play();
    }

    public void PlayerDie()
    {
        if (isAlive)
        {
            isAlive = false;
            transform.Rotate(transform.forward, 90.0f);
            animator.enabled = false;
        }
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        if (!isAlive)
        {
            isAlive = true;
            transform.Rotate(transform.forward, -90.0f);
            animator.enabled = true;
            transform.position = respawnPosition.position;
            resetEnemies.RespawnEnemies();
        }
    }
}