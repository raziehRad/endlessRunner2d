
    using UnityEngine;

    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData data;
        public ItemData Data => data;

        // Handle item collection and trigger its effect
        public void Collect(Player player, Collider2D other=null)
        {
            GameEvents.OnItemCollected?.Invoke(data, other);
            if (data.effect == ItemEffect.Jump)
                GetComponent<Animator>().CrossFade("jumpTable", 0.5f);
            //else
                GameEvents.OnReleaseItem?.Invoke(gameObject);
        }
    }
