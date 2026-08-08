
    using System.Collections.Generic;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private HUDManager _hudManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Player _player;
        [SerializeField] private GroundManager _groundManager;
        private void Awake()
        {
            Instance = this;
        }

    }
