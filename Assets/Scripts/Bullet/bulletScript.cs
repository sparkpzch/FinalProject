using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : Weapon
{
    [SerializeField] private float speed;

    private void Start()
    {
        Damage = 1;
        speed = 3f * GetShootDirection();
    }

    void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        float newX = transform.position.x + speed * Time.deltaTime;
        float newY = transform.position.y;

        Vector2 newPos = new Vector2(newX, newY);
        transform.position = newPos;
    }

    public override void OnHitWith(Character character)
    {
        if (character != null && character.CompareTag("Enemy"))
        {
            character.TakeDamage(Damage);
        }
    }
}
