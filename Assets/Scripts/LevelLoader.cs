using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelDataBase _levels;
    [SerializeField] private GameGrid _gameGrid;
    [Header("Prefabs")]
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private Checkpoint _itemTemplate;
    [SerializeField] private SpriteRenderer _wallTemplate;

    private void OnEnable()
    {
        _gameGrid.Init(_levels[0]);

        Instantiate(_playerTemplate, _gameGrid[_gameGrid.PlayerPosition].Position, Quaternion.identity, _gameGrid.transform);

        for (int y = 0; y < _gameGrid.Size.y; y++)
        {
            for (int x = 0; x < _gameGrid.Size.x; x++)
            {
                if (_gameGrid[y, x].Type == CellType.Item)
                    Instantiate(_itemTemplate, _gameGrid[y, x].Position, Quaternion.identity, _gameGrid.transform);
                else if (_gameGrid[y, x].Type == CellType.Filled)
                    Instantiate(_wallTemplate, _gameGrid[y, x].Position, Quaternion.identity, _gameGrid.transform);
            }
        }
    }
}
