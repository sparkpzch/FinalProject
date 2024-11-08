using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LuckyBlockScript : MonoBehaviour
{
    [Header("Lucky Block Components")]
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private bool isLuckyBlockActive;
    [SerializeField] private GameObject luckyBlockAfterActive;

    [Header("Lucky Block Random Items")]
    public GameObject[] randomItems;

    void Start()
    {
        isLuckyBlockActive = false;
        playerCollider = GetComponentInChildren<BoxCollider2D>();
    }

    public GameObject GetRandomItem()
    {
        if (randomItems == null || randomItems.Length == 0)
        {
            Debug.LogWarning("No random items available.");
            return null;
        }

        int randomIndex = Random.Range(0, randomItems.Length);
        return randomItems[randomIndex];
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isLuckyBlockActive)
        {
            isLuckyBlockActive = true;

            // Change picture of the lucky block
            GetComponent<SpriteRenderer>().sprite = luckyBlockAfterActive.GetComponent<SpriteRenderer>().sprite;

            // Spawn a random item above the lucky block
            GameObject randomItem = GetRandomItem();
            if (randomItem != null)
            {
                Instantiate(randomItem, transform.position + new Vector3(0, 0.15f, 0), Quaternion.identity);
            }
        }
    }
}
