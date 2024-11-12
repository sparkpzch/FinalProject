using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : GetItem
{
    [Header("Components")]
    [SerializeField] private Player player;

    public override void ActivateItem()
    {
        if (player.Health < 2)
        {
            player.Health += 1;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
}
