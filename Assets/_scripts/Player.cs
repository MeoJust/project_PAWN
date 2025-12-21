
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player_IA Controls { get; private set; }
    public PlayerAim Aim { get; private set; }

    public Animator Animator { get; private set; }

    void Awake()
    {
        Controls = new Player_IA();
        Aim = GetComponent<PlayerAim>();
        Animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Controls.Enable();
    }
    void OnDisable()
    {
        Controls.Disable();
    }
}
