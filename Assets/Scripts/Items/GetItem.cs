using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GetItem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BoxCollider2D itemCollider;

    void Start()
    {
        itemCollider = GetComponent<BoxCollider2D>();
    }

    public abstract void ActivateItem();
}
