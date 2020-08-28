using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGizmosViewer : MonoBehaviour
{
    [SerializeField] private LevelDataBase _levels;
    [SerializeField] private int _currentLevel;
    [SerializeField] private GridSettings _settings;
    [SerializeField] private Vector2Int _targetScreenSize;
    [SerializeField] private float _sphereRadius;

    private void OnValidate()
    {
        _targetScreenSize.x = Mathf.Clamp(_targetScreenSize.x, 0, int.MaxValue);
        _targetScreenSize.y = Mathf.Clamp(_targetScreenSize.y, 0, int.MaxValue);

        if (_levels.EmptyOrNull == false)
            _currentLevel = Mathf.Clamp(_currentLevel, 0, _levels.Count - 1);
    }

    private void OnDrawGizmos()
    {
        if (_levels.EmptyOrNull)
            return;

        Vector2[,] grid = _levels[_currentLevel].Size.GetGrid(_targetScreenSize.x, _targetScreenSize.y, _settings.Paddings);

        for (int y = 0; y < _levels[_currentLevel].Size.y; y++)
        {
            for (int x = 0; x < _levels[_currentLevel].Size.x; x++)
            {
                Gizmos.color = GetColorByCellType(_levels[_currentLevel][y, x]._type);
                Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(grid[y, x]), _sphereRadius);
            }
        }
    }

    private Color GetColorByCellType(CellType type)
    {
        switch (type)
        {
            case CellType.Empty:
                return Color.gray;
            case CellType.Item:
                return Color.blue;
            case CellType.Filled:
                return Color.white;
            case CellType.Player:
                return Color.green;
            default:
                return Color.black;
        }
    }
}
