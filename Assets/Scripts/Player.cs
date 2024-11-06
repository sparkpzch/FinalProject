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
    // private Animator animator;

    [Header("Jumping Variables")]
    [SerializeField] private float fallMultiplier = 2.5f; // Fall multiplier for jumping
    private float defaultSpeed;

    private float timeSinceLastMove;
    private float idleTimeLimit = 0.5f; // Time limit for idle state

    void Start()
    {
        // 3 hearts
        health = 3;

        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();

        // Lock the rotation of the Rigidbody2D
        rb.freezeRotation = true;

        // Start with the initial slower speed
        defaultSpeed = initialSpeed;
        moveSpeed = defaultSpeed;


        StartCoroutine(IncreaseSpeedAfterDelay(1f)); // Delay the speed increase
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            // animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            timeSinceLastMove = 0f; // Reset the timer
        }
        else
        {
            // animator.SetBool("isRunning", false);
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove >= idleTimeLimit)
            {
                moveSpeed = defaultSpeed;
                StartCoroutine(IncreaseSpeedAfterDelay(1f)); // Restart the coroutine
            }
        }

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // animator.SetTrigger("jump");
        }

        if (!isGrounded && rb.velocity.y < 0)
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
    }
}