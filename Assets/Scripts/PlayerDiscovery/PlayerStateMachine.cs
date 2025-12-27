using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public PlayerState currenctState { get; private set; }

        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerBoost _boost;

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
                    break;
                case PlayerState.Fall:
                    SceneManager.LoadScene(0);
                    break;
                case PlayerState.Ideal:
                    _boost.DisableBoost();
                     break;
            }
        }

        public void Tick()
        {
            if (transform.position.y<-9)
                ChangeState(PlayerState.Fall);
        }
    }
}