using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    protected IShootable shooter;

    public float range = 10f;
    public float nextTimeToFire = 0f;

    private Vector3 initialPosition;

    public abstract void OnHitWith(Character character);
    public abstract void Move();

    public void Init(int damage, IShootable _owner)
    {
        Damage = damage;
        shooter = _owner;
        initialPosition = transform.position;
    }

    public int GetShootDirection()
    {
        float ShootDir = shooter.BulletSpawnPoint.position.x - shooter.BulletSpawnPoint.parent.position.x;
        if (ShootDir > 0)
        {
            return 1;
        }
        else return -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Character character = other.gameObject.GetComponent<Character>();
            if (character != null)
            {
                OnHitWith(character);
            }
        }
    }

    private void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }
}