using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 200f;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource jumpSound;

    private bool _isGround = false;
    private bool _isJump = false;
    private float _horizontal = 0f;
    private bool _isFacingRight = true;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        _horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("speedX", Math.Abs(_horizontal));
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * Time.fixedDeltaTime, _rb.velocity.y);

        if (_isJump)
        {
            _rb.AddForce(new Vector2(0f, 500f));
            _isGround = false;
            _isJump = false;
        }
        if (_horizontal > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontal < 0f && _isFacingRight)
        {
            Flip();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    public void Jump()
    {
        if (!_isGround) return;

        _isJump = true;
        jumpSound.Play();

    }
}
