using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    [SerializeField, ReadOnly] private GameObject[] borderObject;

    void Start()
    {
        borderObject = GetComponentsInChildren<Transform>()
            .Where(x => x.gameObject.name == "DeathBorder")
            .Select(x => x.gameObject)
            .ToArray();
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            Destroy(player.gameObject);
        }
    }

}