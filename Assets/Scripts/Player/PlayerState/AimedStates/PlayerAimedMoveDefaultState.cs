using UnityEngine;

public class PlayerAimedMoveDefaultState : PlayerState<AimedMoveStateMachine>
{
    public PlayerAimedMoveDefaultState(AimedMoveStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Update()
    {
        base.Update();

        if (StateMachine.IsTakeAim == false || StateMachine.Player.IsCanTakeAim == false)
        {
            StateMachine.SwitchState(StateMachine.DefaultState);
            return;
        }

        if (StateMachine.Input.MoveVector != Vector2.zero && StateMachine.IsReloading == false)
            StateMachine.SwitchState(StateMachine.AimedMoveState);
        else if(StateMachine.IsReloading == false)
            StateMachine.SwitchState(StateMachine.RotateState);
    }
}