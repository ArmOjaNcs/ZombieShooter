using UnityEngine;

public abstract class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    private protected State CurrentState;

    public PlayerInput Input => _input;
    public Animator Animator { get; protected set; }
    public AnimationProperties AnimationProperties { get; protected set; }
    public Player Player { get; protected set; }
    public bool IsTakeAim { get; protected set; }

    private protected virtual void OnEnable()
    {
        Input.TakeAim += OnTakeAim;
    }

    private protected virtual void OnDisable()
    {
        Input.TakeAim -= OnTakeAim;
    }

    private protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        AnimationProperties = GetComponent<AnimationProperties>();
        Player = GetComponent<Player>();
    }

    private protected virtual void Update()
    {
        CurrentState?.Update();
    }

    public void SwitchState(State state)
    {
        if(CurrentState == null)
        {
            CurrentState = state;
            CurrentState.Enter();
            return;
        }

        if(CurrentState.Equals(state) == false)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
        } 
    }

    private void OnTakeAim(bool isTakeAim)
    {
        IsTakeAim = isTakeAim;
    }
}