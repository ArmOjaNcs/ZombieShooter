using UnityEngine;

public class PlayerReloadState : PlayerState<AimedMoveStateMachine>
{
    private bool _animationPlayed = false;

    public PlayerReloadState(AimedMoveStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        StateMachine.StartReload();
        StateMachine.AnimationProperties.VerticalLookRig.weight = 0f;
        StateMachine.AnimationProperties.LeftHandConstraintRig.weight = 0f;
        StateMachine.Animator.SetTrigger(GameUtils.Reload);
    }

    public override void Update()
    {
        base.Update();

        AnimatorStateInfo animatorStateInfo = StateMachine.Animator.GetCurrentAnimatorStateInfo(GameUtils.PlayerAnimatorArmsLayer);

        if(_animationPlayed && animatorStateInfo.normalizedTime >= 1)
        {
            _animationPlayed = false;
            StateMachine.SwitchState(StateMachine.DefaultState);
        }

        if (animatorStateInfo.normalizedTime < 1)
            _animationPlayed = true;
    }

    public override void Exit()
    {
        StateMachine.AnimationProperties.LeftHandConstraintRig.weight = 1f;
        StateMachine.CompleteReload();
    }
}