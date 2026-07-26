
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private HUDManager _hudManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Player _player;

        public HUDManager HUDManager => _hudManager;
        public AudioManager AudioManager => audioManager;
        public Player Player => _player;
        private void Awake()
        {
            Instance = this;
        }

    }
