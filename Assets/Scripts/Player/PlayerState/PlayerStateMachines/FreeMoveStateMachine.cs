public class FreeMoveStateMachine : PlayerStateMachine
{
    public PlayerIdleState IdleState {  get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerFreeMoveDefaultState DefaultState { get; private set; }
    public bool IsAimed {  get; private set; }

    private protected override void OnEnable()
    {
        base.OnEnable();
        Player.Aimed += OnAimed;
    }

    private protected override void OnDisable()
    {
        base.OnDisable();
        Player.Aimed -= OnAimed;
    }

    private protected override void Awake()
    {
        base.Awake();
        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        DefaultState = new PlayerFreeMoveDefaultState(this);
    }

    private void Start()
    {
        SwitchState(DefaultState);
    }

    private void OnAimed(bool isAimed)
    {
        IsAimed = isAimed;
    }
}