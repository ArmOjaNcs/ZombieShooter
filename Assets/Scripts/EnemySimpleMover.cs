using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemySimpleMover : MonoBehaviour
{
    private const float StepSmooth = 0.1f;
    private const float MinStepRayLength = 0.2f;

    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _stepRayUpper;
    [SerializeField] private Transform _stepRayLower;
    [SerializeField] private float _stepUpperHeight = 0.3f;
    [SerializeField] private float _stepLowerHeight = 0.05f;

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private EnemyDistanceTracker _distanceTracker;
    private Animator _animator;
    private Vector3 _moveDirection;
    private LayerMask _layerWithOutEnemy;
    private float _stepRayLength;
    private bool _isMinimalDistance;

    private void Awake()
    {
        LayerMask enemyLayer = 1 << 10;
        _layerWithOutEnemy = ~ enemyLayer;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
        _distanceTracker = GetComponent<EnemyDistanceTracker>();
        _stepRayUpper.localPosition = new Vector3(0, _stepUpperHeight * _collider.height, 0);
        _stepRayLower.localPosition = new Vector3(0, _stepLowerHeight * _collider.height, 0);
        _stepRayLength = _collider.radius + MinStepRayLength;
    }

    private void OnEnable()
    {
        _distanceTracker.DistanceIsMinimal += OnDistanceIsMinimal;
    }

    private void OnDisable()
    {
        _distanceTracker.DistanceIsMinimal -= OnDistanceIsMinimal;
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void FixedUpdate()
    {
        _moveDirection = transform.TransformDirection(Vector3.forward);
        _moveDirection = new Vector3(_moveDirection.x, _rigidbody.linearVelocity.y, _moveDirection.z) * _speed;

        if (_isMinimalDistance == false)
        {
            HandleClimbing();
            _rigidbody.linearVelocity = _moveDirection;
            _animator.SetBool(GameUtils.Running, true);
            _animator.SetBool(GameUtils.Idle, false);
        }
        else
        {
            _animator.SetBool(GameUtils.Running, false);
            _animator.SetBool(GameUtils.Idle, true);
        }
    }

    private void HandleClimbing()
    {
        RaycastHit hitLower;

        if(Physics.Raycast(_stepRayLower.position, transform.TransformDirection(Vector3.forward), out hitLower, _stepRayLength, _layerWithOutEnemy))
        {
            if(Vector3.Angle(Vector3.up, hitLower.normal) <= 90)
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
        Vector3 lookPosition = _player.position;
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);
    }

    private void OnDistanceIsMinimal(bool isMinimalDistance) => _isMinimalDistance = isMinimalDistance;
}