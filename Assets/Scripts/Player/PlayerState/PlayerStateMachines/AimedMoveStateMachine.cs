public class AimedMoveStateMachine : PlayerStateMachine
{
    public PlayerAimedMoveState AimedMoveState { get; private set; }
    public PlayerRotateState RotateState { get; private set; }
    public PlayerReloadState ReloadState { get; private set; }
    public PlayerAimedMoveDefaultState DefaultState { get; private set; }
    public bool IsReloading { get; private set; }

    private protected override void Awake()
    {
        base.Awake();
        AimedMoveState = new PlayerAimedMoveState(this);
        RotateState = new PlayerRotateState(this);
        ReloadState = new PlayerReloadState(this);
        DefaultState = new PlayerAimedMoveDefaultState(this);
    }

    private protected override void OnEnable()
    {
        base.OnEnable();
        Input.Reload += OnReload;
    }

    private protected override void OnDisable()
    {
        base.OnDisable();
        Input.Reload -= OnReload;
    }

    private void Start()
    {
        SwitchState(DefaultState);
    }

    private void OnReload()
    {
        if (IsReloading == false)
            SwitchState(ReloadState);
    }

    public void CompleteReload()
    {
        IsReloading = false;
    }

    public void StartReload()
    {
        IsReloading = true;
    }
}