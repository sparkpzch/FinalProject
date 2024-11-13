using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.Mathematics;

public class Player : Character, IShootable
{
    [Header("Player Stats")]
    private int initialHealth = 2;
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

    [Header("UI Components")]
    public Image healthBar;
    public Sprite[] healthBarSprites;
    public TMPro.TextMeshProUGUI bulletText;
    public GameObject restartMenu;

    [Header("Jumping Variables")]
    [SerializeField] private float fallMultiplier = 1f;
    private float defaultSpeed;
    private float timeSinceLastMove;
    private float idleTimeLimit = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [field: SerializeField] public Transform BulletSpawnPoint { get; set; }
    [field: SerializeField] public GameObject BulletPrefab { get; set; }
    [field: SerializeField] public float CoolDown { get; set; }
    [field: SerializeField] public float NextFireTime { get; set; }


    void Start()
    {
        gameObject.tag = "Player";
        Health = initialHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;

        defaultSpeed = initialSpeed;
        moveSpeed = defaultSpeed;
        animator.speed = 1;
        NextFireTime = 2f;

        StartCoroutine(IncreaseSpeedAfterDelay(1.5f));
        restartMenu.SetActive(false);
    }

    void Update()
    {
        Shoot();
        UpdateUI();
        HandleMovement();
        HandleJumping();
        HandleIdleState();
    }

    void FixedUpdate()
    {
        CoolDown += Time.deltaTime;
    }

    public void Shoot()
    {
        if (bullet > 0 && Input.GetKeyDown(KeyCode.Space) && CoolDown >= NextFireTime)
        {
            if (BulletPrefab != null && BulletSpawnPoint != null)
            {
                bullet--;
                GameObject newBullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, quaternion.identity);
                newBullet.GetComponent<Weapon>().Init(1, this);
                newBullet.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
                CoolDown = 0;
                NextFireTime = CoolDown + 2f;
            }
            else
            {
                Debug.LogError("BulletPrefab or BulletSpawnPoint is not assigned");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private IEnumerator IncreaseSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveSpeed = initialSpeed * 2;
        if (animator != null)
        {
            animator.speed = 1;
            animator.SetBool("isRunning", true);
        }
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        animator.SetTrigger("Hurt");
        if (Health <= 0)
        {
            Die();
        }
    }

    private void UpdateUI()
    {
        if (Health <= 0)
        {
            healthBar.sprite = healthBarSprites[0];
        }
        else
        {
            healthBar.sprite = healthBarSprites[Health];
        }
        bulletText.text = "x " + bullet;
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            animator.SetBool("isRunning", true);
            timeSinceLastMove = 0f;
        }
        else
        {
            animator.SetBool("isRunning", false);
            timeSinceLastMove += Time.deltaTime;
        }
    }

    private void HandleJumping()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("isJump", true);
            // FlipPlayer();
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            // FlipPlayer();
        }

        if (rb.velocity.y > 0.1f && !isGrounded)
        {
            animator.SetBool("isJump", true);
            // FlipPlayer();
        }
        else if (rb.velocity.y < -0.1f && !isGrounded)
        {
            animator.SetBool("isJump", false);
        }
        else if (isGrounded)
        {
            animator.SetBool("isJump", false);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
    }

    // private void FlipPlayer()
    // {
    //     transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    // }

    private void HandleIdleState()
    {
        if (timeSinceLastMove >= idleTimeLimit)
        {
            moveSpeed = defaultSpeed;
            animator.speed = 1;
            StartCoroutine(IncreaseSpeedAfterDelay(1.5f));
        }

        if (moveSpeed == initialSpeed && animator != null)
        {
            animator.speed = 1;
            animator.SetBool("isRunning", false);
        }
    }

    public override void Die()
    {
        animator.SetTrigger("Die");
        rb.velocity = Vector2.up * jumpForce;
        rb.gravityScale = 2;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.7f);
        restartMenu.SetActive(true);
    }
}