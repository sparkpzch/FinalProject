using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int health;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public abstract void TakeDamage(int damage);

    public abstract void Die();
}