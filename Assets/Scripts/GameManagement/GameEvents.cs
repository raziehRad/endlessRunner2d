
    using System;
    using System.Collections.Generic;
    using DefaultNamespace;
    using UnityEngine;

    public class GameEvents : MonoBehaviour
    {
        // Player
        public static Action<int> OnHealthChanged;
        public static Action<PlayerState> OnPlayerDied;

        // Score
        public static Action<int> OnScoreChanged;
        public static Action<int> OnCoinChanged;

        // Speed
        public static Action<float> OnSpeedChanged;
        public static Action<bool> OnSwitchBoosted;
        public static Action<float> OnShieldBoosted;
        public static Action<float> OnPowerUp;
        public static Action<float> OnFlyingBoosted;
        public static Action<ItemData,Collider2D> OnItemCollected;
        public static Action OnSetCharacter;
        public static Action<GameObject> OnReleaseItem;
        public static Action OnHitEnemy;
        public static Action<int> OnSpeedAddCoin;
        public static Func<IReadOnlyList<CharacterData>> OnGetCharacter;
    }
