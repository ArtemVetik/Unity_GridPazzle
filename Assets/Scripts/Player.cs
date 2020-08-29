using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridObject
{ 
    private Vector2Int _position;

    public void Init(Vector2Int position)
    {
        _position = position;
    }
}
