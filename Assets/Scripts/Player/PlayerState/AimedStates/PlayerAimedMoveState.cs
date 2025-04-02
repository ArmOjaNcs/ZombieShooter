using UnityEngine;

public class PlayerAimedMoveState : PlayerAimedState
{
    public PlayerAimedMoveState(AimedMoveStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        base.Update();

        if (StateMachine.Input.MoveVector != Vector2.zero)
        {
            StateMachine.Animator.SetFloat(GameUtils.Forward, StateMachine.Input.MoveVector.y);
            StateMachine.Animator.SetFloat(GameUtils.Right, StateMachine.Input.MoveVector.x);
        }
        else
        {
            StateMachine.SwitchState(StateMachine.RotateState);
        }
    }

    public override void Exit()
    {
        StateMachine.Animator.SetBool(GameUtils.IsAiming, false);
        StateMachine.Animator.SetFloat(GameUtils.Forward, 0);
        StateMachine.Animator.SetFloat(GameUtils.Right, 0);
    }
}