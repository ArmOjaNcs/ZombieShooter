using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private EnemyDistanceTracker _distanceTracker;
    private NavMeshAgent _agent;
    private bool _isMinimalDistance;
    private bool _isEnable;

    private void Awake()
    {
        _distanceTracker = GetComponent<EnemyDistanceTracker>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = true;
        _agent.updatePosition = true;
        _agent.autoRepath = true;
        _agent.updateUpAxis = true;
    }

    private void OnEnable()
    {
        _distanceTracker.MinimalDistanceReached += OnDistanceIsMinimal;
    }

    private void OnDisable()
    {
        _distanceTracker.MinimalDistanceReached -= OnDistanceIsMinimal;
    }

    private void Update()
    {

        if (_isEnable == false || _isMinimalDistance)
        {
            Stop();

            if (_isEnable && _isMinimalDistance)
                LookAtPlayer();

            return;
        }
        else
        {
            Resume();
        }

        _agent.destination = _player.position;
        _agent.Move(transform.forward * Time.deltaTime);
    }

    public void Enable() => _isEnable = true;
    public void Disable() => _isEnable = false;

    private void Stop()
    {
        _agent.velocity = Vector3.zero;
        _agent.isStopped = true;
    }

    private void LookAtPlayer()
    {
        Vector3 lookPosition = _player.position;
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);
    }

    private void Resume() => _agent.isStopped = false;
    private void OnDistanceIsMinimal(bool isMinimalDistance) => _isMinimalDistance = isMinimalDistance;
}