using System;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour 
{
    [SerializeField] private float maxHealth;

    public event Action onDeath;

    public event Action onHealthChanged;

    public float CurrentHealth { get; set; }
    public bool IsDead { get; set; }

    public void Start()
    {
        CurrentHealth = maxHealth;
        IsDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead || CurrentHealth <= 0) return; 

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        onHealthChanged?.Invoke();

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        IsDead = true;
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
