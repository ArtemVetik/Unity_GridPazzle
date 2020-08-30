using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridObject
{
    [SerializeField] private float _speed;

    private Vector2 _targetPosition;

    public bool CanMove => (Vector2)(transform.position) == _targetPosition;

    private void Update()
    {
        Move();

        _targetPosition = _grid[_grid.PlayerPosition].Position;
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void Move()
    {
        if (CanMove == false)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _grid.Move(Vector2Int.left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            _grid.Move(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _grid.Move(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            _grid.Move(Vector2Int.down);
    }
}
