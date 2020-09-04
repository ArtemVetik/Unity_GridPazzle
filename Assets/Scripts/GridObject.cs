using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridObject : MonoBehaviour
{
    private GameGrid _grid;

    private void Awake()
    {
        _grid = GetComponentInParent<GameGrid>();
        transform.localScale = Vector2.one * _grid.WorldCellSize;
    }
}
