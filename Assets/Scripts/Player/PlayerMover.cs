using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _speed;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce;

    private Vector3 _moveDirection;
    private Vector3 _totalDirection;
    private Vector3 _cameraRelativeMovement;
    private Vector3 _verticalVelocity;
    private float _horizontalRotation;
    private bool _isFreeMoving;
    private bool _isNeedRotateToCameraForward;

    public event Action<bool> Aimed;

    private void Awake()
    {
        _isFreeMoving = true;
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    private void Update()
    {
        Move();
        AddGravity();
        Jump();
    }

    private void Subscribe()
    {
        _player.Input.Move += OnMove;
        _player.Input.HorizontalLook += OnHorizontalLook;
        _player.Aimed += OnAimed;
    }

    private void UnSubscribe()
    {
        _player.Input.Move -= OnMove;
        _player.Input.HorizontalLook -= OnHorizontalLook;
        _player.Aimed -= OnAimed;
    }

    private void OnMove(Vector2 moveVector)
    {
        _moveDirection = moveVector;
    }

    private void OnHorizontalLook(float lookDirection)
    {
        _horizontalRotation = lookDirection;
    }

    private void OnAimed(bool isAimed)
    {
        if (isAimed)
        {
            RotateToCameraForward();
            Aimed?.Invoke(true);
        }
        else
            Aimed?.Invoke(false);

        _isFreeMoving = !isAimed;
    }

    private void Move()
    {
        if (_isFreeMoving)
            FreeMoving();
        else
            AimMoving();
    }

    private void AimMoving()
    {
        _isNeedRotateToCameraForward = false;
        Vector3 moveDirectionForward = (transform.forward * _moveDirection.y);
        Vector3 moveDirectionStrafe = (transform.right * _moveDirection.x);
        _totalDirection = (moveDirectionForward + moveDirectionStrafe) * _speed;
        _player.Controller.Move((_totalDirection + _verticalVelocity) * Time.deltaTime);
        Rotate();
    }

    private void FreeMoving()
    {
        _isNeedRotateToCameraForward = true;
        HandleRotation();
        _cameraRelativeMovement = ConvertToCameraSpace(_moveDirection);
        _player.Controller.Move((_cameraRelativeMovement * _speed + _verticalVelocity) * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.Rotate(_horizontalRotation * _rotateSpeed * Vector3.up * Time.deltaTime);
    }

    private Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        Vector3 cameraForward = _camera.forward;
        Vector3 cameraRight = _camera.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZProduct = vectorToRotate.y * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        return vectorRotatedToCameraSpace;
    }

    private void HandleRotation()
    {
        Vector3 positionTolookAt;
        positionTolookAt.x = _cameraRelativeMovement.x;
        positionTolookAt.y = 0;
        positionTolookAt.z = _cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (_cameraRelativeMovement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }

    private void RotateToCameraForward()
    {
        if (_isNeedRotateToCameraForward)
        {
            Vector3 cameraForward = _camera.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;
            transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }

    private void AddGravity()
    {
        if (_verticalVelocity.y > GameUtils.MinValueForVerticalVelocityOnGround)
            _verticalVelocity += Physics.gravity * Time.deltaTime;
    }

    private void Jump()
    {
        if (_player.Controller.isGrounded && _player.IsJump)
            _verticalVelocity = Vector3.up * _jumpForce;
    }
}