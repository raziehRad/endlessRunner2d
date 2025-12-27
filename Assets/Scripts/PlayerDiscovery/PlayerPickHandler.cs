using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPickHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _coinFX;
        private int itemCount;
        private PlayerStateMachine _playerStateMachine;
        private GroundManager ground;
        private void Start()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine>();
            ground = GetComponent<GroundManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("item"))
            {
                AudioManager.instance.PlayCoin();
               SpawnCoinEffect(other);
               HUDManager.Instance.SetItemCount(itemCount);
            }
        }
        
        private void SpawnCoinEffect(Collider2D other)
        {
            var fx = Instantiate(_coinFX, other.transform.position, quaternion.identity);
            fx.GetComponent<ParticleSystem>().Play();
            HUDManager.Instance.SetPlayerScore(other.GetComponent<Item>().Data.value);
            other.transform.DOScale(new Vector3(0.5f, 0.8f, 0.5f), 0.3f)
                .SetEase(Ease.OutBack).OnComplete((() =>
                {
                    other.transform.DOScale(new Vector3(0.2f, 0.5f, 0.2f), 0.2f)
                        .SetEase(Ease.InOutSine).OnComplete(() =>
                        {
                            other.gameObject.SetActive(false);
                            Destroy(fx);
                            other.transform.DOScale(new Vector3(0.3f, 0.6f, 0.3f), 0.2f);
                        });
                }));
            itemCount++;
            if (itemCount == 10)
            {
                _playerStateMachine.ChangeState(PlayerState.Run);
                var speed= ground.Mover.MoveSpeed + 2;
                ground.SetMoveSpeed(speed) ;
                itemCount = 0;
            }
        }
    }
}