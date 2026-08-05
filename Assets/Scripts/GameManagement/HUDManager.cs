
    using System;
    using System.Collections;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class HUDManager : MonoBehaviour
    {
        [SerializeField]protected HUDPlayer _hUDPlayer;
      [SerializeField]protected HUDScore _HUDScore;
      [SerializeField] protected HUDBoost _HUDBoost;

      private void OnEnable()
        {
            GameEvents.OnHealthChanged += _hUDPlayer.SetPlayerHealth;
            GameEvents.OnScoreChanged += _HUDScore.SetPlayerScore;
            GameEvents.OnCoinChanged += _hUDPlayer.SetItemCount;
            GameEvents.OnSpeedChanged += _hUDPlayer.SpeedTxt;
            GameEvents.OnSwitchBoosted += _HUDBoost.SwitchBoosted;
            GameEvents.OnShieldBoosted += _HUDBoost.ShieldBoosted;
            GameEvents.OnPowerUp +=_HUDBoost. PowerUpBoosted;
            GameEvents.OnFlyingBoosted +=_HUDBoost. FlyingBoosted;
        }

        private void OnDisable()
        {
            GameEvents.OnHealthChanged -= _hUDPlayer.SetPlayerHealth;
            GameEvents.OnScoreChanged -=_HUDScore. SetPlayerScore;
            GameEvents.OnCoinChanged -= _hUDPlayer.SetItemCount;
            GameEvents.OnSpeedChanged -= _hUDPlayer.SpeedTxt;
            GameEvents.OnSwitchBoosted -=_HUDBoost. SwitchBoosted;
            GameEvents.OnShieldBoosted -=_HUDBoost. ShieldBoosted;
            GameEvents.OnPowerUp -=_HUDBoost. PowerUpBoosted;
            GameEvents.OnFlyingBoosted -=_HUDBoost. FlyingBoosted;
        }
     
      

    }
    [System.Serializable]
    public class SaveData
    {
        public int highScore;
        public int characterIndex;
    }