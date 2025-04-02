public abstract class PlayerAimedState : PlayerAimedMoveDefaultState
{
    private bool _isCanSetDefault;

    protected PlayerAimedState(AimedMoveStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        StateMachine.Animator.SetBool(GameUtils.IsAiming, true);
        _isCanSetDefault = true;
    }

    public override void Update()
    {
        base.Update();

        if(StateMachine.IsReloading == false)
            SetDefaultAimAnimationSettings();
    }

    private void SetDefaultAimAnimationSettings()
    {
        if (_isCanSetDefault)
        {
            StateMachine.AnimationProperties.VerticalLookRig.weight = 1f;
            StateMachine.AnimationProperties.LeftHandHint.localPosition = StateUtils.LeftHandHintAimDefaultPosition;
            StateMachine.AnimationProperties.LeftHandTarget.localPosition = StateUtils.LeftHandTargetAimDefaultPosition;
            StateMachine.AnimationProperties.LeftHandTarget.localRotation = StateUtils.LeftHandTargetAimDefaultRotation;

            _isCanSetDefault = false;
        }
    }
}