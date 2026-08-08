
    using System;
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        [SerializeField] private AudioClip _jumpClip;
        [SerializeField] private AudioClip _coinClip;
        [SerializeField] private AudioClip _pickupClip;
        [SerializeField] private AudioClip _gameoveClip;

        private AudioSource _audioSource;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            _audioSource = GetComponent<AudioSource>();
        }
        private void OnEnable()
        {
            GameEvents.OnCoinChanged += PlayCoin;
        }
        private void OnDisable()
        {
            GameEvents.OnCoinChanged -= PlayCoin;
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip != null) _audioSource.PlayOneShot(clip);
        }
        public void Play(SoundType sound)
        {
            switch (sound)
            {
                case SoundType.Jump:
                    PlaySound(_jumpClip);
                    break;

                case SoundType.Coin:
                    PlaySound(_coinClip);
                    break;

                case SoundType.Pickup:
                    PlaySound(_pickupClip);
                    break;

                case SoundType.GameOver:
                    PlaySound(_gameoveClip);
                    break;
            }
        }

        private void PlayCoin(int _)
        {
          PlaySound(_coinClip);
        }
    }
    public enum SoundType
    {
        Jump,
        Coin,
        Pickup,
        GameOver
    }