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

        Player instPlayer = InstatiateInWorld(_playerTemplate, _gameGrid[_gameGrid.PlayerPosition].Position, _gameGrid.transform);
        instPlayer.Init(_gameGrid.PlayerPosition);

        for (int y = 0; y < _gameGrid.Size.y; y++)
        {
            for (int x = 0; x < _gameGrid.Size.x; x++)
            {
                if (_gameGrid[y, x].Type == CellType.Item)
                    InstatiateInWorld(_itemTemplate, _gameGrid[y, x].Position, _gameGrid.transform);
                else if (_gameGrid[y, x].Type == CellType.Filled)
                    InstatiateInWorld(_wallTemplate, _gameGrid[y, x].Position, _gameGrid.transform);
            }
        }
    }

    private T InstatiateInWorld<T>(T template, Vector2 screenPosition, Transform parent = null) where T : Object
    {
        Vector3 instPosition = Camera.main.ScreenToWorldPoint(screenPosition, Camera.MonoOrStereoscopicEye.Right);
        return Instantiate(template, instPosition, Quaternion.identity, parent);
    }
}
