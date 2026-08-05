using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBoost : MonoBehaviour
    {
        [SerializeField] private float boostDuration = 3f;
        [SerializeField] private GroundManager ground;
        [SerializeField] private GameObject shieldObject;
        private PlayerHealth _playerHealth;
        private float timer;
        private bool isBoosted;
        private bool isShieldOn;
        private bool isPowerUpOn;
        private bool isFlyingOn;

        private void OnEnable()
        {
            _playerHealth = GetComponent<PlayerHealth>();
        }

        public void EnableBoost()
        {
            isBoosted = true;
            
           // HUDManager.Instance.SwitchBoosted(true);
            StartCoroutine(BoostTimer());
            
            var speed= ground.Mover.MoveSpeed + 2;
            ground.SetMoveSpeed(speed) ;
        }

        private IEnumerator BoostTimer()
        {
            yield return new WaitForSeconds(boostDuration);
            DisableBoost();
        }

        public void DisableBoost()
        {
            if (!isBoosted)return;

            isBoosted = false;
            StopCoroutine(BoostTimer());
            ground.SetMoveSpeed(5) ;
            GameEvents.OnSwitchBoosted?.Invoke(false);
          //  HUDManager.Instance.SwitchBoosted(false);
        }

        public void EnableShield(float itemDuration)
        {
            if (isShieldOn) return;
            
            isShieldOn = true;
            shieldObject.SetActive(true);
            GameEvents.OnShieldBoosted?.Invoke(itemDuration);
            //HUDManager.Instance.ShieldBoosted(itemDuration);
            StartCoroutine(ShieldCoroutine(itemDuration));
        }

        private IEnumerator ShieldCoroutine(float itemDuration)
        {
            _playerHealth.SetShield(true);
            yield return new WaitForSeconds(itemDuration);
            _playerHealth.SetShield(false);
            shieldObject.SetActive(false);
            isShieldOn = false;
        }

        public void EnablePowerUp(ItemData itemData)
        {
            if (isPowerUpOn) return;
            isPowerUpOn = true;
            GameEvents.OnPowerUp?.Invoke(itemData.duration);
           // HUDManager.Instance.PowerUpBoosted(itemData.duration);
            StartCoroutine(PowerUpCoroutine(itemData));
        }

        private IEnumerator PowerUpCoroutine(ItemData itemData)
        {
            GameManager.Instance.Player.SetPowerUp(itemData,true);
            yield return new WaitForSeconds(itemData.duration);
            isPowerUpOn = false;
            GameManager.Instance.Player.SetPowerUp(itemData,false);
        }

        public void EnableFlying(ItemData item)
        {
            if (isFlyingOn) return;
            isFlyingOn = true;
            GameEvents.OnFlyingBoosted?.Invoke(item.duration);
            //HUDManager.Instance.FlyingBoosted(item.duration);
            StartCoroutine(FlyingCoroutine(item));
        }

        private IEnumerator FlyingCoroutine(ItemData itemData)
        {
            GameManager.Instance.Player.SetFlying(true,itemData.value);
            yield return new WaitForSeconds(itemData.duration);
            isFlyingOn = false;
            GameManager.Instance.Player.SetFlying(false,itemData.value);
        }
    }
}