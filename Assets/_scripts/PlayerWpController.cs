using UnityEngine;

public class PlayerWpController : MonoBehaviour
{
    Player _player;
    Player_IA _controls;
    Animator _animator;

    bool _hasPistol;
    bool _hasRifle;
    bool _isAiming;
    bool _isAimingHi;
    bool _isAimingLow;

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

        WP_Range wp = GetComponentInChildren<WP_Range>();

        if (wp != null)
        {

            _hasPistol = wp.IsPistol;
            _hasRifle = wp.IsRifle;
            _isAimingHi = wp.IsRifleHi;
            _isAimingLow = wp.IsRifleLow;

            _animator.SetBool("hasPistol", _hasPistol);
            _animator.SetBool("hasRifle", _hasRifle);
        }
    }

    void Update()
    {
        SetAiming();
    }

    //TODO:Настроить скорость анимации прицеливания через код, индивидуально для каждого оружия
    void SetAiming()
    {
        if (_hasPistol)
        {
            if (_isAiming)
            {
                _animator.SetBool("isAimingHi", true);
            }
            else
            {
                _animator.SetBool("isAimingHi", false);
            }
        }
        if (_hasRifle)
        {
            if (_isAiming)
            {
                if (_isAimingHi)
                {
                    _animator.SetBool("isAimingHi", true);
                }
                else
                {
                    _animator.SetBool("isAimingLow", true);
                }
            }
            else
            {
                _animator.SetBool("isAimingHi", false);
                _animator.SetBool("isAimingLow", false);
            }
        }
    }
}