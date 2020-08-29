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
    [SerializeField, HideInInspector] private CellData[] _grid;

    public Vector2Int Size => new Vector2Int(_width, _height);
    public Vector2Int PlayerPosition { get; private set; }
    public CellData this[int y, int x]
    {
        get
        {
            return _grid[y * _width + x];
        }
        set
        {
            if (value.Type == CellType.Player && this[PlayerPosition.y, PlayerPosition.x].Type == CellType.Player)
            {
                this[PlayerPosition.y, PlayerPosition.x] = new CellData(CellType.Empty);
                PlayerPosition = new Vector2Int(x, y);
            }
            _grid[y * _width + x] = value;
        }
    }

    public LevelData(int width = 0, int height = 0)
    {
        _width = width;
        _height = height;
        _grid = new CellData[_height * _width];
    }

    public void Resize(int newWidth, int newHeight)
    {
        if (_width == newWidth && _height == newHeight)
            return;

        _width = newWidth;
        _height = newHeight;
        Array.Resize(ref _grid, _width * _height);
    }

    public void InitPositions(int screenWidth, int screenHeight, Paddings paddings)
    {
        Vector2[,] positions = Size.GetGrid(screenWidth, screenHeight, paddings);

        for (int y = 0; y < Size.y; y++)
        {
            for (int x = 0; x < Size.x; x++)
            {
                this[y, x] = new CellData(this[y, x].Type, positions[y, x]);
            }
        }
    }

    public Vector2 GetWorldCenter()
    {
        float x = (this[0, 0].Position.x + this[0, _width - 1].Position.x) / 2;
        float y = (this[0, 0].Position.y + this[_height - 1, 0].Position.y) / 2;
        return Camera.main.ScreenToWorldPoint(new Vector2(x, y), Camera.MonoOrStereoscopicEye.Right);
    }
}

[Serializable]
public struct CellData
{
    [SerializeField, HideInInspector] private CellType _type;
    [SerializeField, HideInInspector] private Vector2 _position;

    public CellType Type => _type;
    public Vector2 Position => _position;

    public CellData(CellType type, Vector2 position = default)
    {
        _type = type;
        _position = position;
    }
}