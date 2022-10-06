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
    private bool isFacingRight = true;
    private bool isJump = false;
    private float yBoundarie = -35f;

    private Rigidbody2D rigidBody;
    private InputManager inputManager;

    public InputManager InputManager { get { return inputManager = inputManager ?? GetComponent<InputManager>(); } }
    private Rigidbody2D RigidBody { get { return rigidBody = rigidBody ?? GetComponent<Rigidbody2D>(); }}

    private void Update()
    {
        if (isAlive) 
        {
            SetAnimatorSpeedX();
            UpdateIsJump();
            CheckYPosition();
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
            Move();
            Jump();
            CheckFlip();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void CheckFlip() 
    {
        if (InputManager.Horizontal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (InputManager.Horizontal < 0f && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void Move() 
    {
        RigidBody.AddForce(transform.right * InputManager.Horizontal * speedX);
    }

    private void UpdateIsJump()
    {
        if (InputManager.IsJump)
        {
            if (!isGround) return;
            isJump = true;
        }
    }

    private void Jump()
    {
        if (isJump)
        {
            if (!isGround) return;
            jumpSound.Play();
            RigidBody.AddForce(new Vector2(0f, 500f));
            isJump = false;
            isGround = false;
        }
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

    private void CheckYPosition() 
    {
        if (transform.position.y < yBoundarie)
        {
            PlayerDie();
        }
    }

    private void SetAnimatorSpeedX() 
    {
        animator.SetFloat("speedX", Math.Abs(InputManager.Horizontal));
    }

    private IEnumerator Respawn()
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