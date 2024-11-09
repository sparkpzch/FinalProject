using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    Transform BulletSpawnPoint { get; set; }
    GameObject BulletPrefab { get; set; }

    float CoolDown { get; set; }
    float NextFireTime { get; set; }

    void Shoot();
}