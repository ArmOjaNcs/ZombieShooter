using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _firingRange;
    [SerializeField] private float _delay;
    [SerializeField] private float _damage;
    [SerializeField] private int _clipSize;
    [SerializeField] private AimCameraTarget _aimCameraTarget;

    private WeaponAmmo _weaponAmmo;
    private WeaponEffects _weaponEffects;
    private LaserAim _laserAim;
    private float _currentDelay;
    private bool _isAimed;
   
    private void Awake()
    {
        _currentDelay = _delay;
        _weaponAmmo = GetComponentInParent<WeaponAmmo>();
        _weaponEffects = GetComponent<WeaponEffects>();
        _laserAim = GetComponentInParent<LaserAim>();
        _weaponAmmo.SetClipSize(_clipSize);
    }

    private void Start()
    {
        _laserAim.SetPoints(_firePoint, _firingRange);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        WaitBeforeShoot();
        _weaponEffects.StopMuzzleEffect();
    }

    private void LateUpdate()
    {
        _laserAim.DrawLaserAim(_isAimed);
    }

    public void TakeAim(bool isAimed)
    {
        _isAimed = isAimed;
    }

    public void Shoot()
    {
        if (_currentDelay < 0 && _weaponAmmo.IsHasBullets)
        {
            PerformShot(_firePoint.position, _firePoint.forward);
            _aimCameraTarget.TriggerRecoil();
            _currentDelay = _delay;
            _weaponEffects.GetShootEffectAudioPlayer().AudioSource.Play();
            _weaponEffects.PlayMuzzleEffect();
            _weaponAmmo.SpendAmmo();
        }
    }

    private void PerformShot(Vector3 shootPosition, Vector3 shootDirection)
    {
        if (_laserAim.IsHitCollider(out RaycastHit hitInfo))
        {
            Enemy enemy = hitInfo.collider.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                BloodEffect bloodEffect = _weaponEffects.GetBloodEffect();
                bloodEffect.transform.position = hitInfo.point + hitInfo.normal * GameUtils.MinDistanceFromNormal;
                bloodEffect.transform.LookAt(hitInfo.point);
                bloodEffect.transform.Rotate(Vector3.right, GameUtils.UnfoldedAngle, Space.Self);
                hitInfo.normal.Scale(bloodEffect.transform.localScale);
                bloodEffect.transform.SetParent(hitInfo.transform);

                if (enemy.Health.CurrentValue > _damage)
                {
                    enemy.Health.TakeDamage(_damage);
                }
                else
                {
                    Vector3 forceDirection = (hitInfo.point - shootPosition).normalized;
                    forceDirection.y = 0;
                    enemy.Kill(forceDirection * GameUtils.BulletForceForRigidbody, hitInfo.point);
                }
                
                return;
            }

            DecalEffect decalEffect = _weaponEffects.GetDecalEffect();
            decalEffect.transform.position = hitInfo.point + hitInfo.normal * GameUtils.MinDistanceFromNormal;
            decalEffect.transform.LookAt(hitInfo.point);
            decalEffect.transform.Rotate(Vector3.up, GameUtils.UnfoldedAngle, Space.Self);
            hitInfo.normal.Scale(decalEffect.transform.localScale);
            decalEffect.transform.SetParent(hitInfo.transform);

            Rigidbody rigidbody = hitInfo.collider.GetComponentInParent<Rigidbody>();

            if (rigidbody != null)
                rigidbody.AddForceAtPosition(shootDirection * GameUtils.BulletForceForRigidbody, hitInfo.point);
        }
    }

    private void WaitBeforeShoot()
    {
        _currentDelay -= Time.deltaTime;
    }
}