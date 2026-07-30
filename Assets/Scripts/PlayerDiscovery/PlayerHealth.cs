using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerHealth : MonoBehaviour ,IDamageable
    {
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;

        private PlayerStateMachine _stateMachine;

        private bool isShieldOn;

        
        private void Awake()
        {
            _stateMachine = GetComponent<PlayerStateMachine>();
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
            GameManager.Instance.GroundManager.PlayerSpeed.HitEnemy();
            HUDManager.Instance.SetPlayerHealth(currentHealth);
            HUDManager.Instance.SetItemCount(0);
            if (currentHealth<=0)
            {
                _stateMachine.ChangeState(PlayerState.Die);
            }
        }

        public void Heal(int itemValue)
        {
            if (currentHealth >= maxHealth) return;
            currentHealth += itemValue;
            if (currentHealth>maxHealth)
                currentHealth = maxHealth;
            HUDManager.Instance.SetPlayerHealth(currentHealth);
        }
    }
}