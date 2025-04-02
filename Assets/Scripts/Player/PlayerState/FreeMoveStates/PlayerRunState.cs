public class PlayerRunState : PlayerFreeMoveState
{
    public PlayerRunState(FreeMoveStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StateMachine.Animator.SetBool(GameUtils.Running, true);
    }

    public override void Exit()
    {
        StateMachine.Animator.SetBool(GameUtils.Running, false);
    }
}