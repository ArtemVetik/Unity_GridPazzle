using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GridSettings", menuName = "Scriptable/GridSettings")]
public class GridSettings : ScriptableObject
{
    [SerializeField] private Sprite _cellSprite, _itemSprite, _filledSprite, _playerSprite;
    [SerializeField] private Paddings _paddings;

    public Paddings Paddings => _paddings;
    public Sprite CellSprite => _cellSprite;
    public Sprite ItemSprite => _itemSprite;
    public Sprite FilledSprite => _filledSprite;
    public Sprite PlayerSprite => _playerSprite;
}

[Serializable]
public class Paddings
{
    [SerializeField, Range(0f, 0.5f)] private float _leftPadding;
    [SerializeField, Range(0f, 0.5f)] private float _rightPadding;
    [SerializeField, Range(0f, 0.5f)] private float _topPadding;
    [SerializeField, Range(0f, 0.5f)] private float _downPadding;

    public float LeftPadding => _leftPadding;
    public float RightPadding => _rightPadding;
    public float TopPadding => _topPadding;
    public float DownPadding => _downPadding;
}