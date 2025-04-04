using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemySimpleAnimator : MonoBehaviour
{
    [SerializeField] private EnemySimpleMover _simpleMover;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _simpleMover.IsRunning += OnRunning;
    }

    private void OnDisable()
    {
        _simpleMover.IsRunning -= OnRunning;
    }

    private void OnRunning(bool isRunning)
    {
        if (isRunning)
        {
            _animator.SetBool(GameUtils.Running, true);
            _animator.SetBool(GameUtils.Idle, false);
        }
        else
        {
            _animator.SetBool(GameUtils.Running, false);
            _animator.SetBool(GameUtils.Idle, true);
        }
    }
}