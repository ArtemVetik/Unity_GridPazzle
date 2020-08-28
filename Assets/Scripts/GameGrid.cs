using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    private LevelData _level;

    public Vector2Int Size => _level.Size;
    public Vector2Int PlayerPosition => _level.GetPlayerPosition();
    public float DistanceBetweenPoints => Vector2.Distance(_level[0, 0]._position, _level[0, 1]._position);
    public CellData this[int y, int x] => _level[y, x];
    public CellData this[Vector2Int position] => this[position.y, position.x];

    public GameGrid(LevelData level, Paddings paddings)
    {
        _level = level;
        Vector2[,] positions = _level.Size.GetGrid(Screen.width, Screen.height, paddings);
        _level.InitPositions(positions);
    }
}