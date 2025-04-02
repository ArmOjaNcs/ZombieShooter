public abstract class PlayerFreeMoveState : PlayerFreeMoveDefaultState
{
    private bool _isCanSetDefault;

    protected PlayerFreeMoveState(FreeMoveStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        _isCanSetDefault = true;
    }

    public override void Update()
    {
        base.Update();

        if(StateMachine.IsAimed == false)
            SetDefaultFreeAnimationSettings();
    }

    private void SetDefaultFreeAnimationSettings()
    {
        if (_isCanSetDefault)
        {
            StateMachine.AnimationProperties.VerticalLookRig.weight = 0f;
            StateMachine.AnimationProperties.LeftHandHint.localPosition = StateUtils.LeftHandHintDefaultPosition;
            StateMachine.AnimationProperties.LeftHandTarget.localPosition = StateUtils.LeftHandTargetDefaultPosition;
            StateMachine.AnimationProperties.LeftHandTarget.localRotation = StateUtils.LeftHandTargetDefaultRotation;

            _isCanSetDefault = false;
        }
    }
}