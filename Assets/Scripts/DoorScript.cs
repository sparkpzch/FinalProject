using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private GameObject winUI;

    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                gameObject.SetActive(false);
                Destroy(player.gameObject);
                winUI.SetActive(true);
            }
        }
    }
}
