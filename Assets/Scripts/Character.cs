using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected int health;

    public abstract void TakeDamage(int damage);

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");
    }
}
