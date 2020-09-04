using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CellType
{
    Empty, Item, Filled, Player,
}

[Serializable]
public class LevelData
{
    [SerializeField, HideInInspector] private int _width;
    [SerializeField, HideInInspector] private int _height;
    [SerializeField, HideInInspector] private CellType[] _grid;

    public Vector2Int Size => new Vector2Int(_width, _height);
    public CellType this[Vector2Int position] => this[position.y, position.x];
    public CellType this[int y, int x] => _grid[y * _width + x];

    public LevelData(int width = 0, int height = 0)
    {
        _width = width;
        _height = height;
        _grid = new CellType[_height * _width];
    }

    public void Resize(int newWidth, int newHeight)
    {
        if (_width == newWidth && _height == newHeight)
            return;

        _width = newWidth;
        _height = newHeight;
        Array.Resize(ref _grid, _width * _height); // TODO: custom resize method
    }

    public Vector2Int GetPlayerPosition()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (this[y, x] == CellType.Player)
                    return new Vector2Int(x, y);
            }
        }

        return Vector2Int.zero;
    }

    public int GetItemCount()
    {
        int count = 0;
        foreach (CellType type in _grid)
        {
            if (type == CellType.Item)
                count++;
        }

        return count;
    }
}