using Assets.Scripts;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedX = 200f;
    public Animator animator;
    public AudioSource jumpSound;

    public bool isAlive = true;

    private bool isGround = false;
    private bool isFacingRight = true;
    private bool isJump = false;

    private Rigidbody2D rigidBody;
    private Rigidbody2D RigidBody { get { return rigidBody = rigidBody ?? GetComponent<Rigidbody2D>(); } }

    private void Start()
    {
        InputManager.GetInstance().NotifyHorizontalFixedUpdate += Move;
        InputManager.GetInstance().NotifyJump += UpdateIsJump;
        InputManager.GetInstance().NotifyHorizontalUpdate += SetAnimatorSpeedX;
        InputManager.GetInstance().NotifyHorizontalUpdate += CheckFlip;

    }

    private void Update()
    {
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

    private void SetAnimatorSpeedX(float horizontal)
    {
        animator.SetFloat(MyTags.Animator.SpeedX, Math.Abs(horizontal));
    }

}
