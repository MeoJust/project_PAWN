using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float _maxHealth = 100f;
    float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Dead");
    }
}