
    using UnityEngine;

    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData data;
        public ItemData Data => data;

        public void Collect(Player player, Collider2D other=null)
        {
            GameEvents.OnItemCollected?.Invoke(data, other);
            if (data.effect!=ItemEffect.Jump)
                GameEvents.OnReleaseItem?.Invoke(gameObject);
            else GetComponent<Animator>().CrossFade("jumpTable",0.5f);
        }
    }
