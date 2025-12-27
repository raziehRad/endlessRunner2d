using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBoost : MonoBehaviour
    {
        [SerializeField] private float boostDuration = 3f;
        [SerializeField] private GroundManager ground;

        private float timer;
        private bool isBoosted;

        public void EnableBoost()
        {
            isBoosted = true;
            timer = boostDuration;
            StartCoroutine(BoostTimer());
            var speed= ground.Mover.MoveSpeed + 2;
            ground.SetMoveSpeed(speed) ;
            HUDManager.instace.SwitchBoosted(true);
        }

        private IEnumerator BoostTimer()
        {
            yield return new WaitForSeconds(timer);
            DisableBoost();
        }

        public void DisableBoost()
        {
            if (!isBoosted)return;

            isBoosted = false;
            StopCoroutine(BoostTimer());
            ground.SetMoveSpeed(5) ;
            HUDManager.instace.SwitchBoosted(false);
        }
        
    }
}