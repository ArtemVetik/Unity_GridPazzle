using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    protected GameGrid _grid;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _grid = GetComponentInParent<GameGrid>();
    }

    private void Start()
    {
        transform.localScale = Vector2.one * _grid.WorldCellSize;
    }
}
