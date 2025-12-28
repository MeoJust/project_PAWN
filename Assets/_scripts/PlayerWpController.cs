using UnityEngine;

public class PlayerWpController : MonoBehaviour
{
    Player _player;
    Player_IA _controls;
    Animator _animator;
    WP_Range _wpRange;

    bool _hasPistol;
    bool _hasRifle;
    bool _isAiming;
    bool _isAimingHi;
    bool _isAimingLow;
    bool _wasAiming;

    void Awake()
    {

    }

    void Start()
    {
        _player = GetComponent<Player>();
        _controls = _player.Controls;
        _animator = GetComponent<Animator>();

        _controls.onFoot.aim.performed += ctx => _isAiming = true;
        _controls.onFoot.aim.canceled += ctx => _isAiming = false;

        _wpRange = GetComponentInChildren<WP_Range>();

        if (_wpRange != null)
        {
            _hasPistol = _wpRange.IsPistol;
            _hasRifle = _wpRange.IsRifle;
            _isAimingHi = _wpRange.IsRifleHi;
            _isAimingLow = _wpRange.IsRifleLow;

            _animator.SetBool("hasPistol", _hasPistol);
            _animator.SetBool("hasRifle", _hasRifle);
        }
    }

    void Update()
    {
        SetAiming();
        _wasAiming = _isAiming;
    }

    void SetAiming()
    {
        float aimSpeed = _wpRange != null ? _wpRange.AimSpeed : 0.25f;

        if (_hasPistol)
        {
            if (_isAiming && !_wasAiming)
            {
                // Устанавливаем параметр и переходим с индивидуальной скоростью оружия
                _animator.SetBool("isAimingHi", true);
                _animator.CrossFade("player_pistolAim", aimSpeed);
            }
            else if (!_isAiming && _wasAiming)
            {
                // Устанавливаем параметр и возвращаемся с индивидуальной скоростью оружия
                _animator.SetBool("isAimingHi", false);
                _animator.CrossFade("player_pistolIdle", aimSpeed);
            }
        }
        if (_hasRifle)
        {
            if (_isAiming && !_wasAiming)
            {
                // Устанавливаем параметры и переходим с индивидуальной скоростью оружия
                if (_isAimingHi)
                {
                    _animator.SetBool("isAimingHi", true);
                    _animator.SetBool("isAimingLow", false);
                    _animator.CrossFade("player_rifleAimHi", aimSpeed);
                }
                else
                {
                    _animator.SetBool("isAimingHi", false);
                    _animator.SetBool("isAimingLow", true);
                    _animator.CrossFade("player_rifleAimLow", aimSpeed);
                }
            }
            else if (!_isAiming && _wasAiming)
            {
                // Устанавливаем параметры и возвращаемся с индивидуальной скоростью оружия
                _animator.SetBool("isAimingHi", false);
                _animator.SetBool("isAimingLow", false);
                _animator.CrossFade("player_rifleIdle", aimSpeed);
            }
        }
    }
}