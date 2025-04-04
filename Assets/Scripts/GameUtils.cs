public static class GameUtils 
{
    public static readonly int StartExtraAmmo = 2000;
    public static readonly int StickmanLayer = 3;
    public static readonly int EnemyLayer = 10;
    public static readonly int PlayerAnimatorArmsLayer = 2;
    public static readonly float StraightAngle = 90;
    public static readonly float MinDistanceFromNormal = 0.01f;
    public static readonly float BulletForceForRigidbody = 50;
    public static readonly float UnfoldedAngle = 180;
    public static readonly float DividerForVerticalMouseInput = 100;
    public static readonly float MinVerticalControllerPosition = 0;
    public static readonly float MaxVerticalControllerPosition = 1.5f;
    public static readonly float MinThresholdForClampMouseInput = -1;
    public static readonly float MaxThresholdForClampMouseInput = 1;
    public static readonly float SeedForIncrement = 0.1f;
    public static readonly float MinValueForVerticalVelocityOnGround = -5f;
    public static readonly float MinusSign = -1f;
    public static readonly float MaxDistanceForGroundRaycast = 5f;
    public static readonly string Ground = nameof(Ground);
    public static readonly string BackStandUp = nameof(BackStandUp);
    public static readonly string FrontStandUp = nameof(FrontStandUp);
    public static readonly string Idle = nameof(Idle);
    public static readonly string Running = nameof(Running);
    public static readonly string Forward = nameof(Forward);
    public static readonly string Right = nameof(Right);
    public static readonly string IsAiming = nameof(IsAiming);
    public static readonly string Reload = nameof(Reload);
    public static readonly string IsJump = nameof(IsJump);
    public static readonly string Jump = nameof(Jump);
    public static readonly string IsRotate = nameof(IsRotate);
    public static readonly string Rotation = nameof(Rotation);
}