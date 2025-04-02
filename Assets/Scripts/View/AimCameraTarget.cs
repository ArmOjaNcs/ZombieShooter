using UnityEngine;

public class AimCameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _kickBackAmount = -1;
    [SerializeField] private float _kickBackSpeed = 10;
    [SerializeField] private float _returnSpeed = 20;

    private float _currentRecoilPosition;
    private float _finalRecoilPosition;
    private float _defaultPosition;

    private void Awake()
    {
        _defaultPosition = transform.localPosition.z;
    }

    private void Update()
    {
        transform.LookAt(_target);
        ReturnAfterRecoil();
    }

    public void TriggerRecoil()
    {
        _currentRecoilPosition += _kickBackAmount;
    }

    private void ReturnAfterRecoil()
    {
        _currentRecoilPosition = Mathf.Lerp(_currentRecoilPosition, _defaultPosition, _returnSpeed * Time.deltaTime);
        _finalRecoilPosition = Mathf.Lerp(_finalRecoilPosition, _currentRecoilPosition, _kickBackSpeed * Time.deltaTime);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _finalRecoilPosition);
    }
}