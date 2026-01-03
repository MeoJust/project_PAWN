using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Id")]
    [SerializeField] string _id;

    [Header("Damage")]
    public float DamageMin;
    public float DamageMax;
}
