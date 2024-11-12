using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<GetItem>() == null)
        {
            gameObject.AddComponent<GetItem>();
        }
        GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetItem item = GetComponent<GetItem>();
            item.ActivateItem();
            Destroy(gameObject);
        }
    }
}
