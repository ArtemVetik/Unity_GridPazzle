using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _gridSprite;
    [SerializeField] private Paddings _paddings;

    private LevelData _level;

    public Paddings Paddings => _paddings;
    public Vector2Int Size => _level.Size;
    public Vector2Int PlayerPosition => _level.PlayerPosition;
    public CellData this[Vector2Int position] => _level[position.y, position.x];
    public CellData this[int y, int x] => _level[y, x];
    public float DistanceBetweenPoints { get; private set; }
    public float WorldCellSize { get; private set; }

    public void Init(LevelData level)
    {
        _level = level;
        _level.InitPositions(Screen.width, Screen.height, _paddings);

        DistanceBetweenPoints = Vector2.Distance(_level[0, 0].Position, _level[0, 1].Position);
        WorldCellSize = DistanceBetweenPoints / 128;

        _gridSprite.ConfigureSprite(_gridSprite.sprite, Vector2.one * WorldCellSize, _level.GetWorldCenter(), Size);
    }
}