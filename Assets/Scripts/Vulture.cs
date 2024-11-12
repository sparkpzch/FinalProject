using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : Character
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private BoxCollider2D enemyHitBox;

    [Header("Enemy Settings")]
    public BoxCollider2D enemyFeetCollider;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        health = 1;

        // Ignore collision between player and enemyFeetCollider
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(enemyFeetCollider, playerCollider);
            }
        }
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletScript bullet = collision.gameObject.GetComponent<bulletScript>();
            if (bullet != null)
            {
                TakeDamage(bullet.Damage);
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }
        rb2d.velocity = Vector2.zero;
        enemyHitBox.enabled = false;
        Destroy(gameObject, .5f);
    }
}
