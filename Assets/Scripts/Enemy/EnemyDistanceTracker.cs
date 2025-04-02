using System;
using UnityEngine;

public class EnemyDistanceTracker : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _minDistance;

    public event Action<bool> DistanceIsMinimal;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < _minDistance)
            DistanceIsMinimal?.Invoke(true);
        else
            DistanceIsMinimal?.Invoke(false);
    }
}