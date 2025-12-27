using UnityEngine;

namespace DefaultNamespace
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool itemPool;
        [SerializeField] private ObjectPool enemyPool;

        public void SpawnItems(GameObject ground, bool safeSpawn)
        {
            TrySpawn(ground, itemPool, ItemType.item);

            if (!safeSpawn)
            {
                TrySpawn(ground, enemyPool, ItemType.enemy);
            }
        }

        private void TrySpawn(GameObject ground, ObjectPool pool, ItemType type)
        {
            if (Random.value>0.5f)return;

            var item = pool.GetFromPool();
            if (item==null)return;
            
            item.SetActive(true);
            item.transform.SetParent(ground.transform);

            var x = ground.transform.position.x;
            var y = ground.transform.position.y;

            if (type == ItemType.enemy)
            {
                y += item.GetComponent<FlyingDamage>().Data.ypos;
            }
            else
                y += 3;

            item.transform.position = new Vector3(x, y);
        }

       
    }
}