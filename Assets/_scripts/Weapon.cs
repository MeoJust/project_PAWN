using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player Player { get; private set; }
    public Animator Animator { get; private set; }

    [Header("Id")]
    [SerializeField] string _id;

    [Header("Damage")]
    [SerializeField] float _damageMin;
    [SerializeField] float _damageMax;

    void Start()
    {
        Player = GetComponentInParent<Player>();
        Animator = Player.Animator;
    }
}
