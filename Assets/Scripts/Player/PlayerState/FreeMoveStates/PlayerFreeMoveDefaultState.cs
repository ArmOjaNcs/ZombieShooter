using UnityEngine;

public class PlayerFreeMoveDefaultState : PlayerState<FreeMoveStateMachine>
{
    public PlayerFreeMoveDefaultState(FreeMoveStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Update()
    {
        base.Update();

        if (StateMachine.IsAimed == true)
        {
            StateMachine.SwitchState(StateMachine.DefaultState);
            return;
        }

        if (StateMachine.Input.MoveVector != Vector2.zero)
            StateMachine.SwitchState(StateMachine.RunState);
        else
            StateMachine.SwitchState(StateMachine.IdleState);
    }
}