using System;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [NonSerialized] public float _currenthealth;
    public float _maxHealth;

    private void Awake()
    {
        Reset();
    }
    public void TakeDamage(float Damage)
    {
        
        _currenthealth -= Damage;
        Debug.Log(_currenthealth);
        if (_currenthealth <= 0 )
        {
            Die();
        }
    }

    public void Reset()
    {
        _currenthealth = _maxHealth;
    }
    // Start is called before the first frame update
    private void Die()
    {
        Destroy(gameObject);
    }
}
