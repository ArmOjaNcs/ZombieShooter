using UnityEngine;

public class Effect : MonoBehaviour 
{
    private protected bool IsFinished;
    private protected Quaternion DefaultRotation;
    private protected Vector3 DefaultScale;

    private protected virtual void Awake()
    {
        DefaultRotation = transform.rotation;
        DefaultScale = transform.localScale;
    }

    private protected virtual void OnEnable()
    {
        IsFinished = false;
        transform.parent = null;
        transform.rotation = DefaultRotation;
        transform.localScale = DefaultScale;
    }
}