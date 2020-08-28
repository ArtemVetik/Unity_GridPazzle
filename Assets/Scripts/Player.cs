using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    
    private Vector2Int _position;

    public SpriteRenderer Sprite => _sprite;

    public void Init(Vector2Int position)
    {
        _position = position;
    }
}
