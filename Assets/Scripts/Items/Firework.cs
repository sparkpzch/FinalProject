using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : LuckyBlockItem
{
    [Header("Components")]
    [SerializeField] private Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public override void ActivateItem()
    {
        // Bullet Add
        player.bullet += 1;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ActivateItem();
            Destroy(gameObject);
        }
    }
}
