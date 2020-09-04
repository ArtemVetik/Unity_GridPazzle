using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private Paddings _paddings;

    private Player _player;
    private CellData[,] _grid;
    private int _checkpointCount;

    public CellData this[Vector2Int position] => _grid[position.y, position.x];
    public CellData this[int y, int x] => _grid[y, x];
    public Paddings Paddings => _paddings;
    public Vector2Int Size { get; private set; }
    public Vector2Int PlayerPosition { get; private set; }
    public float WorldCellSize { get; private set; }

    public void Init(LevelData level)
    {
        _grid = level.CreateGrid(Screen.width, Screen.height, _paddings);

        Size = level.Size;
        PlayerPosition = level.GetPlayerPosition();
        WorldCellSize = Vector2.Distance(_grid[0, 0].Position, _grid[0, 1].Position);
        _checkpointCount = level.GetItemCount();
    }

    public void SetPlayer(Player player)
    {
        ClearActions();
        _player = player;
        _player.SetTargetPosition(this[PlayerPosition].Position);
        _player.Moved += Move;
        _player.CollectedCheckpoint += OnCollectCheckpoint;
    }

    public void Move(Vector2Int shift)
    {
        Vector2Int tempPlayerPosition = PlayerPosition;
        while (Has(tempPlayerPosition + shift))
        {
            tempPlayerPosition += shift;
            CellType type = this[tempPlayerPosition].Type;
            if (type == CellType.Item)
                break;
            else if (type == CellType.Filled)
            {
                tempPlayerPosition -= shift;
                break;
            }
        }

        _grid[tempPlayerPosition.y, tempPlayerPosition.x] = new CellData(CellType.Player, _grid[tempPlayerPosition.y, tempPlayerPosition.x].Position);
        PlayerPosition = tempPlayerPosition;

        _player.SetTargetPosition(this[PlayerPosition].Position);
    }

    private bool Has(Vector2Int position)
    {
        return position.x >= 0 && position.x < Size.x &&
                position.y >= 0 && position.y < Size.y;
    }

    public void OnCollectCheckpoint()
    {
        _checkpointCount--;

        if (_checkpointCount == 0)
            Debug.Log("WIN!");
    }

    private void ClearActions()
    {
        if (_player == null)
            return;

        _player.Moved -= Move;
        _player.CollectedCheckpoint -= OnCollectCheckpoint;
    }

    private void OnDisable()
    {
        ClearActions();
    }
}