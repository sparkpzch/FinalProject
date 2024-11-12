using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : GetItem
{
    [Header("Components")]
    [SerializeField] private Player player;

    public override void ActivateItem()
    {
        player.bullet += 1;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
}
