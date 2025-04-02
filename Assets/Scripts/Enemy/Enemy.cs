using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private RagdollHandler _ragdollHandler;

    private float _standUpTimer;
    private float _standUpTime = 2;
    private bool _isDowned;

    public Health Health { get; private set; }

    private void Awake()
    {
        _animator.Initialize();
        Health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _animator.StandingUpAnimationEnded += OnStandingUpAnimationEnded;
    }

    private void OnDisable()
    {
        _animator.StandingUpAnimationEnded -= OnStandingUpAnimationEnded;
    }

    private void Start()
    {
        StartRun();
    }

    private void Update()
    {
        if (_isDowned == false)
            return;

        _standUpTimer += Time.deltaTime;

        if (_standUpTimer > _standUpTime)
        {
            StandUp();
            _isDowned = false;
        }
    }

    public void StartRun()
    {
        _animator.EnableAnimator();
        _animator.StartRun();
        _mover.Enable();
    }

    public void Kill(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdollBehavior();
        _ragdollHandler.Hit(force, hitPoint);
        _standUpTimer = 0;
        _isDowned = true;
    }

    private void OnStandingUpAnimationEnded()
    {
        _mover.Enable();
        Health.Refresh();
    }

    private void StandUp()
    {
        _ragdollHandler.Disable();
        _animator.PlayStandingUp(_animator.EnableAnimator, _animator.StartRun);
    }

    private void EnableRagdollBehavior()
    {
        _animator.DisableAnimator();
        _mover.Disable();
        _ragdollHandler.Enable();
    }
}