using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private BoxCollider2D enemyHitBox;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyHitBox = GetComponent<BoxCollider2D>();
        health = 1;
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
                Destroy(collision.gameObject); // Destroy the bullet after it hits the enemy
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
}
