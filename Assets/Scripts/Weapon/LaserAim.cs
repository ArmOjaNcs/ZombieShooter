using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class LaserAim : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private LayerMask _layerMask;

    private LineRenderer _laserAim;
    private Transform _startPoint;
    private float _firingRange;

    private void Awake()
    {
        _laserAim = GetComponent<LineRenderer>();
        _laserAim.enabled = false;
        _aim.gameObject.SetActive(false);
        GameObject newGameObject = new GameObject();
        _startPoint = newGameObject.transform;
    }

    public void SetPoints(Transform firePoint, float firingRange)
    {
        _startPoint = firePoint;
        _firingRange = firingRange;
        _endPoint.localPosition = _startPoint.localPosition;
        _endPoint.localRotation = _startPoint.localRotation;

        if (IsVectorHasNegativeComponents(_endPoint.localPosition))
            _endPoint.localPosition = _endPoint.forward * - _firingRange;
        else
            _endPoint.localPosition = _endPoint.forward * _firingRange;
    }

    public void DrawLaserAim(bool isAimed)
    {
        if (isAimed)
        {
            _laserAim.enabled = true;
            _laserAim.SetPosition(0, _startPoint.position);
            _laserAim.SetPosition(1, _endPoint.position);

            if (IsHitCollider(out RaycastHit hitInfo))
            {
                _laserAim.SetPosition(1, hitInfo.point);
                _aim.transform.position = hitInfo.point;
                _aim.gameObject.SetActive(true);
            }
            else
            {
                _aim.gameObject.SetActive(false);
            }
        }
        else
        {
            _laserAim.enabled = false;
            _aim.gameObject.SetActive(false);
        }
    }

    public bool IsHitCollider(out RaycastHit hitInfo)
    {
        return (Physics.Raycast(_startPoint.position, _startPoint.forward, out hitInfo, _firingRange, _layerMask, QueryTriggerInteraction.Ignore));
    }

    private bool IsVectorHasNegativeComponents(Vector3 vector)
    {
        if (vector.x < 0)
            return true;

        if (vector.y < 0)
            return true;

        if (vector.z < 0)
            return true;

        return false;
    }
}