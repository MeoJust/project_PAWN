
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Player _player;
    Player_IA _controls;

    CharacterController _controller;

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _aimSpeed = 10f;

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
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        _moveDir = new Vector3(_moveInput.x, 0, _moveInput.y);

        ApplyGravity();

        if (_moveDir.magnitude > 0)
        {
            _controller.Move(_moveDir * _moveSpeed * Time.deltaTime);
        }
    }

    void Rotate()
    {
        Vector3 lookDir = _player.Aim.GetMousePosition() - transform.position;
        lookDir.y = 0;
        lookDir = lookDir.normalized;

        transform.forward = Vector3.Lerp(transform.forward, lookDir, _aimSpeed * Time.deltaTime);
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
}
