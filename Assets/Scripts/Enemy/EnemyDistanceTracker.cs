using System;
using UnityEngine;

public class EnemyDistanceTracker : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _minDistance;

    private Vector2 _currentPosition;
    private Vector2 _playerPosition;

    public event Action<bool> MinimalDistanceReached;

    public Transform Player => _player;

    private void Update()
    {
        _currentPosition = new Vector2(transform.position.x, transform.position.z);
        _playerPosition = new Vector2(_player.position.x, _player.position.z);
        MinimalDistanceReached?.Invoke(Vector2.Distance(_currentPosition, _playerPosition) < _minDistance);
    }
}