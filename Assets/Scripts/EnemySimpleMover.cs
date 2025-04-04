using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
[RequireComponent(typeof(EnemyDistanceTracker))]
public class EnemySimpleMover : MonoBehaviour
{
    private const float StepSmooth = 0.1f;
    private const float MinStepRayLength = 0.2f;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _stepRayUpper;
    [SerializeField] private Transform _stepRayLower;
    [SerializeField] private float _stepUpperHeight = 0.3f;
    [SerializeField] private float _stepLowerHeight = 0.05f;

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private EnemyDistanceTracker _distanceTracker;
    private Vector3 _moveDirection;
    private LayerMask _layerWithOutEnemy;
    private float _stepRayLength;
    private bool _isMinimalDistance;

    public event Action<bool> IsRunning;

    private void Awake()
    {
        LayerMask enemyLayer = 1 << GameUtils.EnemyLayer;
        _layerWithOutEnemy = ~ enemyLayer;
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _distanceTracker = GetComponent<EnemyDistanceTracker>();
        _stepRayUpper.localPosition = new Vector3(0, _stepUpperHeight * _collider.height, 0);
        _stepRayLower.localPosition = new Vector3(0, _stepLowerHeight * _collider.height, 0);
        _stepRayLength = _collider.radius + MinStepRayLength;
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
        LookAtPlayer();
    }

    private void FixedUpdate()
    {
        _moveDirection = transform.TransformDirection(Vector3.forward);
        _moveDirection *= _speed;
        _moveDirection = new Vector3(_moveDirection.x, _rigidbody.linearVelocity.y, _moveDirection.z);

        if (_isMinimalDistance == false)
        {
            HandleClimbing();
            _rigidbody.linearVelocity = _moveDirection;
            IsRunning?.Invoke(true);
        }
        else
        {
            IsRunning?.Invoke(false);
        }
    }

    private void HandleClimbing()
    {
        RaycastHit hitLower;

        if(Physics.Raycast(_stepRayLower.position, transform.TransformDirection(Vector3.forward), out hitLower, _stepRayLength, _layerWithOutEnemy))
        {
            if(Vector3.Angle(Vector3.up, hitLower.normal) == GameUtils.StraightAngle)
            {
                RaycastHit hitUpper;

                if (Physics.Raycast(_stepRayUpper.position, transform.TransformDirection(Vector3.forward), out hitUpper, 
                    _stepRayLength, _layerWithOutEnemy) == false)
                    _rigidbody.MovePosition(transform.position + new Vector3(0, StepSmooth, 0));
            }
        }
    }

    private void LookAtPlayer()
    {
        Vector3 lookPosition = _distanceTracker.Player.position;
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);
    }

    private void OnDistanceIsMinimal(bool isMinimalDistance) => _isMinimalDistance = isMinimalDistance;
}