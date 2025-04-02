using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _checkBox;

    private AimedMoveStateMachine _aimStateMachine;
    private BoxCollider _checkBoxCollider;
    private int _playerLayer = GameUtils.StickmanLayer;

    public event Action<bool> Aimed;

    public PlayerInput Input => _playerInput;
    public Animator Animator { get; private set; }
    public CharacterController Controller {  get; private set; }
    public bool IsCanTakeAim {  get; private set; }
    public bool IsTakeAim {  get; private set; }
    public bool IsJump {  get; private set; }
    public bool IsShoot { get; private set; }

    private void Awake()
    {
        _aimStateMachine = GetComponent<AimedMoveStateMachine>();
        _checkBoxCollider = _checkBox.GetComponent<BoxCollider>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        Input.TakeAim += OnTakeAim;
        Input.Shoot += OnShoot;
        Input.Jump += OnJump;
    }

    private void OnDisable()
    {
        Input.TakeAim -= OnTakeAim;
        Input.Shoot -= OnShoot;
        Input.Jump -= OnJump;
    }

    private void Update()
    {
       
        IsCanTakeAim = TryTakeAim();
        
        if (IsCanTakeAim && IsTakeAim && _aimStateMachine.IsReloading == false)
        {
            Aimed.Invoke(true);
            _weapon.TakeAim(true);
        }
        else
        {
            Aimed.Invoke(false);
            _weapon.TakeAim(false);
        }

        if (IsShoot && IsCanTakeAim && IsTakeAim)
            _weapon.Shoot();
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    private void OnShoot(bool isShoot)
    {
        IsShoot = isShoot;
    }

    private void OnJump(bool isJump)
    {
        IsJump = isJump;
    }

    private void OnTakeAim(bool isTakeAim)
    {
        IsTakeAim = isTakeAim;
    }

    private bool TryTakeAim()
    {
        Collider[] colliders = Physics.OverlapBox(_checkBox.position, _checkBoxCollider.bounds.extents, Quaternion.identity,
        _playerLayer, QueryTriggerInteraction.Ignore);
        
        if (colliders.Length > 0)
            return false;

        return true;
    }
}