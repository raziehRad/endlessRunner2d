using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerHealth : MonoBehaviour ,IDamageable
    {
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;

        private PlayerStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = GetComponent<PlayerStateMachine>();
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            HUDManager.instace.SetPlayerHealth(currentHealth);

            if (currentHealth<=0)
            {
                _stateMachine.ChangeState(PlayerState.Die);
            }
        }
    }
}