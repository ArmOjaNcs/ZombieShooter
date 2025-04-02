using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationProperties : MonoBehaviour
{
    [SerializeField] private Transform _leftHandTarget;
    [SerializeField] private Transform _leftHandHint;
    [SerializeField] private Rig _verticalLookRig;
    [SerializeField] private Rig _leftHandConstraintRig;

    public Transform LeftHandTarget => _leftHandTarget;
    public Transform LeftHandHint => _leftHandHint;
    public Rig VerticalLookRig => _verticalLookRig;
    public Rig LeftHandConstraintRig => _leftHandConstraintRig;
}