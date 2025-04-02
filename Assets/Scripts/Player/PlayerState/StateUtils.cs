using UnityEngine;

public static class StateUtils
{
    public static readonly Vector3 LeftHandHintDefaultPosition = new Vector3(-0.121f, 0.361f, 0.185f);
    public static readonly Vector3 LeftHandTargetDefaultPosition = new Vector3(0.008f, 0.066f, 0.024f);
    public static readonly Quaternion LeftHandTargetDefaultRotation = Quaternion.Euler(1.001f, -202.9f, 0.643f);
    public static readonly Vector3 LeftHandHintAimDefaultPosition = new Vector3(0.046f, 0f, 0.165f);
    public static readonly Vector3 LeftHandTargetAimDefaultPosition = new Vector3(0f, 0.066f, 0.009f);
    public static readonly Quaternion LeftHandTargetAimDefaultRotation = Quaternion.Euler(0.073f, -180f, -0.288f);
}