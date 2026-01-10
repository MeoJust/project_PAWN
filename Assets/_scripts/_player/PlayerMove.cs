
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMove : MonoBehaviour
{
    Player _player;
    Player_IA _controls;

    CharacterController _controller;

    public bool IsRunning { get; private set; }

    [Header("Speed")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _walkSpeed = 4f;
    [SerializeField] float _runSpeed = 10f;
    [SerializeField] float _aimSpeed = 10f;
    [SerializeField] float _runRotationSpeed = 15f;

    [Header("Rigging")]
    [SerializeField] MultiAimConstraint _headMultiAimConstraint;

    Vector3 _moveDir;
    Vector2 _moveInput;
    float _verticalVelocity;
    float _gravity = 9.81f;

    void Start()
    {
        _player = GetComponent<Player>();
        _controls = _player.Controls;
        _controller = GetComponent<CharacterController>();

        _controls.onFoot.move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.onFoot.move.canceled += ctx => _moveInput = Vector2.zero;

        _controls.onFoot.run.performed += ctx => StartRunning();
        _controls.onFoot.run.canceled += ctx => StopRunning();
    }

        void Update()
    {
        Move();
        Rotate();
        SetRigWeight(IsRunning ? 0 : 1f);
    }

    void StartRunning()
    {
        // Нельзя начать бег во время прицеливания
        if (_player.WpController != null && _player.WpController.IsAiming)
        {
            return;
        }
        IsRunning = true;
    }

    public void StopRunning()
    {
        IsRunning = false;
    }

    //TODO: run until has stamina
    void Move()
    {
        _moveDir = new Vector3(_moveInput.x, 0, _moveInput.y);

        ApplyGravity();

        // Если прицеливаемся, останавливаем бег
        if (_player.WpController != null && _player.WpController.IsAiming && IsRunning)
        {
            StopRunning();
        }

        if (_moveDir.magnitude > 0 && !IsRunning)
        {
            _moveSpeed = _walkSpeed;
            _controller.Move(_moveDir * _moveSpeed * Time.deltaTime);
        }
        else if (_moveDir.magnitude > 0 && IsRunning)
        {
            _moveSpeed = _runSpeed;
            _controller.Move(_moveDir * _moveSpeed * Time.deltaTime);
        }
    }

    // void Rotate()
    // {
    //     Vector3 lookDir = _player.Aim.GetMousePosition() - transform.position;
    //     lookDir.y = 0;
    //     lookDir = lookDir.normalized;

    //     transform.forward = Vector3.Lerp(transform.forward, lookDir, _aimSpeed * Time.deltaTime);
    // }

    void Rotate()
    {
        if (IsRunning)
        {
            // При беге поворачиваемся в направлении движения
            if (_moveDir.magnitude > 0.1f)
            {
                Vector3 moveDirection = new Vector3(_moveDir.x, 0, _moveDir.z).normalized;
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, _runRotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            // При ходьбе поворачиваемся в направлении прицеливания
            Vector3 lookDir = _player.Aim.GetMousePosition() - transform.position;
            lookDir.y = 0;
            lookDir = lookDir.normalized;

            transform.forward = Vector3.Lerp(transform.forward, lookDir, _aimSpeed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (!_controller.isGrounded)
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
        }
        else
        {
            _verticalVelocity = -.5f;
        }

        _moveDir.y = _verticalVelocity;
    }

        void SetRigWeight(float weight)
    {
        if (_headMultiAimConstraint != null)
        {
            _headMultiAimConstraint.weight = weight;
        }
    }
}
