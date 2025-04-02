using UnityEngine;

public class PositionAdjuster : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _delta;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _point.position, _delta);
    }
}