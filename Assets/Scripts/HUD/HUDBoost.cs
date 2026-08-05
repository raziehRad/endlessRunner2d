
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class HUDBoost : MonoBehaviour
    {
        [SerializeField] protected Image shieldTimerImage;
        [SerializeField] protected Image powerUpTimerImage;
        [SerializeField] protected Image flyingTimerImage;
        [SerializeField] protected TextMeshProUGUI _boostedItem;
        [SerializeField]protected HUDAnimation _HUDAnimate;
        internal void FlyingBoosted(float itemDuration)
        {
            StartCoroutine(FlyingCoroutine(itemDuration));
        }
        internal void PowerUpBoosted(float itemDuration)
        {
            StartCoroutine(PowerUpCoroutine(itemDuration));
        }
        internal void ShieldBoosted(float itemDuration)
        {
            StartCoroutine(ShieldCoroutine(itemDuration));
        }
       private  IEnumerator ShieldCoroutine(float itemDuration)
        {
            shieldTimerImage.gameObject.SetActive(true);
            float timer = itemDuration;

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                shieldTimerImage.fillAmount = timer / itemDuration;

                yield return null;
            }
            shieldTimerImage.gameObject.SetActive(false);
        }
       private IEnumerator PowerUpCoroutine(float itemDuration)
        {
            powerUpTimerImage.gameObject.SetActive(true);
            float timer = itemDuration;

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                powerUpTimerImage.fillAmount = timer / itemDuration;

                yield return null;
            }
            powerUpTimerImage.gameObject.SetActive(false);
        }
       private IEnumerator FlyingCoroutine(float itemDuration)
        {
            flyingTimerImage.gameObject.SetActive(true);
            float timer = itemDuration;

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                flyingTimerImage.fillAmount = timer / itemDuration;

                yield return null;
            }
            flyingTimerImage.gameObject.SetActive(false);
        }
        internal void SwitchBoosted(bool isBoosted)
        {
            _boostedItem.gameObject.SetActive(isBoosted);
            if (isBoosted)
                _HUDAnimate.ScaleBounce(_boostedItem.transform);
        }
    }
