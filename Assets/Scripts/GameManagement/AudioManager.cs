
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    // Manages game audio and loads AudioClips through Addressables.
// Loaded clips are cached to avoid loading the same asset multiple times.
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        [SerializeField] private AssetReferenceT<AudioClip> _jumpClip;
        [SerializeField] private AssetReferenceT<AudioClip> _coinClip;
        [SerializeField] private AssetReferenceT<AudioClip> _pickupClip;
        [SerializeField] private AssetReferenceT<AudioClip> _gameoveClip;

        private AudioSource _audioSource;

        // Keeps already loaded clips in memory for quick playback.
        private readonly Dictionary<AssetReferenceT<AudioClip>, AudioClip> _loadedClips = new();
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
        private void Start()
        {
            // Preload all required audio clips at startup.
            LoadClip(_jumpClip);
            LoadClip(_coinClip);
            LoadClip(_pickupClip);
            LoadClip(_gameoveClip);
        }

        private void LoadClip(AssetReferenceT<AudioClip> reference)
        {
            if (reference == null || !reference.RuntimeKeyIsValid())
                return;

            reference.LoadAssetAsync<AudioClip>().Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    _loadedClips[reference] = handle.Result;
                }
                else
                {
                    Debug.LogError($"Failed to load audio: {reference.RuntimeKey}");
                }
            };
        }
      
        private void PlaySound(AssetReferenceT<AudioClip> reference)
        {
            if (reference == null)
                return;

            // Play the cached clip instead of loading it again.
            if (_loadedClips.TryGetValue(reference, out AudioClip clip))
            {
                _audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"Audio is not loaded yet: {reference.RuntimeKey}");
            }
        }

        // Provides a single entry point for playing different game sounds.
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
    }
    public enum SoundType
    {
        Jump,
        Coin,
        Pickup,
        GameOver
    }