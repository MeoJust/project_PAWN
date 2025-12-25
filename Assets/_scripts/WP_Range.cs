using UnityEngine;

public class WP_Range : Weapon
{
    [Header("Specs")]
    [SerializeField] float _magazineCapacity;
    [SerializeField] float _reloadTime;
    [SerializeField] float _aimingSpeed;

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
    [SerializeField] float _bulletForce;

    public bool IsPistol => _isPistol;
    public bool IsRifle => _isRifle;
    public bool IsRifleHi => _isRifleHi;
    public bool IsRifleLow => _isRifleLow;
}
