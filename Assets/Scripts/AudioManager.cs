
    using System;
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        [SerializeField] private AudioClip _jumpClip;
        [SerializeField] private AudioClip _coinClip;
        [SerializeField] private AudioClip _gameoveClip;

        private AudioSource _audioSource;

        private void Awake()
        {
            instance = this;

            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip)
        {
            if (clip!=null)
            {
                if (clip != null) _audioSource.PlayOneShot(clip);
            }
        }

        public void PlayJump()
        {
            if (_jumpClip != null) PlaySound(_jumpClip);
        }

        public void PlayCoin()
        {
            if (_coinClip != null) PlaySound(_coinClip);
        }

        public void PlayGameOver()
        {
            if (_gameoveClip != null) PlaySound(_gameoveClip);
        }
    }
