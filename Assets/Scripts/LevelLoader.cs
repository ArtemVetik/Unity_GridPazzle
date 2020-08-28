using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelDataBase _levels;
    [SerializeField] private GridSettings _settings;
    [SerializeField] private SpriteRenderer _gridSprite;
    [Header("Prefabs")]
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private Checkpoint _itemTemplate;
    [SerializeField] private SpriteRenderer _filledTemplate;

    private GameGrid _gameGrid;
    private float _cellSize;

    private void Start()
    {
        _gameGrid = new GameGrid(_levels[0], _settings.Paddings);
        _cellSize = _gameGrid.DistanceBetweenPoints / 128;


        _gridSprite.ConfigureSprite(_settings.CellSprite, Vector2.one * _cellSize, Vector3.zero, _gameGrid.Size);

        Player instPlayer = InstatiateInWorld(_playerTemplate, _gameGrid[_gameGrid.PlayerPosition]._position);
        instPlayer.Init(_gameGrid.PlayerPosition);

        instPlayer.Sprite.ConfigureSprite(_settings.PlayerSprite, Vector2.one * _cellSize);
        _itemTemplate.Sprite.ConfigureSprite(_settings.ItemSprite, Vector2.one * _cellSize);

        for (int y = 0; y < _gameGrid.Size.y; y++)
        {
            for (int x = 0; x < _gameGrid.Size.x; x++)
            {
                if (_gameGrid[y, x]._type == CellType.Item)
                    InstatiateInWorld(_itemTemplate, _gameGrid[y,x]._position);
                else if (_gameGrid[y, x]._type == CellType.Filled)
                    InstatiateInWorld(_filledTemplate, _gameGrid[y, x]._position);
            }
        }
    }

    private T InstatiateInWorld<T>(T template, Vector2 screenPosition) where T : Object
    {
        Vector3 instPosition = Camera.main.ScreenToWorldPoint(screenPosition, Camera.MonoOrStereoscopicEye.Right);
        return Instantiate(template, instPosition, Quaternion.identity);
    }
}
