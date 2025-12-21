
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    Player _player;
    Player_IA _controls;

    [SerializeField] LayerMask _layerMask;
    [SerializeField] Transform _aimGO;

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

        _aimGO.position = new Vector3(GetMousePosition().x, transform.position.y + 1.25f, GetMousePosition().z);

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
