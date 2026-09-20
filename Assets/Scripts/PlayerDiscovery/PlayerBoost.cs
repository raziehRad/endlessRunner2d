using System;
using System.Collections;
using UnityEngine;


    // Handles activating and managing player boosts
    public class PlayerBoost : MonoBehaviour
    {
        [SerializeField] private GameObject shieldObject;
        [SerializeField] private Weapon weapon;
        private PlayerMovement movement;
        private PlayerHealth _playerHealth;
        private bool isShieldOn;
        private bool isPowerUpOn;
        private bool isFlyingOn;

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            movement = GetComponent<PlayerMovement>();
        }
        // Activate a boost and manage its duration
        public void EnableBoost(BoostType boostType, ItemData item = null)
        {
            if (item == null)
            {
                Debug.LogError("ItemData is null.");
                return;
            }
            switch (boostType)
            {
                case BoostType.shield:
                    if (isShieldOn) return;
                    isShieldOn = true;
                    shieldObject.SetActive(true);
                    GameEvents.OnShieldBoosted?.Invoke(item.duration);
                    StartCoroutine(RunBoost(
                        item.duration,
                        () => _playerHealth.SetShield(true),
                        () =>
                        {
                            _playerHealth.SetShield(false);
                            shieldObject.SetActive(false);
                            isShieldOn = false;
                        }));
                    break;
                case BoostType.powerUp:
                    if (isPowerUpOn) return;
                    isPowerUpOn = true;
                    GameEvents.OnPowerUp?.Invoke(item.duration);
                    StartCoroutine(RunBoost(
                        item.duration,
                        () =>  weapon.SetPowerUp(item, true),
                        () =>
                        {
                            isPowerUpOn = false;
                            weapon.SetPowerUp(item, false);
                        }));
                    break;

                case BoostType.flying:
                    if (isFlyingOn) return;
                    isFlyingOn = true;
                    GameEvents.OnFlyingBoosted?.Invoke(item.duration);
                    StartCoroutine(RunBoost(
                        item.duration,
                        () =>    movement.SetFlyingMode(true, item.value),
                        () =>
                        {
                            isFlyingOn = false;
                            movement.SetFlyingMode(false, item.value);
                        }));
                    break;
            }
        }
        // Run a boost for a specific duration
        private IEnumerator RunBoost(float duration, Action onStart, Action onEnd)
        {
            onStart?.Invoke();
            yield return new WaitForSeconds(duration);
            onEnd?.Invoke();
        }
    }


public enum BoostType
{
    flying,powerUp,shield
}