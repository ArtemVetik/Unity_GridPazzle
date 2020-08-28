using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class LevelData
{
    [SerializeField, HideInInspector] private int _width;
    [SerializeField, HideInInspector] private int _height;
    [SerializeField, HideInInspector] private CellData[] _grid;

    public Vector2Int Size => new Vector2Int(_width, _height);
    public CellData this[int y, int x]
    {
        get
        {
            return _grid[y * _width + x];
        }
        set
        {
            if (value._type == CellType.Player)
                RemoveAllPlayers();
            _grid[y * _width + x] = value;
        }
    }

    public LevelData(int width = 0, int height = 0)
    {
        _width = width;
        _height = height;
        _grid = new CellData[_height * _width];
    }

    public Vector2Int GetPlayerPosition()
    {
        for (int y = 0; y < _height; y++)
            for (int x = 0; x < _width; x++)
                if (this[y, x]._type == CellType.Player)
                    return new Vector2Int(x, y);

        return Vector2Int.zero;
    }

    public void Resize(int newWidth, int newHeight)
    {
        if (_width == newWidth && _height == newHeight)
            return;

        _width = newWidth;
        _height = newHeight;
        Array.Resize(ref _grid, _width * _height);
    }

    public void InitPositions(Vector2[,] positions)
    {
        if (positions.Length != _grid.Length)
            throw new ArgumentException();

        for (int y = 0; y < _height; y++)
            for (int x = 0; x < _width; x++)
                this[y, x] = new CellData(this[y, x]._type, positions[y, x]);
    }

    private void RemoveAllPlayers()
    {
        for (int index = 0; index < _width*_height; index++)
        {
            if (_grid[index]._type == CellType.Player)
                _grid[index] = new CellData(CellType.Empty, _grid[index]._position);
        }
    }
}

public enum CellType
{
    Empty, Item, Filled, Player,
}

[Serializable]
public struct CellData
{
    [SerializeField, HideInInspector] public CellType _type;
    [SerializeField, HideInInspector] public Vector2 _position;

    public CellData(CellType type, Vector2 position = new Vector2())
    {
        _type = type;
        _position = position;
    }
}
