using UnityEngine;

public class ViewChanger : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Transform _freeLookCamera;
    [SerializeField] private Transform _aimCamera;
    [SerializeField] private Transform _verticalMover;

    public bool IsFreeMode {  get; private set; }

    private void OnEnable()
    {
        _playerMover.Aimed += OnTakeAim;
    }

    private void OnDisable()
    {
        _playerMover.Aimed -= OnTakeAim;
    }

    private void OnTakeAim(bool isTakeAim)
    {
        if (isTakeAim)
        {
            _freeLookCamera.gameObject.SetActive(false);
            _aimCamera.gameObject.SetActive(true);
            _verticalMover.gameObject.SetActive(true);
        }
        else
        {
            _freeLookCamera.gameObject.SetActive(true);
            _aimCamera.gameObject.SetActive(false);
            _verticalMover.gameObject.SetActive(false);
        }
    }
}