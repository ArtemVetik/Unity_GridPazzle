using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelDataBase _levels;
    [SerializeField] private GameGrid _gameGrid;
    [SerializeField] private SpriteRenderer _gridSprite;
    [Header("Prefabs")]
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private Checkpoint _itemTemplate;
    [SerializeField] private SpriteRenderer _wallTemplate;

    private void OnEnable()
    {
        LoadLevel(_levels[0]);
    }

    private void LoadLevel(LevelData level)
    {
        _gameGrid.Init(level);
        _gridSprite.ConfigureSprite(_gridSprite.sprite, Vector2.one * _gameGrid.WorldCellSize, GetWorldCenter(), _gameGrid.Size);

        Player instPlayer = Instantiate(_playerTemplate, _gameGrid[_gameGrid.PlayerPosition].Position, Quaternion.identity, _gameGrid.transform);
        _gameGrid.SetPlayer(instPlayer);

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

    private Vector2 GetWorldCenter()
    {
        float x = _gameGrid[0, 0].Position.x + _gameGrid[0, _gameGrid.Size.x - 1].Position.x;
        float y = _gameGrid[0, 0].Position.y + _gameGrid[_gameGrid.Size.y - 1, 0].Position.y;
        return new Vector2(x / 2, y / 2);
    }

    private void DeleteAllInChildren<T>() where T : MonoBehaviour
    {
        T[] objects = _gameGrid.GetComponentsInChildren<T>();
        foreach (var obj in objects)
        {
            Destroy(obj.gameObject);
        }
    }

    public void ReloadLevel()
    {
        DeleteAllInChildren<Checkpoint>();
        DeleteAllInChildren<Player>();

        LoadLevel(_levels[0]);
    }
}
