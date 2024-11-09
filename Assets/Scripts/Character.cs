using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int health;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took damage: {damage}, new health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");
    }
}
