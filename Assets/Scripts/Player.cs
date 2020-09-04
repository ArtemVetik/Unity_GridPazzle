using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _targetPosition;

    public bool CanMove => (Vector2)(transform.position) == _targetPosition;

    public UnityAction<Vector2Int> Moved; 
    public UnityAction CollectedCheckpoint;

    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        Move();

        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    private void Move()
    {
        if (CanMove == false)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Moved?.Invoke(Vector2Int.left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Moved?.Invoke(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Moved?.Invoke(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Moved?.Invoke(Vector2Int.down);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Checkpoint checkpoint))
        {
            checkpoint.Die();
            CollectedCheckpoint?.Invoke();
        }
    }
}