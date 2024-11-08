using UnityEngine;
using System.Collections;

public class Player : Character
{
    [Header("Player Stats")]
    public float initialSpeed;
    public float moveSpeed;
    public float jumpForce;
    private bool isGrounded;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Jumping Variables")]
    [SerializeField] private float fallMultiplier = 1f; // Fall multiplier for jumping
    private float defaultSpeed;

    private float timeSinceLastMove;
    private float idleTimeLimit = 0.5f; // Time limit for idle state

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    void Start()
    {
        // 3 hearts
        health = 3;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;

        defaultSpeed = initialSpeed;
        moveSpeed = defaultSpeed;

        StartCoroutine(IncreaseSpeedAfterDelay(1.5f)); // Delay the speed increase
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (moveInput != 0)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            timeSinceLastMove = 0f; // Reset the timer
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove >= idleTimeLimit)
            {
                moveSpeed = defaultSpeed;
                StartCoroutine(IncreaseSpeedAfterDelay(1.5f)); // Restart the coroutine
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // animator.SetBool("isGrounded", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            // animator.SetBool("isGrounded", false);
        }
    }

    private IEnumerator IncreaseSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveSpeed = initialSpeed * 2;
        animator.SetBool("isRunning", true); // Set isRunning to true after speed increase
    }
}