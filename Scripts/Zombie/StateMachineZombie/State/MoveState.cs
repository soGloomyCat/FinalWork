using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : StateZombie
{
    [SerializeField] private float _speed;

    private Vector2 _startVector;
    private Vector2 _currentVector;
    private List<Vector2> _directions;

    private void Start()
    {
        _startVector = Vector2.down;
        _currentVector = _startVector;
        _directions = new List<Vector2> { Vector2.left, Vector2.right, Vector2.up};
    }

    private void Update()
    {
        transform.Translate(_currentVector * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Zombie>(out Zombie zombie))
            ChangeMove(_directions[Random.Range(0, _directions.Count)]);
        else if (collision.TryGetComponent<InvisableWall>(out InvisableWall invisableWall))
            _currentVector = -_currentVector;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Zombie>(out Zombie zombie))
            ChangeMove(_startVector);
    }

    private void ChangeMove(Vector2 direction)
    {
        _currentVector = direction;
    }
}
