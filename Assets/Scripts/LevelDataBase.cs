using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable/LevelData")]
public class LevelDataBase : ScriptableObject
{
    [SerializeField, HideInInspector] private List<LevelData> _levels = new List<LevelData>();

    public LevelData this[int index] => _levels[index];
    public bool EmptyOrNull => _levels == null || _levels.Count == 0;
    public int Count => _levels.Count;

    public void Add()
    {
        if (_levels == null)
            _levels = new List<LevelData>();

        _levels.Add(new LevelData());
    }

    public void Remove(LevelData levelData)
    {
        _levels.Remove(levelData);
    }

    public int IndexOf(LevelData levelData)
    {
        return _levels.IndexOf(levelData);
    }

    public void MoveTop(LevelData levelData)
    {
        int index = _levels.IndexOf(levelData);
        if (index <= 0)
            return;

        _levels[index] = _levels[index - 1];
        _levels[index - 1] = levelData;
    }

    public void MoveDown(LevelData levelData)
    {
        int index = _levels.IndexOf(levelData);
        if (index >= _levels.Count - 1)
            return;

        _levels[index] = _levels[index + 1];
        _levels[index + 1] = levelData;
    }
}