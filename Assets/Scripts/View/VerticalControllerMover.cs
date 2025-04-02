using UnityEngine;

public class VerticalControllerMover : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _speed;

    private float _verticalPosition;
    private float _defaultVerticalPosition;

    private void Awake()
    {
        _defaultVerticalPosition = transform.localPosition.y;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x,
            _verticalPosition, transform.localPosition.z), _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _input.VerticalLook += OnVerticalLook;
    }

    private void OnVerticalLook(float verticalDirection)
    {
        if (verticalDirection > 0 || verticalDirection < 0)
            _verticalPosition += verticalDirection / GameUtils.DividerForVerticalMouseInput;
        
        _verticalPosition = Mathf.Clamp(_verticalPosition, GameUtils.MinVerticalControllerPosition, GameUtils.MaxVerticalControllerPosition);
    }

    private void OnDisable()
    {
        _input.VerticalLook -= OnVerticalLook;
        _verticalPosition = _defaultVerticalPosition;
        transform.localPosition = new Vector3(transform.localPosition.x, _verticalPosition, transform.localPosition.z);
    }
}
