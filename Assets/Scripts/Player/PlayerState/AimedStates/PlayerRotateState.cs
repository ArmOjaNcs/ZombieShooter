using UnityEngine;

public class PlayerRotateState : PlayerAimedState
{
    public PlayerRotateState(AimedMoveStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StateMachine.Animator.SetBool(GameUtils.IsRotate, true);
    }

    public override void Update()
    {
        base.Update();

        StateMachine.Animator.SetFloat(GameUtils.Rotation, StateMachine.Input.LookXdirection);

        if (StateMachine.Input.MoveVector != Vector2.zero)
            StateMachine.SwitchState(StateMachine.AimedMoveState);
    }

    public override void Exit()
    {
        StateMachine.Animator.SetBool(GameUtils.IsRotate, false);
        StateMachine.Animator.SetBool(GameUtils.IsAiming, false);
        StateMachine.Animator.SetFloat(GameUtils.Rotation, 0);
    }
}