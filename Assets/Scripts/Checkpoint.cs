using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Checkpoint : GridObject
{
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _collider.radius = transform.localScale.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
