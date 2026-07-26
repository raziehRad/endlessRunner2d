using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool itemPool;
        [SerializeField] private ObjectPool enemyPool;

        private GroundSpawner _spawner;
        public void Start()
        {
            _spawner = GetComponent<GroundSpawner>();
        } 

        public void SpawnItems(GameObject ground, bool safeSpawn)
        {
            TrySpawn(ground, itemPool, ItemType.Item);

            if (!safeSpawn)
            {
                TrySpawn(ground, enemyPool, ItemType.Enemy);
            }
        }

        private void TrySpawn(GameObject ground, ObjectPool pool, ItemType type)
        {
            if (Random.value>0.5f)return;
            var xpos= SetXPosition(ground);
            
            var item = pool.GetFromPool();
            if (item==null)return;

            item.SetActive(true);
            item.transform.SetParent(ground.transform);

            if (type == ItemType.Enemy)
            {
                var data = item.GetComponent<FlyingDamage>().Data;
                item.transform.position = new Vector3(xpos, ground.transform.position.y + data.ypos);
            }
            else
            {
                var itemComponent = item.GetComponent<Item>();
                if (itemComponent == null) return;
                item.transform.position = new Vector3(xpos, ground.transform.position.y + 3);
                if (itemComponent.Data.effect== ItemEffect.Heal) { }
                else
                    item.transform.DOScale(new Vector3(0.3f, 0.6f, 0.3f), 0.01f);
            }
        }

        private float SetXPosition(GameObject ground)
        {
            var chancePos = Random.Range(0f, 1f);
            float xpos = ground.transform.position.x;
            if (chancePos < 0.35) xpos = ground.transform.position.x; //middle
            else if (chancePos > 0.35 && chancePos < 0.7)
                xpos = ground.transform.position.x + (_spawner.GetWidth(ground) / 2f) - 2; //left
            else if (chancePos > 0.7) xpos = ground.transform.position.x - (_spawner.GetWidth(ground) / 2f) + 2; //right
            return xpos;
        }
    }
}