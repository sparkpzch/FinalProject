using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Character
{
    [Header("Player Stats")]
    [SerializeField] private int initialHealth = 2;
    public int bullet = 0;

    [Header("Player Controls")]
    public float initialSpeed;
    public float moveSpeed;
    public float jumpForce;
    private bool isGrounded;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Sprites Animation")]
    [SerializeField] private Sprite onJumping;
    [SerializeField] private Sprite onFalling;

    [Header("UI Components")]
    public Image healthBar;
    public Sprite[] healthBarSprites;

    public TMPro.TextMeshProUGUI bulletText;

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
        // Set initial health
        health = initialHealth;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;

        defaultSpeed = initialSpeed;
        moveSpeed = defaultSpeed;

        StartCoroutine(IncreaseSpeedAfterDelay(1.5f)); // Delay the speed increase
    }

    void Update()
    {
        // Update the Health Bar
        healthBar.sprite = healthBarSprites[health];

        // Update the Bullet Text
        bulletText.text = "x " + bullet;

        float moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Set the sprite based on the player's vertical velocity
        if (rb.velocity.y > 0.1f && !isGrounded)
        {
            spriteRenderer.sprite = onJumping;
        }
        else if (rb.velocity.y < -0.1f && !isGrounded)
        {
            spriteRenderer.sprite = onFalling;
        }

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            animator.SetBool("isRunning", true);
            timeSinceLastMove = 0f; // Reset the timer
        }
        else
        {
            animator.SetBool("isRunning", false);
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove >= idleTimeLimit)
            {
                moveSpeed = defaultSpeed;
                StartCoroutine(IncreaseSpeedAfterDelay(1.5f)); // Restart the coroutine
            }
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Reset animation speed to 1 if not increasing speed
        if (moveSpeed == initialSpeed && animator != null)
        {
            animator.speed = 1;
            animator.SetBool("isRunning", false);
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
        if (animator != null)
        {
            animator.speed = 3;
            animator.SetBool("isRunning", true); // Set isRunning to true after speed increase
        }
    }
}