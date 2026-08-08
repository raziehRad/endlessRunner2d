using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerHealth : MonoBehaviour ,IDamageable
    {
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;
        private bool isShieldOn;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void SetShield(bool value)
        {
            isShieldOn = value;
        }
        public void TakeDamage(int damage)
        {
            if (isShieldOn) return;
            
            currentHealth -= damage;
            GameEvents.OnHitEnemy?.Invoke();
            GameEvents.OnHealthChanged?.Invoke(currentHealth);
            GameEvents.OnCoinChanged?.Invoke(currentHealth);
            if (currentHealth<=0)
            {
                GameEvents.OnPlayerDied?.Invoke(PlayerState.Die);
            }
        }

        public void Heal(int itemValue)
        {
            if (currentHealth >= maxHealth) return;
            currentHealth += itemValue;
            if (currentHealth>maxHealth)
                currentHealth = maxHealth;
            GameEvents.OnHealthChanged?.Invoke(currentHealth);
        }
    }
}