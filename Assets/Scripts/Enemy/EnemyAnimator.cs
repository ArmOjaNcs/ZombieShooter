using System;
using System.Linq;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private const int DefaultLayer = -1;

    private RigAdjusterForAnimation _rigAdjusterForBackStandingUpAnimation;
    private RigAdjusterForAnimation _rigAdjusterForFrontStandingUpAnimation;
    private Animator _animator;
    private Transform _hipsBone;
    private EnemyDistanceTracker _distanceTracker;

    private Action _standingUpCallback;

    public event Action StandingUpAnimationEnded;

    private bool IsFrontUp => Vector3.Dot(_hipsBone.up, Vector3.up) > 0;

    private void OnDisable()
    {
        if(_distanceTracker != null)
            _distanceTracker.MinimalDistanceReached -= OnDistanceIsMinimal;
    }

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);
        _distanceTracker = GetComponent<EnemyDistanceTracker>();
        _distanceTracker.MinimalDistanceReached += OnDistanceIsMinimal;

        AnimationClip[] currentClips = _animator.runtimeAnimatorController.animationClips;
        Transform[] bones = _hipsBone.GetComponentsInChildren<Transform>();

        _rigAdjusterForBackStandingUpAnimation = new RigAdjusterForAnimation
            (currentClips.First(clip => clip.name == GameUtils.BackStandUp), bones, this);
        _rigAdjusterForFrontStandingUpAnimation = new RigAdjusterForAnimation
            (currentClips.First(clip => clip.name == GameUtils.FrontStandUp), bones, this);
    }

    private void OnDistanceIsMinimal(bool isMinimalDistance)
    {
        if (isMinimalDistance)
            StopRun();
        else
            StartRun();
    }

    public void StartRun() 
    {
        _animator.SetBool(GameUtils.Running, true);
        _animator.SetBool(GameUtils.Idle, false);
    } 

    public void StopRun() 
    {
        _animator.SetBool(GameUtils.Running, false);
        _animator.SetBool(GameUtils.Idle, true);
    }
    
    public void EnableAnimator() => _animator.enabled = true;
    public void DisableAnimator() => _animator.enabled = false;

    public void OnStandingUpAnimationEnded()
    {
        _standingUpCallback?.Invoke();
        StandingUpAnimationEnded?.Invoke();
    }

    public void PlayStandingUp(Action adjustAnimationEndedCallback = null, Action animationEndedCallback = null)
    {
        AdjustParentPositionToHipsBone();
        AdjustParentRotationToHipsBone();

        _standingUpCallback = animationEndedCallback;

        if (IsFrontUp)
            _rigAdjusterForFrontStandingUpAnimation.Adjust(() =>
            CallbackForAdjustStandingAnimation(GameUtils.FrontStandUp, adjustAnimationEndedCallback));
        else
            _rigAdjusterForBackStandingUpAnimation.Adjust(() =>
            CallbackForAdjustStandingAnimation(GameUtils.BackStandUp, adjustAnimationEndedCallback));
    }

    private void CallbackForAdjustStandingAnimation(string clipName, Action additionalCallback)
    {
        additionalCallback?.Invoke();
        _animator.Play(clipName, DefaultLayer, 0f);
    }

    private void AdjustParentPositionToHipsBone()
    {
        Vector3 initHipsPosition = _hipsBone.position;
        transform.position = initHipsPosition;
        AdjustParentPositionRelativeGround();
        _hipsBone.position = initHipsPosition;
    }

    private void AdjustParentPositionRelativeGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, GameUtils.MaxDistanceForGroundRaycast, 
            1 << LayerMask.NameToLayer(GameUtils.Ground)))
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
    }

    private void AdjustParentRotationToHipsBone()
    {
        Vector3 initHipsPosition = _hipsBone.position;
        Quaternion initHipsRotation = _hipsBone.rotation;
        Vector3 directionForRotate = _hipsBone.up;

        if (IsFrontUp == false)
            directionForRotate *= GameUtils.MinusSign;

        directionForRotate.y = 0;
        Quaternion correctionRotation = Quaternion.FromToRotation(transform.forward, directionForRotate.normalized);
        transform.rotation *= correctionRotation;
        _hipsBone.position = initHipsPosition;
        _hipsBone.rotation = initHipsRotation;
    }
}
