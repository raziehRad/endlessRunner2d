
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private HUDManager _hudManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Player _player;
        [SerializeField] private GroundManager _groundManager;

        public HUDManager HUDManager => _hudManager;
        public AudioManager AudioManager => audioManager;
        public GroundManager GroundManager => _groundManager;
        public Player Player => _player;
        private void Awake()
        {
            Instance = this;
        }

    }
