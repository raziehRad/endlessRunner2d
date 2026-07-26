
    using UnityEngine;

    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData data;
        public ItemData Data => data;

        public void Collect(Player player, Collider2D other=null)
        {
            player.ApplyItem(data,other);
            gameObject.SetActive(false);
        }
    }
