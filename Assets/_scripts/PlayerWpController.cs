using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWpController : MonoBehaviour
{
    Player _player;
    Player_IA _controls;
    Animator _animator;
    WP_Range _wpRange;

[Header("Aim Height")]
    [SerializeField] float _defaultAimHeight = 1.25f;
    [SerializeField] float _pistolAimHeight = 1.25f;
    [SerializeField] float _rifleAimHiHeight = 1;
    [SerializeField] float _rifleAimLowHeight = .5f;

    public float AimHeight{get; private set;}

    [Header("Rigging")]
    [SerializeField] MultiAimConstraint _rightHandMultiAimConstraint;

    bool _hasPistol;
    bool _hasRifle;
    bool _isAiming;
    bool _isAimingHi;
    bool _isAimingLow;
    bool _wasAiming;

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

        SetRigWeight(0);
        AimHeight = _defaultAimHeight;
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
                SetRigWeight(1f);
                AimHeight = _pistolAimHeight;
            }
            else if (!_isAiming && _wasAiming)
            {
                // Устанавливаем параметр и возвращаемся с индивидуальной скоростью оружия
                _animator.SetBool("isAimingHi", false);
                _animator.CrossFade("player_pistolIdle", aimSpeed);
                SetRigWeight(0);
                AimHeight = _defaultAimHeight;
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
                    SetRigWeight(1f);
                    AimHeight = _rifleAimHiHeight;
                }
                else
                {
                    _animator.SetBool("isAimingHi", false);
                    _animator.SetBool("isAimingLow", true);
                    _animator.CrossFade("player_rifleAimLow", aimSpeed);
                    SetRigWeight(1f);
                    AimHeight = _rifleAimLowHeight;
                }
            }
            else if (!_isAiming && _wasAiming)
            {
                // Устанавливаем параметры и возвращаемся с индивидуальной скоростью оружия
                _animator.SetBool("isAimingHi", false);
                _animator.SetBool("isAimingLow", false);
                _animator.CrossFade("player_rifleIdle", aimSpeed);
                SetRigWeight(0f);
                AimHeight = _defaultAimHeight;
            }
        }
    }

    void SetRigWeight(float weight)
    {
        if (_rightHandMultiAimConstraint != null)
        {
            _rightHandMultiAimConstraint.weight = weight;
        }
    }
}