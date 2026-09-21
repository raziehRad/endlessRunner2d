using System;
using UnityEngine;
// Manages the player's speed progression based on collected coins
    public class PlayerSpeed  : MonoBehaviour
    {
        [Header("Speed")]
        [SerializeField] private float baseSpeed = 5f;
        [SerializeField] private float speedPerLevel = 0.5f;
        [SerializeField] private int coinsPerLevel = 10;
        [SerializeField] private float minSpeed = 5f;
        [SerializeField] private float maxSpeed = 15f;
        private int coinCount;
        private int speedLevel;

        private void OnEnable()
        {
            GameEvents.OnHitEnemy += HitEnemy;
            GameEvents.OnSpeedAddCoin += AddCoin;
        }
        private void OnDisable()
        {
            GameEvents.OnHitEnemy -= HitEnemy;
            GameEvents.OnSpeedAddCoin -= AddCoin;
        }
        private void Start()
        {
            UpdateSpeed();
        }
        // Increase the speed level based on collected coins
        private void AddCoin(int amount = 1)
        {
            coinCount += amount;

            int maxLevel = Mathf.FloorToInt((maxSpeed - baseSpeed) / speedPerLevel);
            speedLevel = Mathf.Min(coinCount / coinsPerLevel, maxLevel);
            UpdateSpeed();
        }

        private void HitEnemy()
        {
            speedLevel = Mathf.Max(0, speedLevel - 1);
            var coincount = speedLevel * coinsPerLevel;

            UpdateSpeed();
        }

        private void UpdateSpeed()
        {
            float speed = baseSpeed + speedLevel * speedPerLevel;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            GameEvents.OnSpeedChanged?.Invoke((int)speed);
        }
    }
