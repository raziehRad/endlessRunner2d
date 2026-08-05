
    using System;
    using UnityEngine;

    public class GameEvents : MonoBehaviour
    {
        // Player
        public static Action<int> OnHealthChanged;
        public static Action OnPlayerDied;

        // Score
        public static Action<int> OnScoreChanged;
        public static Action<int> OnCoinChanged;

        // Speed
        public static Action<int> OnSpeedChanged;
        public static Action<bool> OnSwitchBoosted;
        public static Action<float> OnShieldBoosted;
        public static Action<float> OnPowerUp;
        public static Action<float> OnFlyingBoosted;
    }
