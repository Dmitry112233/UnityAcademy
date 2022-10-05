using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 200f;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private Transform respawnPosition;

    private bool _isGround = false;
    private bool _isJump = false;
    private float _horizontal = 0f;
    private bool _isFacingRight = true;

    public bool isAlive = true;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isAlive) 
        {
            _horizontal = Input.GetAxis("Horizontal");
            animator.SetFloat("speedX", Math.Abs(_horizontal));
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
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
        }
    }
}