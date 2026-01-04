using UnityEngine;
using System.Collections;

public class WP_Range : Weapon
{
    [Header("Specs")]
    [SerializeField] float _magazineCapacity;
    [SerializeField] float _reloadTime;
    [SerializeField] float _aimingSpeed;
    [SerializeField] float _aimSpeed = .25f;
    [SerializeField] float _fireRate = .25f;

    [Header("Type")]
    [SerializeField] bool _isAuto;
    [SerializeField] bool _isShotgun;

    [Header("Hold Type")]
    [SerializeField] bool _isPistol;
    [SerializeField] bool _isRifle;
    [SerializeField] bool _isRifleHi;
    [SerializeField] bool _isRifleLow;

    [Header("Bullet")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletSpawnPoint;
    [SerializeField] float _bulletForce;
    [SerializeField] float _bulletSpreadMin = .1f;
    [SerializeField] float _bulletSpreadMax = .2f;

    [Header("Shotgun")]
    [SerializeField] int _shotgunPelletCount = 5;

    [Space(10)]
    [SerializeField] GameObject _playerLeftHand;

    bool _isShooting;
    Coroutine _autoFireCoroutine;

    void OnEnable()
    {
        _playerLeftHand.SetActive(false);
    }
    void OnDisable()
    {
        _playerLeftHand.SetActive(true);
        StopShooting();
    }

    void Start()
    {
        // _playerLeftHand.SetActive(false);
    }

    public void Shoot()
    {
        if ((_isPistol && !_isAuto) || (_isRifle && !_isAuto))
        {
            // Pistol - одиночный выстрел
            SpawnBullet();
        }

        if (_isAuto)
        {
            // Auto - одиночный выстрел (автоматическая стрельба управляется через StartShooting/StopShooting)
            SpawnBullet();
        }

        if (_isShotgun)
        {
            // Shotgun - множественный выстрел (5 пуль)
            for (int i = 0; i < _shotgunPelletCount; i++)
            {
                SpawnBullet();
            }
        }
    }

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        
        // Рассчитываем отдельный разброс для каждой пули в горизонтальной плоскости
        // Для top-down шутера разброс применяется только по горизонтальным осям (X и Z)
        // Вертикальная ось Y остается без изменений
        float spreadX = Random.Range(-_bulletSpreadMax, _bulletSpreadMax);
        float spreadZ = Random.Range(-_bulletSpreadMax, _bulletSpreadMax);
        
        // Применяем разброс через углы отклонения в горизонтальной плоскости
        // spreadX - отклонение по оси X (в градусах)
        // spreadZ - отклонение по оси Z (в градусах)
        // Y остается 0, чтобы не было вертикального разброса
        Vector3 spreadDirection = Quaternion.Euler(0, spreadX, spreadZ) * _bulletSpawnPoint.forward;
        
        bullet.GetComponent<Rigidbody>().AddForce(spreadDirection * _bulletForce, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().Damage = Random.Range(DamageMin, DamageMax);
    }

    public void StartShooting()
    {
        if (_isAuto && !_isShooting)
        {
            _isShooting = true;
            _autoFireCoroutine = StartCoroutine(AutoFireCoroutine());
        }
    }

    public void StopShooting()
    {
        if (_isAuto && _isShooting)
        {
            _isShooting = false;
            if (_autoFireCoroutine != null)
            {
                StopCoroutine(_autoFireCoroutine);
                _autoFireCoroutine = null;
            }
        }
    }

    IEnumerator AutoFireCoroutine()
    {
        while (_isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(_fireRate);
        }
    }

    public bool IsPistol => _isPistol;
    public bool IsRifle => _isRifle;
    public bool IsRifleHi => _isRifleHi;
    public bool IsRifleLow => _isRifleLow;
    public bool IsAuto => _isAuto;
    public float AimSpeed => _aimSpeed;
}
