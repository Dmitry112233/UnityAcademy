using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedX = 200f;
    public Animator animator;
    public AudioSource jumpSound;
    public Transform respawnPosition;
    
    public bool isAlive = true;

    private bool isGround = false;
    private bool isFacingRight = true;
    private bool isJump = false;
    private float yBoundarie = -35f;

    private Rigidbody2D rigidBody;
    private Rigidbody2D RigidBody { get { return rigidBody = rigidBody ?? GetComponent<Rigidbody2D>(); }}

    public delegate void PlayerDieHandler();
    public event PlayerDieHandler NotifyPlayerDie;

    private void Start()
    {
        InputManager.GetInstance().NotifyHorizontalFixedUpdate += Move;

        InputManager.GetInstance().NotifyJump += UpdateIsJump;
        InputManager.GetInstance().NotifyHorizontalUpdate += SetAnimatorSpeedX;
        InputManager.GetInstance().NotifyHorizontalUpdate += CheckFlip;

        NotifyPlayerDie += CallRespawnCoroutine;
    }

    private void Update()
    {
        CheckYPosition();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(MyTags.Layers.Ground))
        {
            isGround = true;
        }
    }

    private void CheckFlip(float horizontal) 
    {
        if (horizontal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0f && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (isAlive) 
        {
            isFacingRight = !isFacingRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }

    private void Move(float horizontal) 
    {
        if (isAlive) 
        {
            RigidBody.AddForce(transform.right * horizontal * speedX);
        }
    }

    private void UpdateIsJump(bool isJumpInput)
    {
        if (isJumpInput)
        {
            if (!isGround) return;
            isJump = true;
        }
    }

    private void Jump()
    {
        if (isJump && isAlive)
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
            NotifyPlayerDie?.Invoke();
        }
    }

    private void CheckYPosition() 
    {
        if (isAlive && transform.position.y < yBoundarie)
        {
            PlayerDie();
        }
    }

    private void SetAnimatorSpeedX(float horizontal) 
    {
        animator.SetFloat(MyTags.Animator.SpeedX, Math.Abs(horizontal));
    }

    private void CallRespawnCoroutine() 
    {
        StartCoroutine(Respawn());
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
        }
    }
}