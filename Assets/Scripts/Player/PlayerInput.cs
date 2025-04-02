using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _inputSensitivity;

    private InputSystem_Actions _input;
    private Vector2 _lookVector;
    private float _incrementLookXDirection;
    private float _lookXdirection;

    public event Action<bool> Shoot;
    public event Action <bool>TakeAim;
    public event Action<bool> Jump;
    public event Action<Vector2> Move;
    public event Action<float> HorizontalLook;
    public event Action<float> VerticalLook;
    public event Action Reload;
    
    public Vector2 MoveVector { get; private set; }
    public float LookXdirection => _lookXdirection;

    private void Awake()
    {
        _input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _input.Enable();
        Subscribe();
    }

    private void OnDisable()
    {
        _input.Disable();
        UnSubscribe();
    }

    private void Update()
    {
        _lookVector = _input.Player.Look.ReadValue<Vector2>();
        _lookXdirection = InterpolateInputValue(_lookVector.x, ref _incrementLookXDirection);
        HorizontalLook?.Invoke(_lookVector.x);
        VerticalLook?.Invoke(_lookVector.y);
    }

    private void Subscribe()
    {
        _input.Player.Move.performed += OnMove;
        _input.Player.Move.canceled += OnMove;
        _input.Player.Attack.performed += OnShoot;
        _input.Player.Attack.canceled += OnStopShoot;
        _input.Player.Jump.performed += OnJump;
        _input.Player.Jump.canceled += OnStopJump;
        _input.Player.TakeAim.performed += OnTakeAim;
        _input.Player.TakeAim.canceled += OnStopTakeAim;
        _input.Player.Reload.performed += OnReload;
    }

    private void UnSubscribe()
    {
        _input.Player.Move.performed -= OnMove;
        _input.Player.Move.canceled -= OnMove;
        _input.Player.Attack.performed -= OnShoot;
        _input.Player.Attack.canceled -= OnStopShoot;
        _input.Player.Jump.performed -= OnJump;
        _input.Player.Jump.canceled -= OnStopJump;
        _input.Player.TakeAim.performed -= OnTakeAim;
        _input.Player.TakeAim.performed -= OnStopTakeAim;
    }

    private void OnTakeAim(InputAction.CallbackContext context)
    {
        TakeAim?.Invoke(true);
    }

    private void OnStopTakeAim(InputAction.CallbackContext context)
    {
        TakeAim?.Invoke(false);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveVector = _input.Player.Move.ReadValue<Vector2>();
        Move?.Invoke(MoveVector);
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Shoot?.Invoke(true);
    }

    private void OnStopShoot(InputAction.CallbackContext context)
    {
        Shoot?.Invoke(false);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Jump?.Invoke(true);
    }

    private void OnStopJump(InputAction.CallbackContext context)
    {
        Jump?.Invoke(false);
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        Reload?.Invoke();
    }

    private float InterpolateInputValue(float value, ref float incrementedValue)
    {
        value = Mathf.Clamp(value, GameUtils.MinThresholdForClampMouseInput, GameUtils.MaxThresholdForClampMouseInput);

        if (Mathf.Approximately(value, 0))
        {
            value = 0;
            incrementedValue = 0;
            return value;
        }

        return value = Mathf.Lerp(0, value, IncrementOverTime(ref incrementedValue));
    }

    private float IncrementOverTime(ref float incrementedValue)
    {
        return incrementedValue += (GameUtils.SeedForIncrement * _inputSensitivity) * Time.deltaTime;
    }
}