using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
// Handles spawning and recycling of items and enemies
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool itemPool;
        [SerializeField] private ObjectPool enemyPool;

        private GroundSpawner _spawner;

        private int _lastItemIndex = -1;
        private int _lastEnemyIndex = -1;

        private void OnEnable()
        {
            GameEvents.OnReleaseItem += ReleaseItem;
        }

        private void OnDisable()
        {
            GameEvents.OnReleaseItem -= ReleaseItem;
        }

        public void Start()
        {
            _spawner = GetComponent<GroundSpawner>();
        } 
        // Spawn items and enemies on the given ground segment
        public void SpawnItem(GameObject ground, bool safeSpawn)
        {
            TrySpawn(ground, itemPool, ItemType.Item);

            if (!safeSpawn)
            {
                TrySpawn(ground, enemyPool, ItemType.Enemy);
            }
        }
        // Try to spawn an item or enemy at a random position
        private Task TrySpawn(GameObject ground, ObjectPool pool, ItemType type)
        {
            if (Random.value>0.5f)return Task.CompletedTask;
            var xpos= SetXPosition(ground);
            
           var item = pool.GetFromPool(2);
            if (item==null)return Task.CompletedTask;

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
                if (itemComponent == null)
                {
                    for (int i = 0; i < item.transform.childCount; i++)
                    {
                        item.transform.GetChild(i).gameObject.SetActive(true);
                    }

                    return Task.CompletedTask;
                }
                item.transform.position = new Vector3(xpos, ground.transform.position.y +itemComponent.Data.yPos);
                item.transform.DOScale(itemComponent.Data.scale, 0.01f);
            }

            return Task.CompletedTask;
        }
        // Calculate a random horizontal position on the ground
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
public void SpawnItems(GameObject ground, bool safeSpawn)
    {
        SpawnItem(ground);

        if (!safeSpawn)
        {
            SpawnEnemy(ground);
        }
    }

    private void SpawnItem(GameObject ground)
    {
        if (Random.value > 0.5f)
            return;

        int itemIndex = GetDifferentIndex(
            itemPool,
            _lastItemIndex
        );

        _lastItemIndex = itemIndex;

        var item = itemPool.GetFromPool(itemIndex);

        if (item == null)
            return;

        item.SetActive(true);
        item.transform.SetParent(ground.transform);

        var itemComponent = item.GetComponent<Item>();

        if (itemComponent == null)
        {
            for (int i = 0; i < item.transform.childCount; i++)
            {
                item.transform.GetChild(i).gameObject.SetActive(true);
            }

            return;
        }

        float xpos = SetXPosition(ground);

        item.transform.position = new Vector3(
            xpos,
            ground.transform.position.y + itemComponent.Data.yPos,
            0
        );

        item.transform.DOScale(
            itemComponent.Data.scale,
            0.01f
        );
    }

    private void SpawnEnemy(GameObject ground)
    {
        if (Random.value > 0.5f)
            return;

        int enemyIndex = GetDifferentIndex(
            enemyPool,
            _lastEnemyIndex
        );

        _lastEnemyIndex = enemyIndex;

        var enemy = enemyPool.GetFromPool(enemyIndex);

        if (enemy == null)
            return;

        enemy.SetActive(true);
        enemy.transform.SetParent(ground.transform);

        if (!enemy.TryGetComponent(out FlyingDamage flyingDamage))
            return;

        float xpos = SetXPosition(ground);

        enemy.transform.position = new Vector3(
            xpos,
            ground.transform.position.y + flyingDamage.Data.ypos,
            0
        );
    }

    private int GetDifferentIndex(ObjectPool pool, int lastIndex)
    {
        int index;

        do
        {
            index = Random.Range(0, pool.PrefabCount);
        }
        while (pool.PrefabCount > 1 && index == lastIndex);

        return index;
    }

        private void ReleaseItem(GameObject obj)
        {
            itemPool.ReturnToPool(obj);
        }
    }
