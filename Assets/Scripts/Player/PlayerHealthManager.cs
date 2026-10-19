using System;
using TMPro;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour 
{
    [SerializeField] private float maxHealth;
    [SerializeField] private TextMeshProUGUI hpText;

    public event Action onDeath;

    public event Action onHealthChanged;

    public float CurrentHealth { get; set; }
    public bool IsDead { get; set; }

    public void StartPlayerHealthManager()
    {
        CurrentHealth = maxHealth;
        IsDead = false;
    }

    private void OnEnable()
    {
        onHealthChanged += PlayDamageSound;
    }

    private void OnDisable()
    {
        onHealthChanged -= PlayDamageSound;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead || CurrentHealth <= 0) return; 

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        hpText.text = CurrentHealth.ToString("F0") + "%";

        onHealthChanged?.Invoke();

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        IsDead = true;
        LoadingManager.LoadScene("DEATH");
        onDeath?.Invoke();
    }

    private void PlayDamageSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.TakeDamage, AudioManager.Instance.sfxVolume);
    }
}
