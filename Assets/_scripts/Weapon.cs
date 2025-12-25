using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Id")]
    [SerializeField] string _id;

    [Header("Damage")]
    [SerializeField] float _damageMin;
    [SerializeField] float _damageMax;
}
