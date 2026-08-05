
    using UnityEngine;

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
        private void Start()
        {
            UpdateSpeed();
        }
        public void AddCoin(int amount = 1)
        {
            coinCount += amount;

            int maxLevel = Mathf.FloorToInt((maxSpeed - baseSpeed) / speedPerLevel);
            speedLevel = Mathf.Min(coinCount / coinsPerLevel, maxLevel);
        }

        public void HitEnemy()
        {
            speedLevel = Mathf.Max(0, speedLevel - 1);
            coinCount = speedLevel * coinsPerLevel;

            UpdateSpeed();
        }

        private void UpdateSpeed()
        {
            float speed = baseSpeed + speedLevel * speedPerLevel;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            
            GameManager.Instance.GroundManager.SetMoveSpeed(speed);
            GameEvents.OnSpeedChanged?.Invoke((int)speed);
            //GameManager.Instance.HUDManager.SpeedTxt((int)speed);
        }
    }
