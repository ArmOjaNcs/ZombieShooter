public class PlayerIdleState : PlayerFreeMoveState
{
    public PlayerIdleState(FreeMoveStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StateMachine.Animator.SetBool(GameUtils.Idle, true);
    }

    public override void Exit()
    {
        StateMachine.Animator.SetBool(GameUtils.Idle, false);
    }
}