
    using DefaultNamespace;
    using UnityEngine;

    public class PlayerItemHandler: MonoBehaviour
    {
        private PlayerHealth _health;
        private PlayerBoost _boost;
        private PlayerMovement _movement;
        private PlayerScore _score;
        
        private void Awake()
        {
            _health = GetComponent<PlayerHealth>();
            _boost = GetComponent<PlayerBoost>();
            _movement = GetComponent<PlayerMovement>();
            _score = GetComponent<PlayerScore>();
        }
        private void OnEnable()
        {
            GameEvents.OnItemCollected += ApplyItem;
        }

        private void OnDisable()
        {
            GameEvents.OnItemCollected -= ApplyItem;
        }
        private void ApplyItem(ItemData item, Collider2D other=null)
        {
            switch (item.effect)
            {
                case ItemEffect.Heal:
                    _health.Heal(item.value);
                    break;
                case ItemEffect.Shield:
                    _boost.EnableBoost(BoostType.shield,item);
                    break;
                case ItemEffect.WeaponUpgrade:
                    _boost.EnableBoost(BoostType.powerUp, item);
                    break;
                case ItemEffect.Flying:
                    _boost.EnableBoost(BoostType.flying, item);
                    break;
                case ItemEffect.AddCoin:
                    _score.AddCoin(item.value,other);
                    break;
                case ItemEffect.Jump:
                    _movement.JumpMode(item);
                    break;
            }
        }
    }
