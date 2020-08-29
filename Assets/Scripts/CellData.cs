using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CellData
{
    private CellType _type;
    private Vector2 _position;

    public CellType Type => _type;
    public Vector2 Position => _position;

    public CellData(CellType type, Vector2 position = default)
    {
        _type = type;
        _position = position;
    }
}
