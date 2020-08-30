using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridObject : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    protected GameGrid _grid;

    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _grid = GetComponentInParent<GameGrid>();

        transform.localScale = Vector2.one * _grid.WorldCellSize;
    }
}
