
    using UnityEngine;

    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData data;
        public ItemData Data => data;

        public void Collect(Player player, Collider2D other=null)
        {
            player.ApplyItem(data,other);
            if (data.effect!=ItemEffect.Jump) 
                GameManager.Instance.GroundManager.Spawner.ItemSpawner.ReleaseItem(gameObject);

            else GetComponent<Animator>().CrossFade("jumpTable",0.5f);
            //gameObject.SetActive(false);
        }
    }
