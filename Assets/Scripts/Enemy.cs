using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private CircleCollider2D enemyHitBox;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyHitBox = GetComponent<CircleCollider2D>();
        health = 1;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(1);
        }
    }
}
