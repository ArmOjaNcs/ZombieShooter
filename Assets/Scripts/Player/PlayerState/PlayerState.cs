public class PlayerState<T> : State where T : PlayerStateMachine
{
    private protected T StateMachine;

    public PlayerState (T stateMachine) 
    {
        StateMachine = stateMachine;
    }

    public override void Update()
    {
        if (StateMachine.Player.Controller.isGrounded && StateMachine.Player.IsJump)
        {
            StateMachine.Animator.SetBool(GameUtils.IsJump, true);
            StateMachine.Animator.SetTrigger(GameUtils.Jump);
        }
        else if (StateMachine.Player.Controller.isGrounded)
        {
            StateMachine.Animator.SetBool(GameUtils.IsJump, false);
        }
    }
}