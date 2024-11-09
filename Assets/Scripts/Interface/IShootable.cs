using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{

    void Shoot(int bullet);

    void OnTriggerEnter2D(Collider2D collision);
}