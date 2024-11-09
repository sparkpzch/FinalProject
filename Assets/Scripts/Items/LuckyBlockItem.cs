using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LuckyBlockItem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BoxCollider2D itemCollider;

    void Start()
    {
        itemCollider = GetComponent<BoxCollider2D>();
    }

    public abstract void ActivateItem();

    public abstract void OnTriggerEnter2D(Collider2D collision);
}
