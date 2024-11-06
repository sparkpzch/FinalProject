using UnityEngine;

public class Player : Character
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator animator;

    // Add a new field for the fall multiplier
    [SerializeField]
    private float fallMultiplier = 2.5f;

    void Start()
    {
        // 3 hearts
        health = 3;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Lock the rotation of the Rigidbody2D
        rb.freezeRotation = true;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("jump");
        }

        // Apply additional downward force when falling
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
            animator.SetBool("isGrounded", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    }
}