using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasePlayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    [Header("Chase Settings")]
    public float chaseRange = 5f;
    public float chaseSpeed = 2f;
    public float detectionDistance = 3f;
    public float jumpForce = 2f;
    public float jumpCooldown = 2f;

    [Header("Logic")]
    private bool movingRight = true;
    private bool chasingPlayer = false;
    private bool isGrounded = false;
    private float jumpCooldownTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (PlayerInRange())
        {
            chasingPlayer = true;
            animator.SetBool("isChasing", true);
        }
        else if (!PlayerInRange())
        {
            chasingPlayer = false;
            animator.SetBool("isChasing", false);
        }

        if (chasingPlayer)
        {
            ChasePlayer();
        }

        if (jumpCooldownTimer > 0)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    private bool PlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= chaseRange;
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, direction.y * chaseSpeed);

        if ((direction.x > 0 && !movingRight) || (direction.x < 0 && movingRight))
        {
            Flip();
        }

        if (isGrounded && Mathf.Abs(direction.y) > 0.5f && jumpCooldownTimer <= 0)
        {
            Jump();
            jumpCooldownTimer = jumpCooldown;
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
