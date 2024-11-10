using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasePlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float chaseRange = 5f;
    public float chaseSpeed = 2f;
    public float detectionDistance = 3f;
    private bool movingRight = true;
    private bool chasingPlayer = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange())
        {
            chasingPlayer = true;
        } 

        else if (chasingPlayer && !PlayerInRange())
        {
            chasingPlayer = false;
        }

        if (chasingPlayer)
        {
            ChasePlayer();
        }

        else
        {
            OnGuard();
        }
    }

    bool PlayerInRange()
    {
        if (Vector2.Distance(transform.position, player.position) <= chaseRange)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, chaseRange);
            if (hit.collider != null && hit.collider.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    void OnGuard()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        rb.velocity = new Vector2 (direction.x * chaseSpeed, direction.y * chaseSpeed);

        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, detectionDistance);
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position + Vector3.down, direction, detectionDistance);

        if (wallHit.collider != null || groundHit.collider == null)
        {
            movingRight = !movingRight;
            transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
        }
    }

    void ChasePlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToPlayer.x * chaseSpeed, rb.velocity.y);

        transform.localScale = new Vector3(directionToPlayer.x > 0 ? 1 : -1, 1, 1);
    }
}
