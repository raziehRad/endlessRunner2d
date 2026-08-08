using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPickHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _coinFX;
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Item item))
                return;
            item.Collect(_player, other);

            if (item.Data.effect != ItemEffect.AddCoin && item.Data.effect != ItemEffect.Jump)
            {
                AudioManager.instance.Play(SoundType.Pickup);
            }
        }
        
        public void SpawnCoinEffect(Collider2D other,int score)
        {
            var fx = Instantiate(_coinFX, other.transform.position, quaternion.identity);
            fx.GetComponent<ParticleSystem>().Play();
            GameEvents.OnScoreChanged?.Invoke(other.GetComponent<Item>().Data.value);
            Sequence sequence = DOTween.Sequence();

            sequence.Append(other.transform.DOScale(new Vector3(0.5f, 0.8f, 0.5f), 0.3f))
                .Append(other.transform.DOScale(new Vector3(0.2f, 0.5f, 0.2f), 0.2f))
                .OnComplete(() =>
                {
                    other.gameObject.SetActive(false);
                    other.transform.localScale = new Vector3(0.3f, 0.6f, 0.3f);
                });

           
            GameEvents.OnSpeedAddCoin?.Invoke(1);
            Destroy(fx,0.7f);
        }
    }
}