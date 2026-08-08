using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private GameObject _dieFX;
        public PlayerState currenctState { get; private set; }
        private Animator _animator;
        private PlayerMovement _movement;
        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GameEvents.OnPlayerDied+= ChangeState;
        }
        private void OnDisable()
        {
            GameEvents.OnPlayerDied-= ChangeState;
        }
        void Start()
        {
            currenctState = PlayerState.Ideal;
        }

        public void ChangeState(PlayerState newState)
        {
            currenctState = newState;
            switch (newState)
            {
                case PlayerState.Jump:
                    _movement.Jump();
                    break;
                case PlayerState.Die:
                    GameOverAction();
                    break;
                case PlayerState.Fall:
                    GameOverAction();
                    break;
            }
        }

        public void Tick()
        {
            ChangeState(transform.position.y < -9 ? PlayerState.Fall : currenctState);
        }
        
        private void GameOverAction()
        {
            AudioManager.instance.Play(SoundType.GameOver);
            _animator.CrossFade("Die",0.5f);
            Instantiate(_dieFX, transform.position, quaternion.identity);
            Invoke("ReLoad",0.2f);
        }
        
        private void ReLoad()
        {
            SceneManager.LoadScene(0);
        }
    }
}