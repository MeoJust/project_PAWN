
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    Player _player;
    Player_IA _controls;

    [Header("Layer Mask")]
    [SerializeField] LayerMask _layerMask;

    [Header("Aim GO")]
    [SerializeField] Transform _aimGO;
    
    [Header("Aim Position Offset")]
    [SerializeField] float _aimPositionOffset = 1.5f;

    Vector2 _aimInput;
    Vector3 _lookDir;

    void Start()
    {
        _player = GetComponent<Player>();
        _controls = _player.Controls;

        _controls.onFoot.look.performed += ctx => _aimInput = ctx.ReadValue<Vector2>();
        _controls.onFoot.look.canceled += ctx => _aimInput = Vector2.zero;
    }

    void Update()
    {
        //_aimGO.position = new Vector3(GetMousePosition().x, transform.position.y + _player.WpController.AimHeight, GetMousePosition().z);

        Vector3 targetPosition = GetMousePosition();
        Vector3 playerPosition = transform.position;

        // Вычисляем направление от игрока к целевой позиции (только по X и Z)
        Vector3 direction = new Vector3(targetPosition.x - playerPosition.x, 0, targetPosition.z - playerPosition.z);
        float distance = direction.magnitude;

        // Если расстояние меньше минимального, ограничиваем его
        if (distance < _aimPositionOffset && distance > 0)
        {
            direction = direction.normalized * _aimPositionOffset;
        }

        // Устанавливаем позицию с учетом минимального расстояния и высоты прицеливания
        _aimGO.position = new Vector3(
            playerPosition.x + direction.x,
            playerPosition.y + _player.WpController.AimHeight,
            playerPosition.z + direction.z
        );
    }

    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(_aimInput);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
