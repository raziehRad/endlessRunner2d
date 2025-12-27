using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public PlayerState currenctState { get; private set; }

        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerBoost _boost;
        [SerializeField] private GameObject _dieFX;
        
        private Animator _animator;

        void Start()
        {
            currenctState = PlayerState.Ideal;
            _animator = GetComponent<Animator>();
        }

        public void ChangeState(PlayerState newState)
        {
            currenctState = newState;
            switch (newState)
            {
                case PlayerState.Jump:
                    _movement.Jump();
                    break;
                case PlayerState.Run:
                    _boost.EnableBoost(); 
                    break;
                case PlayerState.Die:
                    GameOverAction();
                    break;
                case PlayerState.Fall:
                    GameOverAction();
                    break;
                case PlayerState.Ideal:
                    _boost.DisableBoost();
                     break;
            }
        }

        public void Tick()
        {
            ChangeState(transform.position.y < -9 ? PlayerState.Fall : currenctState);
        }
        
        private void GameOverAction()
        {
            AudioManager.instance.PlayGameOver();
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